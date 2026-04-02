using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            await _commentService.CreateCommentAsync(createCommentDto);
            return RedirectToAction("TourDetail", "Tour", new { id = createCommentDto.TourId.ToString() });
        }

    }
}
