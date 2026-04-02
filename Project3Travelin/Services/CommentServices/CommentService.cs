using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Comment> _commentCollection;

        public CommentService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _commentCollection = database.GetCollection<Comment>(_databaseSettings.CommentCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var values = _mapper.Map<Comment>(createCommentDto);
            values.CommentDate = DateTime.Now;
            values.IsStatus = true;
            values.Score = (values.Guide + values.Program + values.ValueForMoney + values.Service + values.Organization) / 5;

            await _commentCollection.InsertOneAsync(values);
        }

        public async Task DeleteCommentAsync(string id)
        {
            var comment = await _commentCollection.Find(x => x.CommentId == id).FirstOrDefaultAsync();
            var update = Builders<Comment>.Update.Set(x => x.IsStatus, !comment.IsStatus);
            await _commentCollection.UpdateOneAsync(x => x.CommentId == id, update);
        }

        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            var values = await _commentCollection.Find(x => true).SortByDescending(x => x.CommentDate).ToListAsync();
            return _mapper.Map<List<ResultCommentDto>>(values);
        }

        public async Task<GetCommentByIdDto> GetCommentByIdAsync(string id)
        {
            var value = await _commentCollection.Find(x => x.CommentId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCommentByIdDto>(value);
        }

        public async Task<(List<ResultCommentListByTourIdDto> Comments, double AverageScore, double GuideScore, double ProgramScore, double ValueForMoney, double OrganizationScore, double ServiceScore)> GetRatingScoreByTourIdAsync(string id)
        {
            var values = await _commentCollection.Find(x => x.TourId == id).ToListAsync();
            double temp = 0;
            double tempGuide = 0;
            double tempProgramScore = 0;
            double tempValueForMoney = 0;
            double tempOrganizatioScore = 0;
            double tempServiceScore = 0;
            if (values.Count > 0)
            {
                foreach (var comment in values)
                {
                    temp += comment.Score;
                    tempGuide += comment.Guide;
                    tempProgramScore += comment.Program;
                    tempValueForMoney += comment.ValueForMoney;
                    tempOrganizatioScore += comment.Organization;
                    tempServiceScore += comment.Service;
                }
            }
            
            double resultTemp = values.Count > 0 ? (temp / values.Count) : 0;
            double resultGuid = values.Count > 0 ? (tempGuide  / values.Count) : 0;
            double resultProgram = values.Count > 0 ? (tempProgramScore / values.Count) : 0;
            double resultValueForMoney = values.Count > 0 ? (tempValueForMoney / values.Count) : 0;
            double resultOrganization = values.Count > 0 ? (tempOrganizatioScore / values.Count) : 0;
            double resultService = values.Count > 0 ? (tempServiceScore / values.Count) : 0;

            var mappedList = _mapper.Map<List<ResultCommentListByTourIdDto>>(values);

            return (mappedList, resultTemp, resultGuid, resultProgram, resultValueForMoney, resultOrganization, resultService);
        }


        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var values = _mapper.Map<Comment>(updateCommentDto);
            await _commentCollection.FindOneAndReplaceAsync(x => x.CommentId == updateCommentDto.CommentId, values);
        }

        async Task<List<ResultCommentListByTourIdDto>> ICommentService.GetCommentsByTourIdAsync(string id)
        {
            var values = await _commentCollection.Find(x => x.TourId == id).SortByDescending(x => x.CommentDate).ToListAsync();
            return _mapper.Map<List<ResultCommentListByTourIdDto>>(values);
        }
    }
}
