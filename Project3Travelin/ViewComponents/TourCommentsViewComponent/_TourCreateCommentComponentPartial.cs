using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Entities;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.ViewComponents.TourCommentsViewComponent
{
    public class _TourCreateCommentComponentPartial:ViewComponent
    {
        private ICommentService _commentService;

        public _TourCreateCommentComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tourId)
        {
            var createCommentModel = new CreateCommentDto { TourId = tourId };
            return View(createCommentModel);
        }
    }
}
