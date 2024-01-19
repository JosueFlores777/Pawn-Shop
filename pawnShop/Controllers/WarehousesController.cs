using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;

namespace pawnShop.Controllers
{
    public class WarehousesController : Controller
    {
        WarehousesDto wearehouses = new WarehousesDto();

        #region Ware
        public IActionResult List(string search)
        {
            var reponse = wearehouses.List(search);
            return View(reponse);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(WarehousesModel warehousesModel)
        {
            var reponse = wearehouses.Save(warehousesModel);

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (reponse)
                return RedirectToAction("List");
            else
                return View();

        }



        public IActionResult Edit(int id)
        {

            var reponse = wearehouses.Get(id);
            return View(reponse);
        }

        [HttpPost]
        public IActionResult Edit(WarehousesModel warehousesModel)
        {


            var reponse = wearehouses.Edit(warehousesModel);

            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }



        public IActionResult Delete(int id)
        {
            var response = wearehouses.Get(id);
            return View(response);
        }

        [HttpPost]
        public IActionResult Delete(WarehousesModel warehousesModel)
        {
            var reponse = wearehouses.Delete(warehousesModel);
            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }

        #endregion

        #region Sha

        public IActionResult ListShabvle(string search)
        {
            var reponse = wearehouses.ListShabvle(search);
            return View(reponse);
        }

        public IActionResult EditShabvle(int id)
        {
            WarehousesDtoa warehousesDto = new WarehousesDtoa();
            var warehouseInfoList = warehousesDto.GetWarehouseInfo();
            ViewBag.WarehouseInfoList = warehouseInfoList;
            
            var response = wearehouses.GetShabvle(id);

            if (response != null)
            {
                return View(response);
            }
            else
            {
               
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult EditShabvle(ShelvesModel shelvesModel)
        {
            var response = wearehouses.EditShabvle(shelvesModel);

            if (response)
                return RedirectToAction("ListShabvle");
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to edit shelf. Please check your inputs.");
                return View(shelvesModel);
            }
        }

        public IActionResult SaveShavle()
        {

            WarehousesDtoa warehousesDto = new WarehousesDtoa();
            var warehouseInfoList = warehousesDto.GetWarehouseInfo();
            ViewBag.WarehouseInfoList = warehouseInfoList;
            return View();


        }

        [HttpPost]
        public IActionResult SaveShavle(ShelvesModel shelvesModel)
        {
            WarehousesDtoa warehousesDto = new WarehousesDtoa();

            if (ModelState.IsValid)
            {

                var response = wearehouses.SaveShavle(shelvesModel);

                if (response)
                {

                    return RedirectToAction("ListShabvle");
                }

            }


            var warehouseInfoList = warehousesDto.GetWarehouseInfo();
            ViewBag.WarehouseInfoList = warehouseInfoList;

            return View("ListShabvle", shelvesModel);
        }

        public IActionResult DeleteShav(int id)
        {
            var response = wearehouses.GetShabvle(id);
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeleteShav(ShelvesModel shelvesModel) { 
            var reponse = wearehouses.DeleteShavl(shelvesModel.Id);

            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }


        #endregion
    }
}
