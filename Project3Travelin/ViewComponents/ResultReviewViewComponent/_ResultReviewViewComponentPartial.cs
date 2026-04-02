using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.ViewComponents.ResultReviewViewComponent
{
    public class _ResultReviewViewComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _ResultReviewViewComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _commentService.GetRatingScoreByTourIdAsync(id);
            ViewBag.Rating = value.AverageScore;
            ViewBag.Guide = value.GuideScore;
            ViewBag.Program = value.ProgramScore;
            ViewBag.ValueForMoney = value.ValueForMoney;
            ViewBag.Organization = value.OrganizationScore;
            ViewBag.Service = value.ServiceScore;
            
            return View();
        }
    }
}
