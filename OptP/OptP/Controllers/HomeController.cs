using Core;
using Entities;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var idUsuario = Request.Cookies["loggedUser"];
            return View();
        }

        [HttpPost]
        public JsonResult ResolverProblema()
        {
            var serializer = new JavaScriptSerializer();
            var req = Request.InputStream;
            var json = new StreamReader(req).ReadToEnd();
            // Convertendo um JSON do modelo em um objeto do modelo
            ModeloMatematico modelo = serializer.Deserialize<ModeloMatematico>(json);
            Resolvedor resolvedor = new Resolvedor();
            // Chamando função para resolver o problema
            Solucao solucao = resolvedor.ResolverProblemaMatematico(modelo);
            // Retornando o resultado
            return new JsonResult() { Data = serializer.Serialize(solucao) };
        }
    }
}