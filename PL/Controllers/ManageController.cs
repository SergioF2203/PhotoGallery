using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Models;
using DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;

namespace PL.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public ManageController()
        {
        }


        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
            };
            return View(model);
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model, string submit)
        {
            if(submit == "Cancel")
            {
                return RedirectToAction("UserGallery", "Gallery");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await UserService.FindByIdAsync(User.Identity.GetUserId());
                user.Password = model.NewPassword;

                ClaimsIdentity claims = await UserService.Authenticate(user);

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
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserService.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}