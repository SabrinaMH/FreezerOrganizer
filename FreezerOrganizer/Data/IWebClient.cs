using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Data
{
    public interface IWebClient : IDisposable
    {
        string DownloadString(string address);
        string UploadString(string address, string method, string data);
    }
}
