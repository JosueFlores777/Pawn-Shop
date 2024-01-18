using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;
using pawnShop.Validated;

namespace pawnShop.Controllers
{
    [ValidarSesion]
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

        
        public IActionResult Edit(int id) {
            var employec = employeeDto.Get(id);
            return View(employec);
        }


        [HttpPost]
        public IActionResult Edit(EmployeeModel employeeModel) {
            var reponse= employeeDto.Edit(employeeModel);
            if (!ModelState.IsValid)
                return View();
            
            if(reponse)
                return RedirectToAction("List");
            else
                return View();
        }
    }
}
