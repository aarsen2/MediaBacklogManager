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
    public class BookService : MediaService<Book>
    {
        public BookService(AppDbContext context) : base(context)
        {
        }



        //Book Creation, Mapping, and Reading
        internal async Task<Book?> CreateBook(CreateBookDto bookDto, string userId)
        {
            Console.WriteLine($"Creating book: {bookDto.Title}");

            bool exists = await CheckExistsAsync(bookDto.Title, bookDto.ReleaseDate);

            Console.WriteLine($"Book Exists: {exists}");
            if (exists)
                return null;

            var book = await MapBookCreation(bookDto);

            return await CreateAsync(book, userId);

        }


        internal async Task<List<ReadBookDto>> ReadAllBooks()
        {
            return await dbSet
                .Select(m => new ReadBookDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Description = m.Description,
                    Author = m.Author,
                    PageCount = m.PageCount,
                    Language = m.Language,

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.Select(g => new ReadGenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateBook(UpdateBookDto bookDto)
        {
            var id = bookDto.Id;
            var book = await GetItemByIdAsync(id);

            if (book == null)
            {
                return false;
            }

            try
            {
                await MapBookUpdate(book, bookDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the book");
            }

        }

        internal async Task<ReadBookDto?> ReadBookById(int id)
        {
            if (await CheckExistsAsync(id))
            {

                var book = await GetItemByIdAsync(id);

                return GetReadBookDto(book!);
            }
            return null;
        }

        internal async Task<bool> DeleteBook(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private async Task<Book> MapBookCreation(CreateBookDto bookDto)
        {
            return new Book
            {
                // Required
                Title = bookDto.Title,

                // Optional (nullable → fallback)
                Description = bookDto.Description ?? string.Empty,
                Author = bookDto.Author ?? string.Empty,
                Language = bookDto.Language ?? "Unknown",

                // Value types with defaults
                GeneralRating = bookDto.GeneralRating ?? 0.0,
                PageCount = bookDto.PageCount ?? 0,

                // Nullable stays nullable
                ReleaseDate = bookDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = bookDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(bookDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = bookDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task MapBookUpdate(Book book, UpdateBookDto bookDto)
        {

            // Required
            book.Title = bookDto.Title;

            // Optional (nullable → fallback)
            book.Description = bookDto.Description ?? string.Empty;
            book.Language = bookDto.Language ?? "Unknown";
            book.Author = bookDto.Author ?? string.Empty;
            // Value types with defaults
            book.PageCount = bookDto.PageCount ?? 0;
            book.GeneralRating = bookDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            book.ReleaseDate = bookDto.ReleaseDate;

            // Collections (avoid nulls)
            book.Assets = bookDto.Assets ?? new List<MediaAsset>();
            book.Genres = await GetGenresAsync(bookDto.Genres) ?? new List<Genre>();
        }

        private ReadBookDto GetReadBookDto(Book book)
        {
            return new ReadBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Assets = book.Assets,
                ReleaseDate = book.ReleaseDate,
                Genres = book.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                GeneralRating = book.GeneralRating,
                Language = book.Language,
                PageCount = book.PageCount,
                Author = book.Author,
            }
            ;
        }
    }
}
