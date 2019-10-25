﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourStoreWeb.Models;
using YourStore.Library.Repo;

namespace YourStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepoBusLogic _repo;
        public HomeController(ILogger<HomeController> logger, IRepoBusLogic repo)
        {
            _logger = logger;
            _repo = repo;
        }
        //display all products for sale
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Home Controller: GetIndex");

            foreach (var xx in TempData.Keys)
            {
                if (xx != "Logged")
                {
                    TempData.Remove(xx);

                }
            }


            ViewStoreProductModel Model = new ViewStoreProductModel();
            IEnumerable<YourStore.Library.Store> st = _repo.GetAllStores();

            Model.Store = st.ToList();


            return View(Model);


        }
       /* [HttpPost]
        public IActionResult Index(ViewStoreProductModel Model)
        {

            IEnumerable<YourStore.Library.Store> st = _repo.GetAllStores();

            Model.Store = st.ToList();
            _logger.LogInformation("Home Controller: PostIndex");


            return RedirectToAction("StoreShop", "AddToCart ", Model);
        }*/



        [HttpGet]
        public IActionResult Login( )
        {
            LoginModel x = new LoginModel();
            _logger.LogInformation("Home Controller: GetLogin");
            return View(x);
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                LoginModel x = new LoginModel();
                var customers = _repo.GetAllCustomer();
                var c = customers.Where(a => a.Username == model.Username && a.Pass == model.Pass).FirstOrDefault();
                if (c != null)
                {
                    x.logged = true;
                }
                else
                {
                    x.logged = false;
                }
                _logger.LogInformation("Home Controller: PostLogin");

                TempData["Logged"] = c.Username;
                return View(x);
            }
            catch
            {
                model.errorMessage = "There is no user name or wrong pass";
                return View(model);
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
