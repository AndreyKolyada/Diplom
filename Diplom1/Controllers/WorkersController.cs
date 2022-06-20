using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diplom1.Models;
using System.Data.Entity.Infrastructure;

namespace Diplom1.Controllers
{
    [Authorize(Roles = "manager")]
    public class WorkersController : Controller
    {
        private ServiceStationContext db = new ServiceStationContext();
        private ApplicationDbContext userDb = new ApplicationDbContext();
        // GET: Workers
        public ActionResult Index()
        {
            return View(db.Workers.ToList());
        }

        // GET: Workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.User = userDb.Users.Find(worker.AccountID);
            return View(worker);
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
            ViewBag.Jobs = db.Jobs.ToList();
            ViewBag.Days = db.Days.ToList();
            ViewBag.Users = userDb.Users.ToList();
            return View();
        }

        // POST: Workers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Surname,AccountID,Jobs,Days")] Worker worker, int[] selectedJobs, int[] selectedDays)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    worker.Status = "Работает";
                    foreach (var j in db.Jobs.Where(j1 => selectedJobs.Contains(j1.Id)))
                    {
                        worker.Jobs.Add(j);
                    }
                    foreach (var d in db.Days.Where(d1 => selectedDays.Contains(d1.Id)))
                    {
                        worker.Days.Add(d);
                    }
                    db.Workers.Add(worker);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Невозможно сохранить результат, при повторении ошибки обратитесь к администрации сайта.");
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.Jobs = db.Jobs.ToList();
            ViewBag.Days = db.Days.ToList();
            ViewBag.Users = userDb.Users.ToList();
            return View(worker);
        }

        // POST: Workers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,AccountID,Status,Jobs,Days")] Worker worker, string selectedStatus, int[] selectedJobs, int[] selectedDays)
        {
            if (ModelState.IsValid)
            {
                if (selectedStatus == "enabled")
                {
                    worker.Status = "Работает";
                }
                else
                {
                    worker.Status = "Уволен";
                }
                DbEntityEntry<Worker> item = db.Entry<Worker>(worker);
                item.State = EntityState.Modified;
                item.Collection(i => i.Jobs).Load();
                worker.Jobs.Clear();
                foreach (int jobId in selectedJobs)
                {
                    var job = db.Jobs.Find(jobId);
                    worker.Jobs.Add(job);
                }
                item.Collection(i => i.Days).Load();
                worker.Days.Clear();
                foreach (int dayId in selectedDays)
                {
                    Day day = db.Days.Find(dayId);
                    worker.Days.Add(day);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
