using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Emuns;
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
    }
}
