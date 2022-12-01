using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebCuoiKhoa.Models;
namespace ProjectWebCuoiKhoa.Controllers
{
    public class HomeController : Controller
    {
        DBQLBHEntities db = new DBQLBHEntities();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult IndexAdmin()
        {
            return View(db.Products.ToList());
        }
        public ActionResult IndexCustomer()
        {
            return View(db.Products.ToList());
        }

        public string GetSearchKey()
        {
            string key = Request.Form["SearchString"];
            return key;
        }
        public double GetMinPrice(double min = 1)
        {
            min = double.Parse(Request.Form["min"]);
            return min;
        }
        public double GetMaxPrice()
        {
            double Max = double.Parse(Request.Form["max"]);
            return Max;
        }
        public ActionResult Search(string key, double min, double? max)
        {
            min = GetMinPrice(min);
            max = GetMaxPrice();
            key = GetSearchKey();
            List<Product> result = db.Products.Where(s => (double)s.Price >= min && (double)s.Price <= max
            && s.NamePro.Contains(key)).ToList();
            return View(result);
        }
        public ActionResult SearchCustomer(string key, double min, double? max)
        {
            min = GetMinPrice(min);
            max = GetMaxPrice();
            key = GetSearchKey();
            List<Product> result = db.Products.Where(s => (double)s.Price >= min && (double)s.Price <= max
            && s.NamePro.Contains(key)).ToList();
            return View(result);
        }
        public ActionResult WorkingCate()
        {
            string idCate = "1";
            List<Product> WorkingPro = db.Products.Where(m => m.IDCate.Contains(idCate)).ToList();
            return View(WorkingPro);
        }
        public ActionResult GamingCate()
        {
            string idCate = "2";
            List<Product> GamingCate = db.Products.Where(m => m.IDCate.Contains(idCate)).ToList();
            return View(GamingCate);
        }
        public ActionResult WorkingCateCustomer()
        {
            string idCate = "1";
            List<Product> WorkingPro = db.Products.Where(m => m.IDCate.Contains(idCate)).ToList();
            return View(WorkingPro);
        }
        public ActionResult GamingCateCustomer()
        {
            string idCate = "2";
            List<Product> GamingCate = db.Products.Where(m => m.IDCate.Contains(idCate)).ToList();
            return View(GamingCate);
        }

    
    }
}