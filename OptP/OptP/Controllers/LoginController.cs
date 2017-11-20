using Core;
using Entities;
using Google.OrTools.LinearSolver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptP.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public JsonResult Login()
        {
            var req = Request.InputStream;
            var json = new StreamReader(req).ReadToEnd();
            var result = JsonConvert.DeserializeObject<Login>(json);
            // Verificar se existe na base
            return new JsonResult();
        }

        [HttpPost]
        public JsonResult Registrar()
        {
            var req = Request.InputStream;
            var json = new StreamReader(req).ReadToEnd();
            var result = JsonConvert.DeserializeObject<Login>(json);
            // Verificar se existe na e registrar
            return new JsonResult();
        }
    }
}