using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Data
{
    public interface ISerializationResource<T> where T : class
    {
        FileStream Open(string path, FileAccess accessMode);
        void WriteObject(FileStream fileStream, IList<T> listToSerialize);
    }
}
