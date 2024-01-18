using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using pawnShop.Models;
using System;
using Microsoft.AspNetCore.Authentication;
using pawnShop.DataDto;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace pawnShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDto _accountDto;
        public AccountController()
        {
            _accountDto = new AccountDto();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Login(UserModel userModel)
        {
            bool loginResult = _accountDto.Login(userModel, HttpContext);
     
            if (loginResult)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "User not found";
                return View();
            }
        }


        public ActionResult Logout()
        {
   
            HttpContext.Session.Clear(); 
            HttpContext.Session.CommitAsync();

            HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
