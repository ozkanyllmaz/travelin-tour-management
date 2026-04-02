using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.Controllers
{
    public class AdminCommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public AdminCommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> CommentList()
        {
            var values = await _commentService.GetAllCommentAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeCommentStatus(string id)
        {
            await _commentService.DeleteCommentAsync(id);

            return RedirectToAction("CommentList");
        }
    }
}
