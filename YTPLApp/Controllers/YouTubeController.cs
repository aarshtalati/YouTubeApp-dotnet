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

		[Route("api/YouTube/GetVideosByUser/{id}")]
		public IEnumerable<object> GetVideosByUser(string id)
		{
			YouTube yt = new YouTube();
			return yt.GetByUserId(id);
		}

		[Route("api/YouTube/GetVideoByPlaylist/{id}")]
		public IEnumerable<object> GetVideoByPlaylist(string id)
		{
			YouTube yt = new YouTube();
			return yt.GetByPlayListId(id);
		}
	}
}
