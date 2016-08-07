using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YTPLApp.Domain;
using YTPLApp.Model;

namespace YTPLApp.Controllers
{
	public class YouTubeController : ApiController
	{
		//public string Get()
		//{
		//	YouTube yt = new YouTube();
		//	yt.GetByPlayListId();
		//	return "this is a test";
		//}

		public HttpResponseMessage Get()
		{
			var response = Request.CreateResponse(HttpStatusCode.Unused, "HTTP GET expected playListId or userId");
			return response;
		}

		[Route("api/YouTube/GetVideosByUser/{id}/{nextPageToken}")]
		public YouTubeSearchResult GetVideosByUser(string id, string nextPageToken)
		{
			YouTubeAPI yt = new YouTubeAPI();
			return yt.GetByUserId(id, nextPageToken == "~!@" ? "" : nextPageToken);

        }

		[Route("api/YouTube/GetVideoByPlaylist/{id}/{nextPageToken}")]
		public YouTubeSearchResult GetVideoByPlaylist(string id, string nextPageToken)
		{
			YouTubeAPI yt = new YouTubeAPI();
			return yt.GetByPlayListId(id, nextPageToken == "~!@" ? "" : nextPageToken);
		}
	}
}
