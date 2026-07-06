using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services.ApiServices;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MediaBacklogManagerBackend.Services
{
    public class SearchService
    {
        private AppDbContext dbContext;
        private readonly MediaMapper MediaMapper;
        private readonly TmdbMovieService TmdbMovieService;
        private readonly TmdbShowService TmdbShowService;
        public SearchService(
            AppDbContext context, 
            MediaMapper mediaMapper, 
            TmdbMovieService tmdbMovieService,
            TmdbShowService tmdbShowService)
        {
            dbContext = context;
            MediaMapper = mediaMapper;
            TmdbMovieService = tmdbMovieService;
            TmdbShowService = tmdbShowService;
        }



        public async Task<SearchResults> runSearch(SearchParameters parameters, string userId)
        {
            var query = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId);

            if (!string.IsNullOrWhiteSpace(parameters.GenericSearch))
            {
                var normalizedSearch = parameters.GenericSearch.ToLower();
                query = query.Where(m =>
                m.Media.Title.ToLower().Contains(normalizedSearch) ||
                m.Media.Description.ToLower().Contains(normalizedSearch) ||
                m.Media.Genres.Any(g => g.Name.ToLower().Contains(normalizedSearch)) ||
                (m.Media is Game && ((Game)m.Media).Platforms.Any(p => p.Name.ToLower().Contains(normalizedSearch)))
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.PlatformSearch))
            {
                var normalizedSearch = parameters.PlatformSearch.ToLower();

                query = query.Where(m =>
                m.Media is Game &&
                ((Game)m.Media)
                    .Platforms.Any(p =>
                        p.Name.ToLower()
                        .Contains(normalizedSearch)));

            }
            if (!string.IsNullOrWhiteSpace(parameters.GenreSearch))
            {
                var normalizedSearch = parameters.GenreSearch.ToLower();

                query = query.Where(m =>
                m.Media.Genres.Any(g =>
                       g.Name.ToLower()
                       .Contains(normalizedSearch)));
            }
            if (!string.IsNullOrWhiteSpace(parameters.RecommenderSearch))
            {
                var normalizedSearch = parameters.RecommenderSearch.ToLower();

                query = query.Where(m =>
                m.Recommenders.Any(r =>
                    r.Name.ToLower()
                    .Contains(normalizedSearch)));
            }

            return new SearchResults
            {
                Results = await query.Select(m => new SearchResultItem
                {
                    Assets = m.Media.Assets,
                    Description = m.Media.Description,
                    Id = m.MediaId,
                    MediaType = m.Media.MediaType,
                    Title = m.Media.Title
                }).ToListAsync()
            };

        }

        //internal async Task<object?> runCreationSearchAsync(string title, string mediaType)
        //{
        //    //var normalizedTitle = title.ToLower().Trim();
        //    //var normalizedMediaType = mediaType.ToLower().Trim();

        //    //var mediaItem = await dbContext.Media
        //    //    .Where(m => m.Title.ToLower() == normalizedTitle &&
        //    //                m.MediaType.ToLower() == mediaType)
        //    //    .FirstOrDefaultAsync();
        //    //Console.WriteLine(normalizedTitle);
        //    //Console.WriteLine(normalizedMediaType);
        //    //Console.WriteLine(mediaItem);
        //    //if (mediaItem == null)
        //    //{
        //    //    return null;
        //    //}

        //    //return MediaMapper.MapMediaRead(mediaItem);
        //}

        internal async Task<ReadMovieDto> MovieCreationSearchAsync(string title)
        {
            var tmdbId = await TmdbMovieService.SearchIdByStringAsync(title); // your search method
            if (tmdbId is null)
                return null; // or NotFound

            var dto = await TmdbMovieService.BuildMovieDtoAsync(tmdbId.Value);
            return dto;
        }

        internal async Task<ReadShowDto> ShowCreationSearchAsync(string title)
        {
            var tmdbId = await TmdbShowService.SearchIdByStringAsync(title); // your search method
            if (tmdbId is null)
                return null; // or NotFound

            var dto = await TmdbShowService.BuildShowDtoAsync(tmdbId.Value);
            return dto;
        }
    }
}

