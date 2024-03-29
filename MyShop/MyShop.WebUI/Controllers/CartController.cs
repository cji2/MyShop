﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;

        public CartController(ICartService CartService)
        {
            this.cartService = CartService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var model = cartService.GetCartItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddToCart(string Id)
        {
            this.cartService.AddToCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string Id)
        {
            this.cartService.RemoveFromCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult CartSummary()
        {
            var cartSummary = cartService.GetCartSummary(this.HttpContext);

            return PartialView(cartSummary);
        }
    }
}