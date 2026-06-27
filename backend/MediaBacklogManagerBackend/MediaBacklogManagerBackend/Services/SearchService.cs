using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MediaBacklogManagerBackend.Services
{
    public class SearchService
    {
        private AppDbContext dbContext;
        public SearchService(AppDbContext context)
        {
            dbContext = context;
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

    }
}
