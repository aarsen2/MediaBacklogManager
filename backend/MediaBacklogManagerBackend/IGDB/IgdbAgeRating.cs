namespace IGDB
{
    public class IgdbAgeRating
    {
        public int Id { get; set; }
        public IgdbAgeRatingOrganization? Organization { get; set; }

        public IgdbAgeRatingCategory? Rating_Category { get; set; }
    }
}