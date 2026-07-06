using System;
using System.Collections.Generic;
using System.Text;

namespace Tmdb
{
    public class TmdbTvContentRatingsResponse
    {
        public List<TmdbTvContentRatingResult> results { get; set; } = new();

    }
}
