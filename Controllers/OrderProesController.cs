using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebCuoiKhoa.Models;

namespace ProjectWebCuoiKhoa.Controllers
{
    public class OrderProesController : Controller
    {
        private DBQLBHEntities db = new DBQLBHEntities();

        // GET: OrderProes
        public ActionResult Index()
        {
            var orderProes = db.OrderProes.Include(o => o.Customer);
            return View(orderProes.ToList());
        }
       
        
    }
}
