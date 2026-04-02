using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.BookingServices;

namespace Project3Travelin.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public AdminBookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> BookingList()
        {
            var bookings = await _bookingService.GetAllBookingAsync();
            return View(bookings);
        }

        public async Task<IActionResult> ApproveBooking(string id)
        {
            await _bookingService.ChangeBookingStatusAsync(id, Models.Enums.BookingStatus.Onaylandı);
            return RedirectToAction("BookingList");
        }

        public async Task<IActionResult> RejectBooking(string id)
        {
            await _bookingService.ChangeBookingStatusAsync(id, Models.Enums.BookingStatus.Reddedildi);
            return RedirectToAction("BookingList");
        }
    }
}
