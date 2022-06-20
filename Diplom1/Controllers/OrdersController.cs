using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diplom1.Models;

namespace Diplom1.Controllers
{
    [Authorize(Roles ="user")]
    public class OrdersController : Controller
    {
        private ServiceStationContext db = new ServiceStationContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Worker);
            orders = orders.Include(o => o.Job);
            orders = orders.Include(o => o.Person);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Job = db.Jobs.Find(order.JobId);
            order.Worker = db.Workers.Find(order.WorkerId);
            order.Person = db.Persons.Find(order.PersonId);
            return View(order);
        }

        [HttpPost]
        public ActionResult Details(int id)
        {
            Order order = db.Orders.Find(id);
            if (order.Status == "Заявка")
            {
                order.Status = "Подтверждён";
            }
            else
            {
                if (order.Status == "Подтверждён")
                {
                    order.Status = "Контроль исполнения";
                }
                else
                {
                    if (order.Status == "Контроль исполнения")
                    {
                        order.Status = "Выполнен";
                    }
                }
            }
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Jobs = db.Jobs.ToList();
            ViewBag.Days = db.Days.ToList();
            db.Jobs.ToList().Select(c => c.Name);
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Car,Date,PesonId,JobId,Comment,Status")] Order order, Person person, int jobId)
        {
            if (ModelState.IsValid)
            {
                if (db.Persons.Any(p => p.Phone == person.Phone))
                {
                    foreach (Person p in db.Persons.Where(s => s.Phone == person.Phone))
                    {
                        p.Name = person.Name;
                        person.Id = p.Id;
                        db.Entry(p).State = EntityState.Modified;
                    }
                }
                else
                {
                    db.Persons.Add(person);
                }
                db.SaveChanges();
                order.Status = "Заявка";
                order.JobId = jobId;
                order.PersonId = person.Id;
                db.Orders.Add(order);
                db.SaveChanges();
                if(User.Identity.IsAuthenticated==false)
                {
                    return Redirect("/home/");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(order);
        }


        [Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if(User.IsInRole("manager") == false)
            {
                Response.StatusCode = 403;
                return View();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var q = from f in db.Workers where f.Days.Any(g => g.Id == 1) select f;
            foreach (Worker c in db.Workers.Where((d => d.Days.Any(e => e.Id == 1) && d.Jobs.Any(e => e.Id == 1))))
            {
                string a = c.Name;
            }
            ViewBag.Jobs = db.Jobs.ToList();
            ViewBag.Days = db.Days.ToList();
            ViewBag.Workers = db.Workers.ToList();
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Car,Date,PersonId,JobId,Comment,Status,WorkerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public PartialViewResult WorkerDropDown(int orderId, int jobId, string day)
        {
            ViewBag.Jobs = db.Jobs.ToList();
            ViewBag.Days = db.Days.ToList();
            ViewBag.Workers = db.Workers.ToList();
            ViewBag.JobId = jobId;
            ViewBag.Day = DateTime.Parse(day).DayOfWeek;
            return PartialView("WorkerDropDown", db.Orders.Find(orderId));
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
