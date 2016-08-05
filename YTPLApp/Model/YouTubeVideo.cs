using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTPLApp.Model
{
	public class YouTubeVideo
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? PublishDate { get; set; }
		public string Url
		{
			get { return ("https://www.youtube.com/watch?v=" + Id); }
			private set { }
		}
	}
}