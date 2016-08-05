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
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;

namespace YTPLApp.Domain
{
    public class YouTube
    {
        private static YouTubeService youtubeService;

        public YouTube()
        {
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

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "youtube-test",
                ApiKey = googleKeys["ServerApiKey"].ToString(),
            });





            UserCredential credential;

            //var filePath = HttpContext.Current.Server.MapPath("client_secrets.json");
            //if (filePath.Contains("api\\YouTube\\"))
            //{
            //    filePath = filePath.Replace("api\\YouTube\\", "");
            //}

            //var folder = HttpContext.Current.Server.MapPath("~/App_Data/MyGoogleStorage");


            //using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //{
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        // This OAuth 2.0 access scope allows for read-only access to the authenticated 
            //        // user's account, but not other types of account access.
            //        new[] { YouTubeService.Scope.YoutubeReadonly },
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(folder)
            // ).Result;


            //}

            //var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = this.GetType().ToString()
            //});


            
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

        public void PlayList()
        {
            List<string> myResults = new List<string>();

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.ForUsername = "EllieGouldingVEVO";
            //channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            var channelsListResponse = channelsListRequest.ExecuteAsync().Result;

            foreach (var channel in channelsListResponse.Items)
            {
                // From the API response, extract the playlist ID that identifies the list
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

                var nextPageToken = "";
                while (nextPageToken != null)
                {
                    var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;

                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    var playlistItemsListResponse = playlistItemsListRequest.ExecuteAsync().Result;

                    foreach (var playlistItem in playlistItemsListResponse.Items)
                    {
                        // Print information about each video.
                        myResults.Add(string.Format("{0} ({1})", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId));
                    }

                    nextPageToken = playlistItemsListResponse.NextPageToken;

                }
            }

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

        }
}