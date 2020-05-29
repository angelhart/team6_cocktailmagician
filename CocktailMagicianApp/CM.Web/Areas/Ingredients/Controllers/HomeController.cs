using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Services.Contracts;
using CM.Web.Areas.Ingredients.Models;
using CM.Web.Providers;
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
            try
            {
                var drawString = HttpContext.Request.Form["draw"].FirstOrDefault();
                int draw = drawString != null ? Convert.ToInt32(drawString) : 0;
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchString = Request.Form["search[value]"].FirstOrDefault();
                var sortBy = Request.Form
                                ["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                                .FirstOrDefault();
                var sortOrder = Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int pageNumber = start != null ? (1 + ((int)Math.Ceiling(Convert.ToDouble(start) / pageSize))) : 0;
                int recordsTotal = await _ingredientServices.CountAllIngredientsAsync();

                var dtos = await _ingredientServices.PageIngredientsAsync(searchString, pageNumber, pageSize);
                var vms = dtos.Select(d => _ingredientViewMapper.CreateIngredientViewModel(d)).ToList();

                var recordsFiltered = dtos.SourceItems;

                var output = AjaxProvider<IngredientViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

                return Json(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public ActionResult Create(string name, string ImagePath)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ArgumentException();

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
