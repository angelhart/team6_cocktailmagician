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

namespace CM.Web.Areas.Magician.Controllers
{
    [Area("Magician")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly ICocktailViewMapper _cocktailViewMapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageProvider _storageProvider;
        private readonly IRatingServices _ratingServices;
        private readonly IToastNotification _toastNotification;
        private readonly ICommentServices _commentServices;

        public CocktailsController(ICocktailServices cocktailServices,
                                   ICocktailMapper cocktailMapper,
                                   ICocktailViewMapper cocktailViewMapper,
                                   IWebHostEnvironment webHostEnvironment,
                                   IStorageProvider storageProvider,
                                   IRatingServices ratingServices,
                                   IToastNotification toastNotification,
                                   ICommentServices commentServices)
        {
            this._cocktailServices = cocktailServices;
            this._cocktailMapper = cocktailMapper;
            this._cocktailViewMapper = cocktailViewMapper;
            this._webHostEnvironment = webHostEnvironment;
            this._storageProvider = storageProvider;
            this._ratingServices = ratingServices;
            this._toastNotification = toastNotification;
            this._commentServices = commentServices;
        }

        // GET: Magician/Cocktails
        public async Task<IActionResult> Index()
        {
            var dtos = await _cocktailServices.PageCocktailsAsync();
            var vms = dtos.Select(c => _cocktailViewMapper.CreateCocktailViewModel(c));

            return View(vms);
        }

        // GET: Magician/Cocktails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // GET: Magician/Cocktails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magician/Cocktails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Recipe,ImagePath,IsUnlisted,AverageRating")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                cocktail.Id = Guid.NewGuid();
                _context.Add(cocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        // GET: Magician/Cocktails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }
            return View(cocktail);
        }

        // POST: Magician/Cocktails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Recipe,ImagePath,IsUnlisted,AverageRating")] Cocktail cocktail)
        {
            if (id != cocktail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocktail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocktailExists(cocktail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        // GET: Magician/Cocktails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // POST: Magician/Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocktailExists(Guid id)
        {
            return _context.Cocktails.Any(e => e.Id == id);
        }
    }
}
