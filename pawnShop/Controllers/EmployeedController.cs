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


        public IActionResult List(string search, int page = 1, int pageSize = 10)
        {
            var employeeList = employeeDto.List(search);
            var paginatedList = new Paginated<EmployeeModel>(employeeList, employeeList.Count, page, pageSize);

            return View(paginatedList);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(EmployeeModel employeeModel)
        {
            var response = employeeDto.Save(employeeModel);
            if (!ModelState.IsValid)
            {
                View();
            }

            if (response)
                return RedirectToAction("List");
            else
                return View();

        }


        public IActionResult Edit(int id)
        {
            var employec = employeeDto.Get(id);
            return View(employec);
        }


        [HttpPost]
        public IActionResult Edit(EmployeeModel employeeModel)
        {
            var reponse = employeeDto.Edit(employeeModel);
            if (!ModelState.IsValid)
                return View();

            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }


        public IActionResult Delete(int id)
        {
            var response = employeeDto.Get(id);
            return View(response);
        }

        [HttpPost]
        public IActionResult Delete(EmployeeModel model) {
            var response = employeeDto.Delete(model.Id);
            if(response)
                return RedirectToAction("List");
            else
                return View( );
        }
    }
}