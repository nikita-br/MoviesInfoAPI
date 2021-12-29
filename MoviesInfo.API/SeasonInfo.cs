using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesInfo.API
{

    public class SeasonInfo
    {
        public string Title { get; set; }
        public string Season { get; set; }
        public string totalSeasons { get; set; }
        public Ep[] Episodes { get; set; }
        public string Response { get; set; }
    }

    public class Ep
    {
        public string Title { get; set; }
        public string Released { get; set; }
        public string Episode { get; set; }
        public string imdbRating { get; set; }
        public string imdbID { get; set; }
    }

}
