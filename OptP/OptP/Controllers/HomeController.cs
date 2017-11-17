using Optimization;
using Optimization.Solver.Cplex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OptP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CplexSolver simplex = new CplexSolver();
            Model modeloMatematico = new Model();

            //modeloMatematico.AddConstraint();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}