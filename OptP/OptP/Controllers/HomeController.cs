using Core;
using Entities;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string jsonStringModeloMatematico)
        {
            return View();
        }

        public JsonResult ResolverProblema(string jsonStringModeloMatematico)
        {
            var serializer = new JavaScriptSerializer();
            // Convertendo um JSON do modelo em um objeto do modelo
            ModeloMatematico modelo = serializer
                .Deserialize<ModeloMatematico>(jsonStringModeloMatematico);
            Resolvedor resolvedor = new Resolvedor();
            // Chamando função para resolver o problema
            Solucao solucao = resolvedor.ResolverProblemaMatematico(modelo);
            // Retornando o resultado
            return new JsonResult() { Data = serializer.Serialize(solucao) };
        }
    }
}