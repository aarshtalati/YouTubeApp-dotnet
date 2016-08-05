using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Upload;
using System.Diagnostics;

namespace YTPLApp.Domain
{
    public class YouTube
    {
        private static YouTubeService youtubeService;

        public YouTube()
        {
            //var filePath = HttpContext.Current.Server.MapPath("GoogleKeys.json");
            //if (filePath.Contains("api\\YouTube\\"))
            //{
            //    filePath = filePath.Replace("api\\YouTube\\", "");
            //}

            //JObject googleKeys = new JObject();
            //using (StreamReader file = File.OpenText(filePath))
            //{
            //    using (var reader = new JsonTextReader(file))
            //    {
            //        googleKeys = (JObject)JToken.ReadFrom(reader);
            //    }
            //}

            //YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            //{
            //    ApplicationName = "youtube-test",
            //    ApiKey = googleKeys["ServerApiKey"].ToString(),
            //});





            UserCredential credential;

            var filePath = HttpContext.Current.Server.MapPath("client_secrets.json");
            if (filePath.Contains("api\\YouTube\\"))
            {
                filePath = filePath.Replace("api\\YouTube\\", "");
            }

            var folder = HttpContext.Current.Server.MapPath("~/App_Data/MyGoogleStorage");


            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.YoutubeReadonly },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(folder)).Result;
            }

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });
        }


        public void Test()
        {
            List<string> myResults = new List<string>();

            
            SearchResource.ListRequest listRequest = youtubeService.Search.List("snippet");
            listRequest.Q = "Loeb Pikes Peak";
            listRequest.MaxResults = 5;
            listRequest.Type = "video";
            SearchListResponse resp = listRequest.Execute();
            foreach (SearchResult result in resp.Items)
            {
                myResults.Add(result.Snippet.Title);
            }
        }

        public async Task PlayList()
        {
            
            


            //var video = new Video();
            //video.Snippet = new VideoSnippet();
            //video.Snippet.Title = "Default Video Title";
            //video.Snippet.Description = "Default Video Description";
            //video.Snippet.Tags = new string[] { "tag1", "tag2" };
            //video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            //video.Status = new VideoStatus();
            //video.Status.PrivacyStatus = "unlisted"; // or "private" or "public"
            //filePath = @"C:\Users\Aarsh\Videos\From Aarsh Talati\dad_bribing_daughter.mp4"; // Replace with path to actual movie file.
            //using (var fileStream = new FileStream(filePath, FileMode.Open))
            //{
            //    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
            //    videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
            //    videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;
            //    await videosInsertRequest.UploadAsync();
            //}


        }

        private void videosInsertRequest_ResponseReceived(Video obj)
        {
            Debug.WriteLine("response received");
        }

        private void videosInsertRequest_ProgressChanged(IUploadProgress obj)
        {
            Debug.WriteLine("process changed");
        }
    }
}