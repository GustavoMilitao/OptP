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
            User user = null;
            var cookie = Request.Cookies["loggedUser"];
            if (cookie != null)
            {
                user = UserCore.Get(cookie.Value);
            }
            if (user == null)
            {
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now;
                    cookie.Value = "";
                }
                return View();
            }
            else
            {
                return View("../Home/Index");
            }
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
            var result = JsonConvert.DeserializeObject<User>(json);

            var user = UserCore.GetByUsuarioESenha(result.Usuario, result.Senha);
            if (user == null)
            {
                var cookie = Request.Cookies["loggedUser"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now;
                    cookie.Value = "";
                }
                return Json(new { data = new { success = false, message = "Usuário inválido!" } });
            }
            else
            {
                return Json(new { success = true, user = user._id.ToString() });
            }
        }

        [HttpPost]
        public JsonResult Registrar()
        {
            var req = Request.InputStream;
            var json = new StreamReader(req).ReadToEnd();
            var result = JsonConvert.DeserializeObject<User>(json);
            var user = UserCore.GetByUsuario(result.Usuario);
            if (user != null)
            {
                var cookie = Request.Cookies["loggedUser"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now;
                    cookie.Value = "";
                }
                return Json(new { success = false, message = "Usuário já cadastrado!" });
            }
            else
            {
                var insertedUser = new User(result.Usuario, result.Senha);
                UserCore.Post(insertedUser);
                return Json(new { success = true, user = insertedUser._id.ToString() });
            }
        }
    }
}