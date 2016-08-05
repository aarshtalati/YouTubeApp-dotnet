using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YTPLApp.Domain
{
    public class YouTube
    {
        public void Test()
        {
            List<string> myResults = new List<string>();

            var filePath = HttpContext.Current.Server.MapPath("GoogleKeys.json");
            if (filePath.Contains("api\\YouTube\\"))
            {
                filePath = filePath.Replace("api\\YouTube\\", "");
            }

            JObject googleKeys = new JObject();
            using (StreamReader file = File.OpenText(filePath))
            {
                using (var reader = new JsonTextReader(file))
                {
                    googleKeys = (JObject)JToken.ReadFrom(reader);
                }
            }

            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "youtube-test",
                ApiKey = googleKeys["ServerApiKey"].ToString(),
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