using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;

namespace MediaBacklogManagerBackend.Services
{
    public class UserMediaMapper
    {
        public AppDbContext dbContext { get; set; }

        public UserMediaMapper(AppDbContext context)
        {
            dbContext = context;
        }

        internal async Task<UserMedia> MapAddition(CreateUserMediaDto userMediaDto, string userId)
        {
            DateTime? dateCompleted;
            if (userMediaDto.Status == UserMediaStatus.Completed)
            {
                dateCompleted = DateTime.UtcNow;
            }
            else { dateCompleted = null; }


            return new UserMedia
            {
                // System Created
                DateAdded = DateTime.UtcNow,
                DateCompleted = dateCompleted,
                UserId = userId,

                //From the Dto
                MediaId = userMediaDto.MediaId,
                Notes = userMediaDto.Notes,
                Prioritized = userMediaDto.Prioritized,
                Status = userMediaDto.Status,
                UserRating = userMediaDto.UserRating,
                Recommenders = await GetRecommendersAsync(userMediaDto.Recommenders)
            };
        }


        public ReadUserMediaDto MapMediaRead(UserMedia userMedia)
        {
            return new ReadUserMediaDto
            {
                Id = userMedia.Id,
                MediaId = userMedia.MediaId,
                UserId = userMedia.UserId,
                UserRating = userMedia.UserRating,
                DateAdded = userMedia.DateAdded,
                DateCompleted = userMedia.DateCompleted,
                Notes = userMedia.Notes,
                Prioritized = userMedia.Prioritized,
                Status = userMedia.Status,
            };
        }

        public async Task MapMediaUpdateAsync(UserMedia userMedia, UpdateUserMediaDto userMediaDto)
        {
            var previousStatus = userMedia.Status;
            var newStatus = userMediaDto.Status;

            userMedia.UserRating = userMediaDto.UserRating;

            if (newStatus == UserMediaStatus.Completed)
            {
                if (userMedia.DateCompleted == null)
                {
                    userMedia.DateCompleted = DateTime.UtcNow;
                }
            }
            else if (previousStatus == UserMediaStatus.Completed)
            {
                userMedia.DateCompleted = null;
            }

            userMedia.Status = newStatus;
            userMedia.Notes = userMediaDto.Notes;
            userMedia.Prioritized = userMediaDto.Prioritized;

            userMedia.Recommenders = await GetRecommendersAsync(userMediaDto.Recommenders);
        }

        protected async Task<List<Recommender>> GetRecommendersAsync(List<string> recommenderStrings)
        {
            // Normalize for comparison only
            var cleanedInputs = recommenderStrings
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            List<Recommender> recommenders = new List<Recommender>();
            foreach (var recommenderString in cleanedInputs)
            {
                var normalizedRecommender = recommenderString.ToLower();
                var recommender = await dbContext.Recommenders
                    .FirstOrDefaultAsync(g => g.Name.ToLower() == normalizedRecommender);

                if (recommender == null)
                {
                    recommender = new Recommender() { Name = recommenderString };

                    await dbContext.Recommenders.AddAsync(recommender);
                }
                recommenders.Add(recommender);

            }

            return recommenders;
        }

        protected List<ReadRecommenderDto> ReadRecommenders(UserMedia userMedia)
        {
            return userMedia.Recommenders.Select(g => new ReadRecommenderDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }
    }
}
