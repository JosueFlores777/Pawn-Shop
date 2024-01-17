using Microsoft.AspNetCore.Http;
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
            var viewModel = new ClientModel
            {
                LoggedInUserId = HttpContext.Session.GetInt32("userId") ?? 0
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(ClientModel usersModel)
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
            var viewModel = new ClientModel
            {
                LoggedInUserId = HttpContext.Session.GetInt32("userId") ?? 0
            };

            var oUser = usersDto.Get(idUser);

            viewModel.Id = oUser.Id;
            viewModel.IDClient = oUser.IDClient;
            viewModel.Name = oUser.Name;
            viewModel.LastName = oUser.LastName;
            viewModel.Email = oUser.Email;
            viewModel.Password = oUser.Password;
            viewModel.Phone = oUser.Phone;
            viewModel.Role = oUser.Role;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ClientModel usersModel)
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
        public IActionResult Delete(ClientModel usersModel)
        {
            var response = usersDto.Delete(usersModel.Id);

            if (response)
                return RedirectToAction("Listar");
            else
                return View();


        }
    }
}
