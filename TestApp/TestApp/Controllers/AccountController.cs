using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(model);
                }

                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    if (model.RememberMe)
                    {
                        // They do, so let's create an authentication cookie
                        var cookie = FormsAuthentication.GetAuthCookie(model.UserName, model.RememberMe);
                        // Since they want to be remembered, set the expiration for 30 days
                        cookie.Expires = DateTime.Now.AddYears(-1);
                        // Store the cookie in the Response
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        // Otherwise set the cookie as normal
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    }

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nom utilisateur ou mot de passe est incorrecte.");
            }

            catch (Exception)
            {
                ViewBag.message = "Échec de la connexion";
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            FormsAuthentication.SignOut(); //In order to force logout in LDAP authentication
            return RedirectToAction("LogOn", "Account");
        }
    }
}