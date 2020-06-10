using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.DTOs.Mappers.Contracts;
using Microsoft.AspNetCore.Hosting;
using CM.Web.Providers.Contracts;
using NToastNotify;
using CM.Web.Models;
using CM.DTOs;
using CM.Web.Providers;
using CM.Web.Areas.Magician.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CM.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private const string ROOTSTORAGE = "\\images\\Cocktails";

        private readonly ICocktailServices _cocktailServices;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly ICocktailViewMapper _cocktailViewMapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageProvider _storageProvider;
        private readonly IRatingServices _ratingServices;
        private readonly IToastNotification _toastNotification;
        private readonly ICommentServices _commentServices;
        private readonly IIngredientServices _ingredientServices;
        private readonly IIngredientViewMapper _ingredientViewMapper;

        public CocktailsController(ICocktailServices cocktailServices,
                                   ICocktailMapper cocktailMapper,
                                   ICocktailViewMapper cocktailViewMapper,
                                   IWebHostEnvironment webHostEnvironment,
                                   IStorageProvider storageProvider,
                                   IRatingServices ratingServices,
                                   IToastNotification toastNotification,
                                   ICommentServices commentServices,
                                   IIngredientServices ingredientServices,
                                   IIngredientViewMapper ingredientViewMapper)
        {
            this._cocktailServices = cocktailServices;
            this._cocktailMapper = cocktailMapper;
            this._cocktailViewMapper = cocktailViewMapper;
            this._webHostEnvironment = webHostEnvironment;
            this._storageProvider = storageProvider;
            this._ratingServices = ratingServices;
            this._toastNotification = toastNotification;
            this._commentServices = commentServices;
            this._ingredientServices = ingredientServices;
            this._ingredientViewMapper = ingredientViewMapper;
        }

        // GET: Magician/Cocktails
        public IActionResult Index()
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
                var sortOrder = Request.Form["order[0][dir]"].FirstOrDefault(x => x.Contains("desc"));

                // TODO: finalise implementation of rating search
                var minRating = RatingParser.ParseRating(Request.Form["minRating"].FirstOrDefault());
                var maxRating = RatingParser.ParseRating(Request.Form["maxRating"].FirstOrDefault());

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int pageNumber = start != null ? 1 + (int)Math.Ceiling(Convert.ToDouble(start) / pageSize) : 0;
                int recordsTotal = await _cocktailServices.CountAllCocktailsAsync();

                var permission = User.IsInRole("Magician");

                var dtos = await _cocktailServices.PageCocktailsAsync(searchString, sortBy, sortOrder, pageNumber,
                                                                      pageSize, permission, minRating, maxRating);
                var vms = dtos.Select(d => _cocktailViewMapper.CreateCocktailViewModel(d)).ToList();

                var recordsFiltered = dtos.SourceItems;

                var output = DataTablesProvider<CocktailViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

                return Ok(output);
            }
            catch (ArgumentException ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Magician/Cocktails/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var permission = User.IsInRole("Magician");

                var dto = await _cocktailServices.GetCocktailDetailsAsync(id, isAdmin: permission);

                var vm = _cocktailViewMapper.CreateCocktailViewModel(dto);

                // TODO: Comments

                return View(vm);
            }
            catch (KeyNotFoundException ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (UnauthorizedAccessException ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return NotFound();
            }
        }
    }
}
