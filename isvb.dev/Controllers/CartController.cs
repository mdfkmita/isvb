﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using isvb.dev;
using System.Threading.Tasks;
using isvb.dev.Models;

namespace isvb.dev.Controllers
{
    [APIAuthorize(Roles = "Customer,Administrator,Owner")]
    public class CartController : Controller
    {
        private EFModelContainer db = new EFModelContainer();

        // GET: Carts
        public ActionResult MyCart()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var myCart = user.Cart;
            var items = myCart.CartItems;
            return View("MyCart",items);            
        }         
        public async Task<string> AddToCart(int? id,int quant)
        {
            //JavaScript should handle this
            if (id < 0 || id == null)
            {
                return "That product doesnt exist!";
            }
                       
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            
            var product = db.Products.Find(id);
            var tempCartItem = user.Cart.CartItems.FirstOrDefault(x => x.Product.ProductId == id);
            if (tempCartItem == null)
            {
                tempCartItem = new CartItem { Product = product, Quantity = quant };
            }
            else
            {
                tempCartItem.Quantity += quant;
            }            
            user.Cart.CartItems.Add(tempCartItem);
            db.SaveChanges();
            return "Product added!!";                           
        }     
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<string> UpdateItem(int id, int quant)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
