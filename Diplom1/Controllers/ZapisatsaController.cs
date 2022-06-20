using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom1.Controllers
{
    public class ZapisatsaController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/orders/create/");
        }
    }
}