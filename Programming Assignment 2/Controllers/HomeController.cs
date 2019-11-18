using Programming_Assignment_2.Models;
using Programming_Assignment_2.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Programming_Assignment_2.Controllers
{
    public class HomeController : Controller
    {
        MyDatabaseEntities myDatabaseEntities = new MyDatabaseEntities();

        //Return Index
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }
        //===============================Add Product to cart======================//
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                var product = myDatabaseEntities.Product.Find(productId);
                List<Item> cart = new List<Item>();
                
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = myDatabaseEntities.Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    Price = product.Price,
                });
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }
        //return About Page
        public ActionResult About()
        {
            return View();
        }
        //return About contact Incomplete
        
        public ActionResult Contact(Contact contact)
        {
            if (contact.Name == null || contact.Message == null || contact.Email == null)
            {
                return View();
            }
            return View();
        }

    }
    //===========================Contact Class======================================//
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}