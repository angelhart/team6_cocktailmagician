using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.Providers.Contracts;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace CM.Web.Areas.BarCrawler.Controllers
{
    [Area("BarCrawler")]
    public class CommentsController : Controller
    {
        private readonly ICommentServices _commentServices;
        private readonly ICommentViewMapper _commentViewMapper;
        private readonly IToastNotification _toastNotification;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CommentsController(ICommentServices commentServices,
                                  ICommentViewMapper commentViewMapper,
                                  IToastNotification toastNotification,
                                  IDateTimeProvider dateTimeProvider)
        {
            this._commentServices = commentServices;
            this._commentViewMapper = commentViewMapper;
            this._toastNotification = toastNotification;
            this._dateTimeProvider = dateTimeProvider;
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CommentCocktail(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CommentedOn = _dateTimeProvider.GetDateTimeDateTimeOffset();
                    model.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var dto = _commentViewMapper.CreateCocktailCommentDTO(model);
                    dto = await _commentServices.AddCocktailCommentAsync(dto);

                    return RedirectToAction("Details", "Cocktails", new { Area = "", Id = model.EntityId });
                }
                catch (Exception ex)
                {
                    _toastNotification.AddErrorToastMessage(ex.Message);
                    return RedirectToAction("Details", "Cocktails", new { Area = "", Id = model.EntityId });
                }
            }

            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    _toastNotification.AddWarningToastMessage(error.ErrorMessage);
                }
            }
            return RedirectToAction("Details", "Cocktails", new { area = "", Id = model.EntityId });
        }
    }
}
