using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebCuoiKhoa.Models;
namespace ProjectWebCuoiKhoa.Controllers
{
    public class LoginUserController : Controller
    {
        DBQLBHEntities db = new DBQLBHEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAccount(AdminUser user)
        {
            var check = db.AdminUsers.Where(m => m.ID == user.ID && m.PasswordUser == user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorLogin = "Error";
                return View("Index");
            }
            else if(user.ID == 1)
            {
                Session["NameUser"] = user.NameUser;
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                return RedirectToAction("IndexCustomer", "Home");
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(AdminUser user)
        {
            AdminUser temp = db.AdminUsers.Find(user.ID);
            if (temp != null)
            {
                return RedirectToAction("SignUp","LoginUser");
            }
            else if (ModelState.IsValid)
            {                           
                db.AdminUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");               
            }
            else
            {
                ViewBag.ErrorSignUp = "Error Sign Up";
                return View("SignUp");
            }
        }

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}