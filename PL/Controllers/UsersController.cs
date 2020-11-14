using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using AutoMapper;
using BLL.Models;
using PL.Models;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        // GET: Users
        public ActionResult Index()
        {
            //var userModels = _userService.GetAll();
            //var users = _mapper.Map<IEnumerable<UserModel>, IEnumerable<UserViewModel>>(userModels);

            return View(/*users*/);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = await db.Users.FindAsync(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*user*/);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserName,UserEmail,UserImagePath")] UserViewModel user)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Users.Add(user);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            return View(/*user*/);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = await db.Users.FindAsync(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*user*/);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,UserEmail,UserImagePath")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(user).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = await db.Users.FindAsync(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*user*/);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //User user = await db.Users.FindAsync(id);
            //db.Users.Remove(user);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _uni .Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
