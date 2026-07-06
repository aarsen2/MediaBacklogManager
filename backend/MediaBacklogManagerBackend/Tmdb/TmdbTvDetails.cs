using System;
using System.Collections.Generic;
using System.Text;

namespace Tmdb
{
    public class TmdbTvDetails
    {
        public string name { get; set; }
        public string overview { get; set; }
        public string first_air_date { get; set; }
        public string original_language { get; set; }

        public List<TmdbGenre> genres { get; set; }

        public List<TmdbCreatedBy> created_by { get; set; } = new();

        public int number_of_seasons { get; set; }
        public int number_of_episodes { get; set; }
    }
}
