using Project3Travelin.Entities;

namespace Project3Travelin.Dtos.TourDtos
{
    public class UpdateTourDto
    {
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public DateTime TourDate { get; set; }
        public string DayNight { get; set; }
        public List<TourProgram> TourPrograms { get; set; }
        public List<Comment> TourComments { get; set; }
        public string GeneratedImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
