using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTPLApp.Model
{
    public class YouTubeSearchResult
    {
        public YouTubeVideo[] Videos { get; set; }
        public string NextPageToken { get; set; }
    }
}