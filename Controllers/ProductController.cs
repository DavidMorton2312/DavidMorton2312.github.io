using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebCuoiKhoa.Models;
namespace ProjectWebCuoiKhoa.Controllers
{
    public class ProductController : Controller
    {
        DBQLBHEntities db = new DBQLBHEntities();
        public ActionResult Details(int? id)
        {
            List<Product> products = new List<Product>();
            products = db.Products.ToList();
            DetailViewProduct model = new DetailViewProduct();
            model.Products = products;
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            model.Product = pro;
            return View(model);
        }

       

        //admin
        public ActionResult List(string _name)
        {
            if (_name == null)
                return View(db.Products.ToList());
            else
                return View(db.Products.Where(s => s.NamePro.Contains(_name)).ToList());
        }
        public ActionResult DetailsAdmin(int? id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        
        public ActionResult CreateAdmin()
        {
            Product pro = new Product();
            return View(pro);
        }

        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = db.Categories.ToList<Category>();
            return PartialView(se_cate);
        }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
        [HttpPost]
        public ActionResult CreateAdmin(Product pro)
        {
            try
            {
                if(pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "/image/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("/image/"), filename));
                }
                if ((double)pro.Price <= 0)
                {
                    pro.Price=10000000;

                }
                string x = pro.NamePro;
                Product ProTemp = db.Products.Where(m => m.NamePro == x).FirstOrDefault();
                if(ProTemp==null)
                {
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin","Home");                          
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View(db.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                pro = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
                db.Products.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin","Home");
            }
            catch
            {
                return Content("Error delete");
            }
        }
        
        //customer
        public ActionResult DetailsCustomer(int? id)
        {
            List<Product> products = new List<Product>();
            products = db.Products.ToList();
            DetailViewProduct model = new DetailViewProduct();
            model.Products = products;
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            model.Product = pro;
            return View(model);
        }
    }
}