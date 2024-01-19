using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Validated;

namespace pawnShop.Controllers
{
    [ValidarSesion]
    public class TransaactionsController : Controller
    {
        TransactionDto transac =new TransactionDto();
        public IActionResult List(string search)
        {
            var response = transac.List(search);
            return View(response);
        }
    }
}
