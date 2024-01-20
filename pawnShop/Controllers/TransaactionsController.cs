using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;
using pawnShop.Validated;

namespace pawnShop.Controllers
{
    [ValidarSesion]
    public class TransaactionsController : Controller
    {
        TransactionDto transac = new TransactionDto();
        public IActionResult List(string search)
        {
            var response = transac.List(search);
            return View(response);
        }


        public IActionResult Save()
        {

            var transactionsModel = new TransactionsModel();
            transactionsModel.TransactionTypes = transac.ListType();
            transactionsModel.ShelvesList = transac.ListShel();
            return View(transactionsModel);
        }

        [HttpPost]
        public IActionResult Save(TransactionsModel transactionsModel)
        {
            var reponse = transac.Save(transactionsModel);

            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }
    }
}
