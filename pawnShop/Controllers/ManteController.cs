using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;

namespace pawnShop.Controllers
{
    public class ManteController : Controller
    {
        UsersDto usersDto = new UsersDto();
        public IActionResult Listar(string search)
        {
            var oList = usersDto.List(search);

            return View(oList);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(UsersModel usersModel)
        {
            var response = usersDto.Save(usersModel);

            if (!ModelState.IsValid)
            {
               return View(); 
            }

            if (response)
                return RedirectToAction("Listar");
            else 
                return View();

        }


        public IActionResult Edit(int idUser)
        {
            var oUser = usersDto.Get(idUser);
            return View(oUser);
        }

        [HttpPost]
        public IActionResult Edit(UsersModel usersModel)
        {
            var response = usersDto.Edit(usersModel);

            if (!ModelState.IsValid)
                return View();

            if (response)
                return RedirectToAction("Listar");
            else
                return View();
           
            
        }

        public IActionResult Delete(int idUser)
        {
            var oUser = usersDto.Get(idUser);
            return View(oUser);
        }

        [HttpPost ]
        public IActionResult Delete(UsersModel usersModel)
        {
            var response = usersDto.Delete(usersModel.Id);

            if (response)
                return RedirectToAction("Listar");
            else
                return View();


        }
    }
}
