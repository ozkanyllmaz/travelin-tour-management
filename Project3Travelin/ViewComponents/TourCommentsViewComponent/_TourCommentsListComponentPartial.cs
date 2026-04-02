using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.ViewComponents.TourCommentsViewComponent
{
    public class _TourCommentsListComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _TourCommentsListComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {
            var comments = await _commentService.GetCommentsByTourIdAsync(tourId);
            return View(comments);
        }
    }
}
