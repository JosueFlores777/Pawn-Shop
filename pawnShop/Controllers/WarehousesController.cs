﻿using Microsoft.AspNetCore.Mvc;
using pawnShop.Data;
using pawnShop.Models;

namespace pawnShop.Controllers
{
    public class WarehousesController : Controller
    {
        WarehousesDto wearehouses = new WarehousesDto();
        public IActionResult List(string search)
        {
            var reponse = wearehouses.List(search);
            return View(reponse);
        }

        public IActionResult Save() {
            return View();
        }

        [HttpPost]
        public IActionResult Save(WarehousesModel warehousesModel) {
            var reponse = wearehouses.Save(warehousesModel);

            if (!ModelState.IsValid)
            {
                return View();
            }


            if(reponse)
                return RedirectToAction("List");
            else    
                return View();

        }



        /**/

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
                  
                    return RedirectToAction("SaveShavle");
                }
    
            }

          
            var warehouseInfoList = warehousesDto.GetWarehouseInfo();
            ViewBag.WarehouseInfoList = warehouseInfoList;

            return View("SaveShavle", shelvesModel);
        }


        public IActionResult Edit(int id) {
            var reponse = wearehouses.Get(id);
            return View(reponse); 
        }

        [HttpPost]
        public IActionResult Edit(WarehousesModel warehousesModel) { 
            var reponse = wearehouses.Edit(warehousesModel);

            if (!ModelState.IsValid) { 
                return View();
            }

            if (reponse)
                return RedirectToAction("List");
            else return View();
        }

        public IActionResult Delete(int id)
        {
            var response = wearehouses.Get(id);
            return View(response);  
        }

        [HttpPost]
        public IActionResult Delete(WarehousesModel warehousesModel) {
            var reponse = wearehouses.Delete(warehousesModel);
            if (reponse)
                return RedirectToAction("List");
            else
                return View();
        }
    }
}