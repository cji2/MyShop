﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.WebUI.Tests.Mocks;
using MyShop.WebUI.Controllers;
using MyShop.Services;
using System.Linq;
using System.Web.Mvc;
using System.Security.Principal;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CanAddCartItem()
        {
            //setup
            IRepository<Cart> carts = new MockContext<Cart>();
            IRepository<Product> products = new MockContext<Product>();
            //IRepository<Order> orders = new MockContext<Order>();

            var httpContext = new MockHttpContext();

            ICartService cartService = new CartService(products, carts);
            //IOrderService orderService = new OrderService(orders);

            var controller = new CartController(cartService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //CartService.AddToCart(httpContext, "1");
            controller.AddToCart("1");

            Cart cart = carts.Collection().FirstOrDefault();


            //Assert
            Assert.IsNotNull(cart);
            Assert.AreEqual(1, cart.CartItems.Count);
            Assert.AreEqual("1", cart.CartItems.ToList().FirstOrDefault().ProductId);
        }
        
        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Cart> carts = new MockContext<Cart>();
            IRepository<Product> products = new MockContext<Product>();
            //IRepository<Order> orders = new MockContext<Order>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Cart cart = new Cart();
            cart.CartItems.Add(new CartItem() { ProductId = "1", Quantity = 2 });
            cart.CartItems.Add(new CartItem() { ProductId = "2", Quantity = 1 });
            carts.Insert(cart);

            ICartService cartService = new CartService(products, carts);
            //IOrderService orderService = new OrderService(orders);
            var controller = new CartController(cartService);

            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = cart.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.CartSummary() as PartialViewResult;
            var cartSummary = (CartSummaryViewModel) result.ViewData.Model;

            Assert.AreEqual(3, cartSummary.CartCount);
            Assert.AreEqual(25.00m, cartSummary.CartTotal);


        }
        /*
        [TestMethod]
        public void CanCheckoutAndCreateOrder()
        {
            IRepository<Customer> customers = new MockContext<Customer>();
            IRepository<Product> products = new MockContext<Product>();
            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            IRepository<Cart> baskets = new MockContext<Cart>();
            Cart basket = new Cart();
            basket.CartItems.Add(new CartItem() { ProductId = "1", Quanity = 2, CartId = cart.Id });
            basket.CartItems.Add(new CartItem() { ProductId = "1", Quanity = 1, CartId = cart.Id });

            baskets.Insert(basket);

            ICartService basketService = new CartService(products, baskets);

            //IRepository<Order> orders = new MockContext<Order>();
            //IOrderService orderService = new OrderService(orders);

            customers.Insert(new Customer() { Id = "1", Email = "brett.hargreaves@gmail.com", ZipCode = "90210" });

            //IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("brett.hargreaves@gmail.com", "Forms"), null);


            var controller = new CartController(cartService, orderService, customers);
            var httpContext = new MockHttpContext();
            httpContext.User = FakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //Order order = new Order();
            controller.Checkout(order);

            //assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            //Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);

        } */
    }
}
