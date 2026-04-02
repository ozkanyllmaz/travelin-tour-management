using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.EmailServices;

namespace Project3Travelin.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateBooking(string id)
        {
            var model = new CreateBookingDto();
            model.TourId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            await _bookingService.CreateBookingAsync(createBookingDto);
            return RedirectToAction("TourDetail", "Tour", new { id = createBookingDto.TourId});
        }

        [HttpGet]
        public async Task<IActionResult> GetPassergerByTourId(string id)
        {
            var passenger = await _bookingService.GetPassengerByTourIdAsync(id);
            return PartialView("_GetPassergerByTourId", passenger);  
        }


        [HttpGet]
        public async Task<IActionResult> DownloadExcel(string tourId, string title)
        {
            var passengers = await _bookingService.GetPassengerByTourIdAsync(tourId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Müşteri Listesi");

                worksheet.Cell(1, 1).Value = "Müşteri Adı";
                worksheet.Cell(1, 2).Value = "Telefon";
                worksheet.Cell(1, 3).Value = "E-posta";
                worksheet.Cell(1, 4).Value = "Kişi Sayısı";
                worksheet.Cell(1, 5).Value = "Kayıt Tarihi";

                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                foreach(var item in passengers)
                {
                    worksheet.Cell(row, 1).Value = $"{item.FirstName} {item.LastName}";
                    worksheet.Cell(row, 2).Value = item.Phone;
                    worksheet.Cell(row, 3).Value = item.Email;
                    worksheet.Cell(row, 4).Value = item.PassengerCount;
                    worksheet.Cell(row, 5).Value = item.BookingDate.ToString("dd.MM.yyyy");
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    string dynamicFileName = $"{title.Replace(" ", "_")}_Musteri_Listesi.xlsx";

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", dynamicFileName);
                }

            }
        }

    }
}
