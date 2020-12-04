using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;

namespace PL.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly Mapper _mapper;
        private IRoleService RoleService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IRoleService>();
            }
        }

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        /// <summary>
        /// Admin controller ctor
        /// </summary>
        public AdminController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoleDto, RoleViewModel>().ReverseMap();
                cfg.CreateMap<UserDto, UserViewModel>().ReverseMap();
            });

            _mapper = new Mapper(config);
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <param name="submit">Cancel or Create param</param>
        /// <returns></returns>
        //GET: Admin/GetRoles
        public ActionResult GetRoles(string submit)
        {
            switch (submit)
            {
                case "Cancel":
                    return RedirectToAction("Index");
                case "Create":
                    return RedirectToAction("AddRole");
                default:
                    var roles = RoleService.GetRoles();
                    var data = new Tuple<IEnumerable<RoleViewModel>, RoleViewModel>(_mapper.Map<IEnumerable<RoleDto>, IEnumerable<RoleViewModel>>(roles), new RoleViewModel());
                    return View(data);
            }
        }

        //GET
        public ActionResult AddRole()
        {
            return View();
        }

        /// <summary>
        /// Add role 
        /// </summary>
        /// <param name="model">Role;s model</param>
        /// <param name="submit">action param</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddRole(RoleViewModel model, string submit)
        {
            switch (submit)
            {
                case "Add":
                    if (!ModelState.IsValid)
                    {
                        ModelState.AddModelError(string.Empty, "The data is not valid");
                        return View(model);
                    }
                    await RoleService.Create(model.Name);
                    return RedirectToAction("GetRoles");
                case "Cancel":
                    return RedirectToAction("GetRoles");
                default:
                    return RedirectToAction("Index");
            }
        }

        //GET:/Admin/GetUsers
        public ActionResult GetUsers()
        {
            var users = UserService.GetUsers().ToList();

            users.RemoveAll(u => u.Roles.Any(r => r == "admin"));

            ViewBag.Item = "Users";

            return View(_mapper.Map<IEnumerable<UserDto>, IEnumerable<UserViewModel>>(users));
        }

        public ActionResult Back()
        {
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// LOck or Remove user's account
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="submit">lock or remove param</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> LockOrRemove(string email, string submit)
        {
            switch (submit)
            {
                case "Remove":
                    await UserService.RemoveUserAsync(email);
                    return Redirect("/Admin/Admin/GetUsers/");

                case "Lock":
                case "Unlock":
                    if (email != null)
                    {
                        await UserService.ChangeLockUserState(email);

                        return Redirect("/Admin/Admin/GetUsers/");
                    }
                    return Redirect("/Admin/Admin/");

                default:
                    return Redirect("Index");
            }

        }

        /// <summary>
        /// Remove a role
        /// </summary>
        /// <param name="roleName">role's name</param>
        /// <returns></returns>
        public async Task<ActionResult> RemoveRole(string roleName)
        {
            await RoleService.Remove(roleName);
            return Redirect("/Admin/Admin/GetRoles/");
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


    }
}