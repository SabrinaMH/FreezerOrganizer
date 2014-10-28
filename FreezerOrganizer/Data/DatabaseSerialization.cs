using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace FreezerOrganizer.Data
{
    public class DatabaseSerialization<T> : ISerialization<T> where T : class
    {
        private IWebClient _webClient;
        private string _codePage;

        // todo: is codepage needed?
        public DatabaseSerialization(IWebClient webClient, string codePage) 
        {
            this._webClient = webClient;
            this._codePage = codePage;
        }

        public void SerializeList(IList<T> list, string path)
        {
            var listAsJson = JsonConvert.SerializeObject(list);
            _webClient.UploadString(path, "PUT", listAsJson);
        }

        /// <param name="path">An URL which returns list to be deserialized.</param>
        public IList<T> DeserializeList(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Path is null or empty");
            }

            var jsonData = _webClient.DownloadString(path);

            return JsonConvert.DeserializeObject<IList<T>>(jsonData);
        }
    }
}
