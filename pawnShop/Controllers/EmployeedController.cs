using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;

namespace pawnShop.Controllers
{
    public class EmployeedController : Controller
    {
        EmployeedDto employeeDto = new EmployeedDto();

        public IActionResult List(string search)
        {
            var olist = employeeDto.List(search);

            return View(olist);
        }

        public IActionResult Save() { 
            return View();
        }

        [HttpPost]
        public IActionResult Save(EmployeeModel employeeModel)
        {
            var response = employeeDto.Save(employeeModel);
            if (!ModelState.IsValid) { 
                View();
            }


            if (response)
                return RedirectToAction("List");
            else
                return View();

        }
    }
}
