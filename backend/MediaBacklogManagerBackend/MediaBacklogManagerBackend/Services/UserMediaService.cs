using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaBacklogManagerBackend.Services
{
    public class UserMediaService
    {
        AppDbContext dbContext;
        UserMediaMapper UserMediaMapper;
        public UserMediaService(AppDbContext context, UserMediaMapper userMediaMapper)
        {
            dbContext = context;
            UserMediaMapper = userMediaMapper;
        }

        internal async Task<UserMedia?> AddMediaAsync(CreateUserMediaDto userMediaDto, string userId)
        {
            var mediaId = userMediaDto.MediaId;
            var exists = await CheckExists(mediaId, userId);
            if (exists)
            {
                return null;
            }

            UserMedia userMedia = await UserMediaMapper.MapAddition(userMediaDto, userId);

            dbContext.UserMedia.Add(userMedia);
            await dbContext.SaveChangesAsync();

            return userMedia;
        }

        internal async Task<bool> DeleteMediaAsync(int mediaID, string userId)
        {
            var media = await GetUserMediaByMediaIdAsync(mediaID, userId);
            if (media == null)
            {
                return false;
            }
            try
            {
                dbContext.UserMedia.Remove(media);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new ArgumentException("Invalid Media ID");
            }
        }

        private async Task<UserMedia?> GetUserMediaByMediaIdAsync(int mediaId, string userId)
        {
            var userMedia = dbContext.UserMedia
                .Where(m => m.UserId == userId && m.MediaId == mediaId)
                .FirstOrDefault();

            if (userMedia == null)
            {
                return null;
            }

            return userMedia;
        }

        internal async Task<UserMedia?> GetUserMediaByIdAsync(int id, string userId)
        {
            var userMedia = await dbContext.UserMedia.FindAsync(id);

            if (userMedia == null)
            {
                return null;
            }

            if (userMedia.UserId != userId)
            {
                return null;
            }

            return userMedia;
        }

        internal async Task<object?> ReadAllAsync(string userId)
        {
            var Media = await dbContext.UserMedia
                            .Include(m => m.User)
                            .Include(m => m.Media)
                            .Where(m => m.UserId == userId)
                            .ToListAsync();
            return Media.Select(UserMediaMapper.MapMediaRead).ToList();
        }

        internal async Task<ReadUserMediaDto?> ReadByIdAsync(int id, string userId)
        {

            var media = await dbContext.UserMedia.FindAsync(id);
            if (media == null)
            {
                return null;
            }
            if (media.UserId != userId)
            {
                return null;
            }
            return UserMediaMapper.MapMediaRead(media);
        }

        internal async Task<bool> UpdateMediaAsync(UpdateUserMediaDto userMediaDto, string userId)
        {
            var mediaId = userMediaDto.MediaId;
            var userMedia = await GetUserMediaByMediaIdAsync(mediaId, userId);

            if (userMedia == null)
            {
                return false;
            }

            try
            {
                UserMediaMapper.MapMediaUpdate(userMedia, userMediaDto);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating User Media");
            }
        }


        internal async Task<bool> CheckExists(int mediaId, string userId)
        {

            var media = await dbContext.UserMedia.FirstOrDefaultAsync(m => m.UserId == userId && m.MediaId == mediaId);

            if (media is null)
            {
                return false;
            }
            return true;
        }
    }
}
