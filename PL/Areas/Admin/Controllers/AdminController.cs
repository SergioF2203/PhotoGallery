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

        public ActionResult GetUsers()
        {
            var users = UserService.GetUsers();
            return View(_mapper.Map<IEnumerable<UserDto>, IEnumerable<UserViewModel>>(users));
        }

        public ActionResult Back()
        {
            return Redirect(Request.UrlReferrer.ToString());
        }

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

        public async Task<ActionResult> RemoveRole(string roleName)
        {
            await RoleService.Remove(roleName);
            return Redirect("/Admin/Admin/GetRoles/");
        }

        public ActionResult LogOff()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}