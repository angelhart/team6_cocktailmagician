using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Services.Contracts;
using CM.Web.Areas.Ingredients.Models;
using CM.Web.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CM.Web.Areas.Ingredients.Controllers
{
    [Area("Ingredients")]
    //[Authorize(Roles = (""))] // TODO: add roles
    public class HomeController : Controller
    {
        private readonly IIngredientServices _ingredientServices;
        private readonly IIngredientViewMapper _ingredientViewMapper;

        public HomeController(IIngredientServices ingredientServices,
                                     IIngredientViewMapper ingredientViewMapper)
        {
            this._ingredientServices = ingredientServices;
            this._ingredientViewMapper = ingredientViewMapper;
        }
        // GET: IngredientsController
        public async Task<ActionResult> Index(IngredientIndexViewModel vm)
        {
            try
            {
                var dtos = await _ingredientServices.PageIngredientsAsync();
                var output = new IngredientIndexViewModel
                {
                    PageNumber = dtos.PageNumber,
                    TotalPages = dtos.TotalPages,
                    PageIngredients = dtos.Select(i => _ingredientViewMapper.CreateIngredientViewModel(i)).ToList()
                };
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> IndexTable()
        {
            return Json(null);
        }

        // GET: IngredientsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IngredientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
