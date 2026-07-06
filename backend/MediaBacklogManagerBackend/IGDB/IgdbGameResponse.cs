using System;
using System.Collections.Generic;
using System.Text;

namespace IGDB
{
    public class IgdbGameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public long? First_Release_Date { get; set; }
        public double? Rating { get; set; }
        public int? Game_Type { get; set; }
        public int? Version_Parent { get; set; }

        public List<IgdbGenre>? Genres { get; set; }
        public List<IgdbPlatform>? Platforms { get; set; }
        public List<IgdbCompanyWrapper>? Involved_Companies { get; set; }
        public List<IgdbAgeRating>? Age_Ratings { get; set; }
        public IgdbCover? Cover { get; set; }
    }
}
