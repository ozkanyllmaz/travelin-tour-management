using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.ViewComponents.BookingCommentViewComponent
{
    public class _BookingCommentViewComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _BookingCommentViewComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var comments = await _commentService.GetAllCommentAsync();
            return View(comments);
        }
    }
}
