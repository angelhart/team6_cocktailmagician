using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AspNetCore;
using CM.DTOs;
using CM.Services.Contracts;
using CM.Web.Areas.Magician.Models;
using CM.Web.Providers;
using CM.Web.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace CM.Web.Areas.Magician.Controllers
{
    [Area("Magician")]
    //[Authorize(Roles = (""))] // TODO: add roles
    public class IngredientsController : Controller
    {
        private const string ROOTSTORAGE = "\\images\\Ingredients";

        private readonly IIngredientServices _ingredientServices;
        private readonly IIngredientViewMapper _ingredientViewMapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageProvider _storageProvider;
        private readonly IToastNotification _toastNotification;

        public IngredientsController(IIngredientServices ingredientServices,
                                     IIngredientViewMapper ingredientViewMapper,
                                     IWebHostEnvironment webHostEnvironment,
                                     IStorageProvider storageProvider,
                                     IToastNotification toastNotification)
        {
            this._ingredientServices = ingredientServices;
            this._ingredientViewMapper = ingredientViewMapper;
            this._webHostEnvironment = webHostEnvironment;
            this._storageProvider = storageProvider;
            this._toastNotification = toastNotification;
        }
        // GET: IngredientsController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
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

                var dtos = await _ingredientServices.PageIngredientsAsync(searchString, sortOrder, pageNumber, pageSize);
                var vms = dtos.Select(d => _ingredientViewMapper.CreateIngredientViewModel(d)).ToList();

                var recordsFiltered = dtos.SourceItems;

                var role = User.IsInRole("Magician") ? "Magician" : "";

                var output = DataTablesProvider<IngredientViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

                return Ok(output);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: IngredientsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var dto = await _ingredientServices.GetIngredientDetailsAsync(id);

            var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

            return View(vm);
        }

        // GET: IngredientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ImagePath = ROOTSTORAGE + "\\DefaultIngredients.jpg";
                    if (model.Image != null)
                    {
                        model.ImagePath = _storageProvider.GenerateRelativePath(ROOTSTORAGE, model.Image.FileName, model.Name);
                    }

                    // create ingredient
                    var dto = _ingredientViewMapper.CreateIngredientDTO(model);
                    dto = await _ingredientServices.CreateIngredientAsync(dto);
                    var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                    // upload image after ingredient added
                    if (model.Image != null)
                        await _storageProvider.StoreImageAsync(model.ImagePath, model.Image);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _toastNotification.AddErrorToastMessage(ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }

            else
            {
                return View(model);
            }
        }

        // GET: IngredientsController/Edit/5
        public async Task<ActionResult> Edit(IngredientViewModel model)
        {
            try
            {
                var dto = await _ingredientServices.GetIngredientDetailsAsync(model.Id);

                var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                return View(vm);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: IngredientsController/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditConfirmed(IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var oldImagePath = model.ImagePath;
                    if (model.Image != null)
                    {
                        model.ImagePath = _storageProvider.GenerateRelativePath(ROOTSTORAGE, model.Image.FileName, model.Name);
                    }

                    var dto = _ingredientViewMapper.CreateIngredientDTO(model);
                    dto = await _ingredientServices.UpdateIngredientAsync(dto);

                    var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                    if (model.Image != null)
                    {
                        _storageProvider.DeleteImage(oldImagePath);
                        await _storageProvider.StoreImageAsync(model.ImagePath, model.Image);
                    }

                    return RedirectToAction(nameof(Details), new { id = vm.Id });
                }
                catch (Exception ex)
                {
                    _toastNotification.AddErrorToastMessage(ex.Message);
                    return View();
                }
            }

            else
            {
                return View(model);
            }
        }

        // GET: IngredientsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var dto = await _ingredientServices.GetIngredientDetailsAsync(id);

            var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

            return View(vm);
        }

        // POST: IngredientsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var dto = await _ingredientServices.DeleteIngredientAsync(id);
                var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                // Delete image after deleting its ingredient
                _storageProvider.DeleteImage(dto.ImagePath);

                return Ok(vm);
                //return RedirectToAction(nameof(IndexTable));
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(id);
            }
        }
    }
}
