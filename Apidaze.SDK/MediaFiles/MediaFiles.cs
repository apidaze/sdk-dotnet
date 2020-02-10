using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apidaze.SDK.Base;
using Apidaze.SDK.Calls;
using Newtonsoft.Json;
using RestSharp;

namespace Apidaze.SDK.MediaFiles
{
    public class MediaFiles : BaseApiClient, IMediaFiles
    {
        public MediaFiles(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        protected override string Resource => "/mediafiles";
        
        public List<dynamic> GetMediaFilesList(bool details, string filter, string lastToken, int maxItems)
        {
            if (string.IsNullOrEmpty(filter)) throw new ArgumentException("filter must not be null or empty");
            if (string.IsNullOrEmpty(lastToken)) throw new ArgumentException("lastToken must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddParameter("details", details);
            restRequest.AddParameter("filter", filter);
            restRequest.AddParameter("last_token", lastToken);
            restRequest.AddParameter("max_items", maxItems);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content).ToList();
        }

        public dynamic UploadMediaFile(string name, string path, bool isStream = true)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name must not be null or empty");
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("path must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("isStream", isStream);
            restRequest.AddParameter("mediafile", path);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content);
        }

        public dynamic DeleteMediaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.DELETE;
            restRequest.AddParameter("filename", fileName);
           
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content);
        }

        public dynamic DownloadMediaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddParameter("filename", fileName);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content);
        }

        public dynamic ShowMediaFileSummary(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.HEAD;
            restRequest.AddParameter("filename", fileName);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content);
        }
    }
}
