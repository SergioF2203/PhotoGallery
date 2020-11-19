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

        public AdminController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleDto, RoleViewModel>());
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
                    var roles = RoleService.GetAll();
                    return View(_mapper.Map<IEnumerable<RoleDto>, IEnumerable<RoleViewModel>>(roles));
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
                        return View("Error");
                    }
                    await RoleService.Create(model.Name);
                    return RedirectToAction("GetRoles");
                //case "Create":
                //    return RedirectToAction("AddRole");
                case "Cancel":
                    return RedirectToAction("GetRoles");
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}