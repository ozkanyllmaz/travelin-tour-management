using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Dtos.EmailDtos;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Mapping;
using Project3Travelin.Models.Enums;
using Project3Travelin.Services.EmailServices;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Booking> _bookingCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IEmailService _emailService;
        private readonly ITourService _tourService;

        public BookingService(IMapper mapper, IDatabaseSettings _databaseSettings, IEmailService emailService, ITourService tourService)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(_databaseSettings.BookingCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
            _emailService = emailService;
            _tourService = tourService;
        }

        public async Task ChangeBookingStatusAsync(string id, BookingStatus status)
        {
            var value = await _bookingCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();

            var update = Builders<Booking>.Update.Set(x => x.BookingStatus, status);
            await _bookingCollection.UpdateOneAsync(x => x.BookingId == id, update);

            var tourInfo = await _tourService.GetTourByIdAsync(value.TourId);

            var emailRequest = new EmailRequestDto
            {
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Phone = value.Phone,
                PassengerCount = value.PassengerCount,
                BookingDate = value.BookingDate,
                Title = tourInfo.Title,
                BookingStatus = status

            };
            await _emailService.SendEmailAsync(emailRequest);

        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var value = _mapper.Map<Booking>(createBookingDto);
            value.BookingDate = DateTime.Now;
            value.BookingStatus = Models.Enums.BookingStatus.Bekliyor;

            await _bookingCollection.InsertOneAsync(value);
        }

        public async Task<List<ResultAllBookingDto>> GetAllBookingAsync()
        {
            var bookings = await _bookingCollection.Find(x => true).SortByDescending(x => x.BookingDate).ToListAsync();

            var dtoList = _mapper.Map<List<ResultAllBookingDto>>(bookings);

            foreach (var item in dtoList)
            {
                var tour = await _tourCollection.Find(x => x.TourId == item.TourId).FirstOrDefaultAsync();

                item.TourTitle = tour != null ? tour.Title : "Tur Bulunamadı";
            }
            return dtoList;
        }

        public async Task<List<ResultBookingDto>> GetAllBookingStatusPendingAsync()
        {
            var value = await _bookingCollection.Find(x => x.BookingStatus == Models.Enums.BookingStatus.Bekliyor).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(value);
        }

        public async Task<List<ResultBookingDto>> GetPassengerByTourIdAsync(string id)
        {
            var passenger = await _bookingCollection.Find(x => x.TourId == id && x.BookingStatus == Models.Enums.BookingStatus.Onaylandı).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(passenger);
        }

        public async Task<List<PendingBookingsDto>> GetPendingRezervasitionAsync()
        {
            var values = await _bookingCollection.Find(x => x.BookingStatus == Models.Enums.BookingStatus.Bekliyor).ToListAsync();
            var dtoList = _mapper.Map<List<PendingBookingsDto>>(values);
            foreach(var dto in dtoList)
            {
                var tour = await _tourCollection.Find(x => x.TourId == dto.TourId).FirstOrDefaultAsync();
                dto.TourTitle = tour != null ? tour.Title : "Tur Bulunamadı";
            }
            return dtoList;
        }
    }
}
