using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MediaBacklogManagerBackend.Services.Media
{
    public class AlbumService : MediaService<Album>
    {
        public AlbumService(AppDbContext context) : base(context)
        {
        }



        //Album Creation, Mapping, and Reading
        internal async Task<Album?> CreateAlbum(CreateAlbumDto albumDto)
        {
            Console.WriteLine($"Creating Album: {albumDto.Title}");

            bool exists = await CheckExistsAsync(albumDto.Title, albumDto.ReleaseDate);

            Console.WriteLine($"Album Exists: {exists}");
            if (exists)
                return null;

            var album = MapAlbumCreation(albumDto);

            return await CreateAsync(album);

        }


        internal async Task<List<ReadAlbumDto>> ReadAllAlbums()
        {
            return await dbContext.Albums
                .Select(m => new ReadAlbumDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    RunTime = m.RunTime,
                    GeneralRating = m.GeneralRating,
                    ReleaseDate = m.ReleaseDate,
                    Artist = m.Artist,
                    TrackCount = m.TrackCount,
                    Description = m.Description,

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.ToList()
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateAlbum(UpdateAlbumDto albumDto)
        {
            var id = albumDto.Id;
            var album = await GetItemById(id);

            if (album == null)
            {
                return false;
            }

            try
            {
                MapAlbumUpdate(album, albumDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the album");
            }

        }

        internal async Task<ReadAlbumDto?> ReadAlbumById(int id)
        {
            if (await CheckExistsAsync(id)) { }

            var album = await GetItemById(id);

            return GetReadAlbumDto(album!);
        }

        internal async Task<bool> DeleteAlbum(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private Album MapAlbumCreation(CreateAlbumDto albumDto)
        {
            return new Album
            {
                // Required
                Title = albumDto.Title,

                // Optional (nullable → fallback)
                Description = albumDto.Description ?? string.Empty,
                Artist = albumDto.Artist ?? string.Empty,



                // Value types with defaults
                RunTime = albumDto.RunTime ?? 0,
                GeneralRating = albumDto.GeneralRating ?? 0.0,
                TrackCount = albumDto.TrackCount,

                // Nullable stays nullable
                ReleaseDate = albumDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = albumDto.Assets ?? new List<MediaAsset>(),
                Genres = albumDto.Genres ?? new List<Genre>(),

                // System-managed fields
                DateCreated = albumDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private void MapAlbumUpdate(Album album, UpdateAlbumDto albumDto)
        {

            // Required
            album.Title = albumDto.Title;

            // Optional (nullable → fallback)
            album.Description = albumDto.Description ?? string.Empty;
            album.Artist = albumDto.Artist ?? string.Empty;

            // Value types with defaults
            album.RunTime = albumDto.RunTime ?? 0;
            album.GeneralRating = albumDto.GeneralRating ?? 0.0;
            album.TrackCount = albumDto.TrackCount;

            // Nullable stays nullable
            album.ReleaseDate = albumDto.ReleaseDate;

            // Collections (avoid nulls)
            album.Assets = albumDto.Assets ?? new List<MediaAsset>();
            album.Genres = albumDto.Genres ?? new List<Genre>();
        }

        private ReadAlbumDto GetReadAlbumDto(Album album)
        {
            return new ReadAlbumDto
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                Assets = album.Assets,
                ReleaseDate = album.ReleaseDate,
                Genres = album.Genres,
                GeneralRating = album.GeneralRating,
                RunTime = album.RunTime,
                TrackCount = album.TrackCount,
                Artist = album.Artist
            };
        }
    }
}
