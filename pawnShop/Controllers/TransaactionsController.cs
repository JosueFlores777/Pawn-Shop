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


        public IActionResult Edit(int id)
        {

            var transaction = transac.Get(id);
            transaction.ShelvesList = transac.ListShel();
            return View(transaction);
        }
        [HttpPost]
        public IActionResult Edit(TransactionsModel transactionsModel)
        {
            var transaction = transac.Edit(transactionsModel);
            
            if(transaction)
                return RedirectToAction("List");
            else 
                return View();

        }

        public IActionResult Delete(int id)
        {

            var transaction = transac.Get(id);
            transaction.ShelvesList = transac.ListShel();
            return View(transaction);
        }
        [HttpPost]
        public IActionResult Delete(TransactionsModel transactionsModel)
        {
            var transaction = transac.Delete(transactionsModel);

            if (transaction)
                return RedirectToAction("List");
            else
                return View();

        }

    }
}
