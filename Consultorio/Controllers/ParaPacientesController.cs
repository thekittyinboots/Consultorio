using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultorio.Controllers
{
    public class ParaPacientesController : Controller
    {
        // GET: ParaPacientes
        public ActionResult PedirTurno()
        {
            return View();
        }

        public ActionResult PedirTurnoHorario()
        {
            string valor = Convert.ToString(Request.QueryString["fecha"]);
            ViewBag.Fecha = valor;
            return View();
        }
    }
}