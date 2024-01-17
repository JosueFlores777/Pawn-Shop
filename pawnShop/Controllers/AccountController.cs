using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using pawnShop.Models;
using System;
using Microsoft.AspNetCore.Authentication;

namespace pawnShop.Controllers
{
    public class AccountController : Controller
    {
        static string Cadena = "Data Source=DESKTOP-GTQCNQG\\SQLEXPRESS;Initial Catalog=pawnshop; Integrated Security=True;";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Login(UserModel userModel)
        {
            using (SqlConnection conn = new SqlConnection(Cadena))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE email = @email OR Password = @Password", conn);
                cmd.Parameters.AddWithValue("email", userModel.Email);
                cmd.Parameters.AddWithValue("Password", userModel.Password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userModel.Id = Convert.ToInt32(reader["Id"]);
                        userModel.Name = Convert.ToString(reader["Name"]);
                        userModel.Role = Convert.ToString(reader["Role"]);

                       
                        HttpContext.Session.SetInt32("userId", userModel.Id);
                        HttpContext.Session.SetString("userName", userModel.Name);
                        HttpContext.Session.SetString("userRole", userModel.Role);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Menssaje"] = "User not found";
                        return View();
                    }
                }
            }
        }


        public ActionResult Logout()
        {
			HttpContext.Session.Clear();
			HttpContext.SignOutAsync(); 
			return RedirectToAction("Login", "Account");
		}
    }
}
