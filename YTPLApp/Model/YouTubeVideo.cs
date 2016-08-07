using System;

namespace YTPLApp.Model
{
    public class YouTubeVideo
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? PublishDate { get; set; }
		public string ThumbnailImageUrl { get; set; }
		public string Url
		{
			get { return ("https://www.youtube.com/watch?v=" + Id); }
			private set { }
		}
	}
}