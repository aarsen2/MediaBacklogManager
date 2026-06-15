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
    public class SongService : MediaService<Song>
    {
        public SongService(AppDbContext context) : base(context)
        {
        }



        //Song Creation, Mapping, and Reading
        internal async Task<Song?> CreateSong(CreateSongDto songDto)
        {
            Console.WriteLine($"Creating Song: {songDto.Title}");

            bool exists = await CheckExistsAsync(songDto.Title, songDto.ReleaseDate);

            Console.WriteLine($"Song Exists: {exists}");
            if (exists)
                return null;

            var song = await MapSongCreation(songDto);

            return await CreateAsync(song);

        }


        internal async Task<List<ReadSongDto>> ReadAllSongs()
        {
            return await dbContext.Songs
                .Select(m => new ReadSongDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    RunTime = m.RunTime,
                    GeneralRating = m.GeneralRating,
                    Artist = m.Artist,
                    ReleaseDate = m.ReleaseDate,
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

        internal async Task<bool> UpdateSong(UpdateSongDto songDto)
        {
            var id = songDto.Id;
            var song = await GetItemById(id);

            if (song == null)
            {
                return false;
            }

            try
            {
                await MapSongUpdate(song, songDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the song");
            }

        }

        internal async Task<ReadSongDto?> ReadSongById(int id)
        {
            if (await CheckExistsAsync(id))
            {

                var song = await GetItemById(id);

                return GetReadSongDto(song!);
            }
            return null;
        }

        internal async Task<bool> DeleteSong(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private async Task<Song> MapSongCreation(CreateSongDto songDto)
        {
            return new Song
            {
                // Required
                Title = songDto.Title,

                // Optional (nullable → fallback)
                Description = songDto.Description ?? string.Empty,
                Artist = songDto.Artist ?? string.Empty,

                // Value types with defaults
                RunTime = songDto.RunTime ?? 0,
                GeneralRating = songDto.GeneralRating ?? 0.0,

                // Nullable stays nullable
                ReleaseDate = songDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = songDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(songDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = songDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task MapSongUpdate(Song song, UpdateSongDto songDto)
        {

            // Required
            song.Title = songDto.Title;

            // Optional (nullable → fallback)
            song.Description = songDto.Description ?? string.Empty;
            song.Artist = songDto.Artist ?? string.Empty;

            // Value types with defaults
            song.RunTime = songDto.RunTime ?? 0;
            song.GeneralRating = songDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            song.ReleaseDate = songDto.ReleaseDate;

            // Collections (avoid nulls)
            song.Assets = songDto.Assets ?? new List<MediaAsset>();
            song.Genres = await GetGenresAsync(songDto.Genres) ?? new List<Genre>();
        }

        private ReadSongDto GetReadSongDto(Song song)
        {
            return new ReadSongDto
            {
                Id = song.Id,
                Title = song.Title,
                Description = song.Description,
                Assets = song.Assets,
                ReleaseDate = song.ReleaseDate,
                Genres = song.Genres.Select(g => new ReadGenreDto
                {
                    Name = g.Name,
                    Id = g.Id,
                }).ToList(),
                GeneralRating = song.GeneralRating,
                RunTime = song.RunTime,
                Artist = song.Artist
            }
            ;
        }
    }
}
