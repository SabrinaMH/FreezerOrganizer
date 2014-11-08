using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace FreezerOrganizer.Data
{
    public class DatabaseSerialization<T> : ISerialization<T> where T : class
    {
        private IWebClient _webClient;

        public DatabaseSerialization(IWebClient webClient) 
        {
            this._webClient = webClient;
        }

        public void SerializeList(IList<T> list, string path)
        {
            var listAsJson = JsonConvert.SerializeObject(list, new Newtonsoft.Json.Converters.StringEnumConverter());
            // because _webClient.Encoding hasn't been set, System.Text.Encoding.Default is used, which is ISO-8895-1
            _webClient.UploadString(path, "PUT", listAsJson);
        }

        /// <param name="path">A URL which returns list to be deserialized.</param>
        public IList<T> DeserializeList(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Path is null or empty");
            }

            var jsonData = _webClient.DownloadString(path);
            return JsonConvert.DeserializeObject<IList<T>>(jsonData, new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
