using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebCuoiKhoa.Models;
namespace ProjectWebCuoiKhoa.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBQLBHEntities db = new DBQLBHEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCheckout()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToCart(int? id)
        {
            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        //ham lay tong san pham va hien len partialView
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
                ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }

        public PartialViewResult BagCartCustomer()
        {
            
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCartCustomer");
        }

        public ActionResult CheckOut(FormCollection form)
        {
            int x= int.Parse(form["CodeCus"]);
            AdminUser temp = db.AdminUsers.Where(m => m.ID == x).FirstOrDefault();
            if (temp != null)
            {
            try
            {
                Customer _customer = new Customer();
                _customer.IDCus= int.Parse(form["CodeCus"]);
                _customer.NameCus = form["NameCus"];
                _customer.PhoneCus = form["PhoneCus"];
                _customer.EmailCus = form["EmailCus"];
                db.Customers.Add(_customer);
                Cart cart = Session["Cart"] as Cart;
                OrderPro order = new OrderPro(); //Bảng hóa đơn sản phẩm
                order.DateOrder = DateTime.Now;
                order.AddressDeliverry = form["AddressDelivery"];
                order.IDCus = int.Parse(form["CodeCus"]);
                db.OrderProes.Add(order);
                foreach (var item in cart.Items)
                {
                    OrderDetail order_detail = new OrderDetail(); //Lưu dòng sản phẩm vào bảng chi tiết hóa đơn
                    order_detail.IDOrder = order.ID;
                    order_detail.IDProduct = item._product.ProductID;
                    order_detail.UnitPrice = (double)item._product.Price;
                    order_detail.Quantity = item._quantity;
                    db.OrderDetails.Add(order_detail);               
                }             
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("error CheckOut");

            }
            }
            else
            {
                return Content("error CheckOut");
            }
            
        }
        
        public ActionResult CheckOut_Success()
        {
            return View();
        }


        //customer
        public ActionResult ShowCartCustomer()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public ActionResult AddToCartCustomer(int? id)
        {
            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCartCustomer", "ShoppingCart");
        }
        public ActionResult ShowCheckoutCustomer()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public ActionResult RemoveCartCustomer(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            if (cart == null)
            {
                return RedirectToAction("EmptyCart");
            }
            return RedirectToAction("ShowCartCustomer", "ShoppingCart");
        }
        public ActionResult Update_Cart_Quantity_Customer(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCartCustomer", "ShoppingCart");
        }
    }
}