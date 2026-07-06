using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MediaBacklogManagerBackend.Services
{
    public class BacklogMapper
    {
        private readonly AppDbContext dbContext;
        private readonly MediaMapper MediaMapper;

        public BacklogMapper(AppDbContext context, MediaMapper mediaMapper)
        {
            dbContext = context;
            MediaMapper = mediaMapper;
        }

        public static ReadUserMediaDto MapUserMediaRead(UserMedia userMedia, Models.Media.Media media)
        {
            if (userMedia == null)
            {
                throw new ArgumentNullException(nameof(userMedia));
            }


            return new ReadUserMediaDto
            {
                Id = userMedia.Id,
                UserId = userMedia.UserId,
                MediaId = userMedia.MediaId,
                Status = userMedia.Status,
                Prioritized = userMedia.Prioritized,
                UserRating = userMedia.UserRating,
                Notes = userMedia.Notes,
                DateAdded = userMedia.DateAdded,
                DateCompleted = userMedia.DateCompleted,
                Recommenders = userMedia.Recommenders.Select(r => r.Name).ToList(),

                Media = MediaMapper.MapMediaRead(media)
            };
        }


    }


}
