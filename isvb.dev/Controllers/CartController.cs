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
        public async Task<string> UpdateItem(int id, int quant)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var tempCartItemS = user.Cart.CartItems.FirstOrDefault(x => x.Product.ProductId == id);
            tempCartItemS.Quantity = quant;
            db.SaveChanges();
            return "Product updated!!";
          
        }

        public async Task<string> DeleteItem(int id)
        {
            var myUser = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var cartItems = myUser.Cart.CartItems.Where(x=>x.Cart.User.UserId==myUser.UserId).ToList();
            foreach (var item in cartItems)
            {
                if (item.Product.ProductId == id)
                    db.CartItems.Remove(item);
            }

            db.SaveChanges();
            return "Item deleted";

        }
        public async Task<int> CountRows() {
            User user= new User();
            if(db.Users.FirstOrDefault(x => x.Email == User.Identity.Name)!=null)
            {
                user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            }
            else
            {
                return 0;
            }            
            var cartItems = user.Cart.CartItems.ToList();
            var count = cartItems.Count;

            return count;
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