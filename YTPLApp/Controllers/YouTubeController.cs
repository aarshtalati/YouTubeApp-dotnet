using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YTPLApp.Domain;

namespace YTPLApp.Controllers
{
    public class YouTubeController : ApiController
    {
        public string Get()
        {
            YouTube yt = new YouTube();
            yt.GetByPlayListId();
            return "this is a test";
        }

        public string post()
        {
            return "you posted to an empty method";
        }

        public string PostSingle(string videoId)
        {
            YouTube yt = new YouTube();
            yt.PlayList();
            return "you sent: " + videoId;
        }

        public string[] PostPlaylist(string playlistId)
        {
            return new string[] { "http://google.com", "http://bing.con" };
        }
    }
}
