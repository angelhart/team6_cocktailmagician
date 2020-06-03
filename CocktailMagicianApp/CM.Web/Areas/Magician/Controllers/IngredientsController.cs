using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Services.Contracts;
using CM.Web.Areas.Magician.Models;
using CM.Web.Providers;
using CM.Web.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CM.Web.Areas.Magician.Controllers
{
    [Area("Magician")]
    //[Authorize(Roles = (""))] // TODO: add roles
    public class IngredientsController : Controller
    {
        private readonly IIngredientServices _ingredientServices;
        private readonly IIngredientViewMapper _ingredientViewMapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageProvider _storageProvider;

        public IngredientsController(IIngredientServices ingredientServices,
                                     IIngredientViewMapper ingredientViewMapper,
                                     IWebHostEnvironment webHostEnvironment,
                                     IStorageProvider storageProvider)
        {
            this._ingredientServices = ingredientServices;
            this._ingredientViewMapper = ingredientViewMapper;
            this._webHostEnvironment = webHostEnvironment;
            this._storageProvider = storageProvider;
        }
        // GET: IngredientsController
        public ActionResult Index()
        {
            return View();
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
                var sortOrder = Request.Form["order[0][dir]"].FirstOrDefault(x => x.Equals("desc"));

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int pageNumber = start != null ? (1 + ((int)Math.Ceiling(Convert.ToDouble(start) / pageSize))) : 0;
                int recordsTotal = await _ingredientServices.CountAllIngredientsAsync();

                var dtos = await _ingredientServices.PageIngredientsAsync(searchString, pageNumber, pageSize);
                var vms = dtos.Select(d => _ingredientViewMapper.CreateIngredientViewModel(d)).ToList();

                var recordsFiltered = dtos.SourceItems;

                var output = DataTablesProvider<IngredientViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                    // create path
                    var targetFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images\\Ingredients");
                    var fileExtension = Path.GetExtension(model.Image.FileName);
                    var newFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")
                                         + "_"
                                         + model.Name
                                         + fileExtension;
                    
                    model.ImagePath = Path.Combine(targetFolder, newFileName);

                    // create ingredient
                    var dto = _ingredientViewMapper.CreateIngredientDTO(model);
                    dto = await _ingredientServices.CreateIngredientAsync(dto);
                    var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                    // upload image after ingredient added
                    await _storageProvider.StoreImageAsync(model.ImagePath, model.Image);

                    return RedirectToAction(nameof(Index));
                    //return Created(nameof(Create), vm);
                    //return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            else
            {
                return View(model);
            }
        }

        // GET: IngredientsController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var dto = await _ingredientServices.GetIngredientDetailsAsync(id);

                var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: IngredientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind]IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = _ingredientViewMapper.CreateIngredientDTO(model);
                    dto = await _ingredientServices.UpdateIngredientAsync(dto);

                    var vm = _ingredientViewMapper.CreateIngredientViewModel(dto);

                    return RedirectToAction(nameof(Details), new { id = vm.Id } ) ;
                }
                catch
                {
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
            catch
            {
                return View();
            }
        }
    }
}
