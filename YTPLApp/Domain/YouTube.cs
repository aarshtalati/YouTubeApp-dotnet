using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTPLApp.Domain
{
    public class YouTube
    {
        public void Test()
        {
            List<string> myResults = new List<string>();

            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "youtube-test",
                ApiKey = "AIzaSyDafGzPwgo6ycE7OQ28wDO_lOFoLWZMluk",
            });
            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = "Loeb Pikes Peak";
            listRequest.MaxResults = 5;
            listRequest.Type = "video";
            SearchListResponse resp = listRequest.Execute();
            foreach (SearchResult result in resp.Items)
            {
                myResults.Add(result.Snippet.Title);
            }
        }
    }
}