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

namespace PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly Mapper _mapper;
        private IRoleService RoleService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IRoleService>();
            }
        } 

        public RoleController() {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RoleDto, RoleViewModel>());
            _mapper = new Mapper(config);
        }

        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="admin")]
        public ActionResult AllRoles(UserDto userDto)
        {

            var roles = RoleService.GetAll();

            return View(_mapper.Map<IEnumerable<RoleDto>, IEnumerable<RoleViewModel>>(roles));
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(RoleViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            await RoleService.Create(model.Name);

            return RedirectToAction("AllRoles");
        }
    }
}