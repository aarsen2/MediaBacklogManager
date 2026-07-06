using System;
using System.Collections.Generic;
using System.Text;

namespace Tmdb
{
    public class TmdbSearchResponse
    {
        public List<TmdbSearchResult> results { get; set; } = new();
    }
}
