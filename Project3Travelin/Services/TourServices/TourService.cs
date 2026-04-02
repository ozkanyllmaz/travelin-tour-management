using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Models.Enums;
using Project3Travelin.Settings;
using System.Text;

namespace Project3Travelin.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly ImageServices _imageService;

        public TourService(IMapper mapper, IDatabaseSettings _databaseSettings, ImageServices imageService)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
            _imageService = imageService;
        }


        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var values = _mapper.Map<Tour>(createTourDto);
            values.IsDeleted = false;
            await _tourCollection.InsertOneAsync(values);
        }

        public async Task DeleteTourAsync(string id)
        {
            var update = Builders<Tour>.Update.Set(x => x.IsDeleted, true);
            await _tourCollection.UpdateOneAsync(x => x.TourId == id, update);
        }

        public async Task<List<ResultTourDto>> GetAllTourAsync()
        {
            var values = await _tourCollection.Find(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }


        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var tour = await _tourCollection.Find(x => x.TourId == id && x.IsDeleted == false).FirstOrDefaultAsync();
            var dto = _mapper.Map<GetTourByIdDto>(tour);
            if (string.IsNullOrEmpty(tour.GeneratedImageUrl))
            {
                var locationBuilder = new StringBuilder();
                foreach (var program in tour.TourPrograms)
                {
                    locationBuilder.Append(program.Title + ", ");
                }
                string allLocations = locationBuilder.ToString().TrimEnd(',', ' ');

                string imagePrompt = $"A cartoony illustrated treasure map, rendered on an aged parchment texture, replicating the specific style and elements of the reference map in image_6.png. At the top center, a large, ornate old-style scroll banner displays the bold text 'HAZİNE HARİTASI: {tour.Title}'. The map shows a detailed coastline with a winding dashed red path connecting numbered location stops (1 to 5). Small non-serif numbers link to location labels with icons: 1. Antalya (archway), 2. Kaş (amphora), 3. Fethiye (rock-cut tomb), 4. Ölüdeniz (lagoon), 5. Kekova (submerged archway). Next to number 1, it says '(START)' and next to number 5, it says '(BATIK ŞEHİR)' and below number 5, 'FINAL DESTINATION (END)'. Small sailing ships and jumping dolphins swim in the light blue-teal water. Exotic parrots sit on the landmass. A detailed ornate compass rose is in the bottom left water. On the right side, a clean rectangular box with a bold non-serif header 'ROTA BAŞLIKLARI' followed by a numbered list from 1 to 5, matching the text from the reference: '1. Antalya', '2. Kaş', '3. Fethiye', '4. Ölüdeniz', '5. Kekova', in clear, legible non-serif font. The map features old-style illustrated ships, including a large galleon and smaller sailboats. All text is in clear, bold Turkish. The overall feel is one of an exciting, engaging treasure hunt with clean, bold lines and a warm, flat color palette.";
                dto.GeneratedImageUrl = await _imageService.GenerateImageAsync(imagePrompt);

                if(dto.GeneratedImageUrl != "/travelin/images/placeholder-map.jpg")
                {
                    tour.GeneratedImageUrl = dto.GeneratedImageUrl;
                    await _tourCollection.ReplaceOneAsync(x => x.TourId == id, tour);
                }
            }
            return dto;
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var values = _mapper.Map<Tour>(updateTourDto);
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, values);
        }
    }
}
