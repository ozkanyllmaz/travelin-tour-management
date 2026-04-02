using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Dtos.TourDtos;

namespace Project3Travelin.Entities
{
    public class TourDetailViewModel
    {
        public GetTourByIdDto TourDetail { get; set; }
        public CreateCommentDto CreateComment { get; set; }
    }
}
