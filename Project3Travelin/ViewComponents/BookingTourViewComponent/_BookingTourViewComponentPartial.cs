using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.BookingTourViewComponent
{
    public class _BookingTourViewComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _BookingTourViewComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _tourService.GetTourByIdAsync(id);
            return View(value);
        }
    }
}
