using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Models.Enums;

namespace Project3Travelin.Services.TourServices
{
    public interface ITourService
    {
        Task<List<ResultTourDto>> GetAllTourAsync();
        Task CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(UpdateTourDto updateTourDto);
        Task DeleteTourAsync(string id);
        Task <GetTourByIdDto> GetTourByIdAsync(string id);


    }
}
