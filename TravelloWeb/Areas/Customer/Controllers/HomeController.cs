using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers;
[Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMain _main;

        public HomeController(ILogger<HomeController> logger, IMain main)
        {
            _logger = logger;
            _main = main;
        }

        public IActionResult Index()
        {
        IEnumerable<Travel> travelList = _main.Travel.GetAll(includeProperties: "Guide");

            return View(travelList);
        }


     public IActionResult Details(int travelId)
    {
        ShoppingCart cartObj = new()
        {

            Ticket = 1,
            TravelId = travelId,
            Travel = _main.Travel.GetFirstOrDefault(u => u.Id == travelId, includeProperties: "Guide")

        };
        return View(cartObj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {

        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.ApplicationUserId = claim.Value;

        ShoppingCart cartFromDb = _main.ShoppingCart.GetFirstOrDefault(u=> u.ApplicationUserId==claim.Value &&
                                                                       u.TravelId==shoppingCart.TravelId);

        if (cartFromDb == null) {
            _main.ShoppingCart.Add(shoppingCart);
        }
        else
        {
            _main.ShoppingCart.IncrementTicket(cartFromDb, shoppingCart.Ticket);
        }

        
        _main.Save();

        return RedirectToAction("Index");
    }




    public IActionResult Privacy()
        {
            return View();
        }

    
    }
