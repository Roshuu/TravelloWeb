using Travello.DataAccess.Repository.IRepository;
using Travello.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Travello.Models.ViewModels;

namespace BulkyBookWeb.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize]
    public class CartController : Controller
    {
        private readonly IMain _main;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IMain main) {  
           _main= main;
        }


        public IActionResult Index()
        {

	       var claimsIdentity = (ClaimsIdentity)User.Identity;
		   var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        ShoppingCartVM = new ShoppingCartVM()
        {
            ListCart = _main.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,includeProperties:"Travel")
        };

		return View(ShoppingCartVM);
      }


    
    }
