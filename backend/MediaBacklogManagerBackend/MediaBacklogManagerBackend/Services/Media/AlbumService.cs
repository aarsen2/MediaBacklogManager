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
        internal async Task<Album?> CreateAlbum(CreateAlbumDto albumDto, string userId)
        {
            Console.WriteLine($"Creating Album: {albumDto.Title}");

            var album = await CheckExistsAsync(albumDto.Title, albumDto.ReleaseDate);

            if (album != null)
                return album;

            album = await MapAlbumCreation(albumDto);

            return await CreateAsync(album, userId);
        }




        internal async Task<List<ReadAlbumDto>> ReadAllAlbums()
        {
            return await dbSet
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

                    Genres = m.Genres.Select(g => new ReadGenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateAlbum(UpdateAlbumDto albumDto)
        {
            var id = albumDto.Id;
            var album = await GetItemByIdAsync(id);

            if (album == null)
            {
                return false;
            }

            try
            {
                await MapAlbumUpdate(album, albumDto);

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
            if (await CheckExistsAsync(id))
            {

                var album = await GetItemByIdAsync(id);

                return GetReadAlbumDto(album!);
            }
            return null;
        }

        internal async Task<bool> DeleteAlbum(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private async Task<Album> MapAlbumCreation(CreateAlbumDto albumDto)
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
                Genres = await GetGenresAsync(albumDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = albumDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task MapAlbumUpdate(Album album, UpdateAlbumDto albumDto)
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
            album.Genres = await GetGenresAsync(albumDto.Genres) ?? new List<Genre>();
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
                Genres = album.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                GeneralRating = album.GeneralRating,
                RunTime = album.RunTime,
                TrackCount = album.TrackCount,
                Artist = album.Artist
            };
        }
    }
}
