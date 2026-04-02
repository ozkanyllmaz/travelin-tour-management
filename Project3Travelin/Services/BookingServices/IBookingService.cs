using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Models.Enums;

namespace Project3Travelin.Services.BookingServices
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto createBookingDto);
        Task<List<ResultBookingDto>> GetAllBookingStatusPendingAsync();
        Task<List<ResultBookingDto>> GetPassengerByTourIdAsync(string id);
        Task<List<PendingBookingsDto>> GetPendingRezervasitionAsync();
        Task ChangeBookingStatusAsync(string id, BookingStatus status);
        Task<List<ResultAllBookingDto>> GetAllBookingAsync();
    }
}
