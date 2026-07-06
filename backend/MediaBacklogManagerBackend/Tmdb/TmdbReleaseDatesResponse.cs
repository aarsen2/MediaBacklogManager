using System;
using System.Collections.Generic;
using System.Text;

namespace Tmdb
{
    public class TmdbReleaseDatesResponse
    {
        public List<TmdbReleaseDatesResult> results { get; set; } = new();
    }
}
