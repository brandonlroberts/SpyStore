﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpyStore.Hol.Dal.Repos.Interfaces;
using SpyStore.Hol.Models.Entities;
using SpyStore.Hol.Models.ViewModels;
using SpyStore.Hol.Mvc.Controllers.Base;

namespace SpyStore.Hol.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class OrdersController : BaseController
    {
        private readonly IOrderRepo _orderRepo;
        public OrdersController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Order History";
            ViewBag.Header = "Order History";
            _orderRepo.Context.CustomerId = ViewBag.CustomerId;
            IList<Order> orders = _orderRepo.GetOrderHistory().ToList();
            return View(orders);
        }

        [HttpGet("{orderId}")]
        public IActionResult Details(int orderId)
        {
            ViewBag.Title = "Order Details";
            ViewBag.Header = "Order Details";
            OrderWithDetailsAndProductInfo orderDetails = _orderRepo.GetOneWithDetails(orderId);
            if (orderDetails == null) return NotFound();
            return View(orderDetails);
        }
    }
}