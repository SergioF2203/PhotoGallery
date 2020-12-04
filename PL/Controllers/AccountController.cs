using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;

namespace PL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public AccountController() { }

        /// <summary>
        /// Log in action
        /// </summary>
        /// <param name="returnUrl">url for return after log in</param>
        /// <returns></returns>
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Log in action
        /// </summary>
        /// <param name="model">Login model</param>
        /// <param name="returnUrl">url for return</param>
        /// <param name="submit">action for log in</param>
        /// <returns></returns>
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string submit)
        {
            if (submit == "Log in")
            {
                await SetInitialData();

                if (ModelState.IsValid)
                {
                    var userDto = new UserDto { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claims = await UserService.Authenticate(userDto);

                    if (claims is null)
                    {
                        ModelState.AddModelError(string.Empty, "Login or Password is incorrect or Your account is blocked");
                        return View(model);
                    }
                    else
                    {
                        AuthenticationManager.SignOut();

                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true

                        }, claims);

                        foreach (var item in claims.Claims)
                        {
                            if (item.Value == "admin")
                                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        }
                    }
                }
            }

            return RedirectToAction("UserGallery", "Gallery");
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register action
        /// </summary>
        /// <param name="model">registration model</param>
        /// <param name="submit">action param</param>
        /// <returns></returns>
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string submit)
        {

            if (submit == "Register")
            {
                await SetInitialData();

                if (ModelState.IsValid)
                {
                    var user = new UserDto { Email = model.Email, UserName = model.Email, Password = model.Password };
                    OperationDetails res = await UserService.Create(user);

                    if (res.Succeeded)
                    {
                        ClaimsIdentity claims = await UserService.Authenticate(user);

                        AuthenticationManager.SignOut();

                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true

                        }, claims);

                        return RedirectToAction("UserGallery", "Gallery");
                    }

                    ModelState.AddModelError(res.Property, res.Message);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        //POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserService != null)
            {
                UserService.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        /// <summary>
        /// Initial stert data for admin account
        /// </summary>
        /// <returns></returns>
        private async Task SetInitialData()
        {
            await UserService.SetInitialData(new UserDto
            {
                Email = "admin@email.com",
                UserName = "admin@email.com",
                Password = "qwdqwd",
                Name = "admin",
                Roles = new List<string>() { "admin" }

            }, new List<string> { "admin", "user" });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
    #endregion
}
