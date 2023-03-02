using Microsoft.AspNet.Identity.EntityFramework;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datos;
using loginsys.Models;
using Microsoft.AspNet.Identity;

namespace loginsys.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            var estaAutenticado = User.Identity.IsAuthenticated;

            if (estaAutenticado)
            {
                var NombreUsuario = User.Identity.Name;
                var id = User.Identity.GetUserId();

                //Info Del Usuario
                using(ApplicationDbContext db = new ApplicationDbContext())
                {
                    var usuario = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    var emailConfirmado = usuario.EmailConfirmed;
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                // Registrar autenticación
                TwoFactorAuthenticator autenticador = new TwoFactorAuthenticator();

                // El código secreto es por usuario
                var setupInfo = autenticador.GenerateSetupCode("LoginSys",
                    User.Identity.Name,
                    "ALSKDMASKLDMKALDKSA", 300, 300);

                ViewBag.qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.manualEntrySetupCode = setupInfo.ManualEntryKey;

                //login

                bool pinCorrecto = autenticador.ValidateTwoFactorPIN("ALSKDMASKLDMKALDKSA",
                    "239056");
            }

            ////Creacion De Roles
            //if(User.Identity.IsAuthenticated)
            //{
            //    using (ApplicationDbContext db = new ApplicationDbContext())
            //    {
            //        var idUsuarioActual = User.Identity.GetUserId();

            //       var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //        //Crear Rol
            //       var resultado = roleManager.Create(new IdentityRole("User"));

            //       var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //        //Agregar usuario al rol
            //        resultado = userManager.AddToRole(idUsuarioActual, "User");

            //        //Usuario esta en ese rol?
            //       var usuarioEstaEnRol1 = userManager.IsInRole(idUsuarioActual, "User");
            //        var usuarioEstaEnRol2 = userManager.IsInRole(idUsuarioActual, "Admin");

            //        //Roles Usuarios
            //       //var roles = userManager.GetRoles(idUsuarioActual, "Admin");
            //    }
            //}
            
            return View();

        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}