using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.BookingServices;

namespace Project3Travelin.ViewComponents.BookingStatusPendingViewComponent
{
    public class _BookingStatusPendingViewComponentPartial : ViewComponent
    {
        private readonly IBookingService _bookingService;

        public _BookingStatusPendingViewComponentPartial(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _bookingService.GetAllBookingStatusPendingAsync();
            return View(values);
        }
    }
}
