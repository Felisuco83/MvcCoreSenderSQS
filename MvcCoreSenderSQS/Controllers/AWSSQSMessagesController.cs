using Microsoft.AspNetCore.Mvc;
using MvcCoreSenderSQS.Models;
using MvcCoreSenderSQS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSenderSQS.Controllers
{
    public class AWSSQSMessagesController : Controller
    {
        private ServiceSQS ServiceSQS;

        public AWSSQSMessagesController(ServiceSQS servicesqs)
        {
            this.ServiceSQS = servicesqs;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(MensajeUsuario msj)
        {
            msj.FechaMensaje = DateTime.Now;
            bool respuesta = await this.ServiceSQS.SendMessageAsync(msj);
            ViewBag.Mensaje = "Mensaje enviado a SQS: " + respuesta;
            return View();
        }
    }
}
