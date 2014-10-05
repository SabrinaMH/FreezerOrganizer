using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Data
{
    public interface ISerialization<T> where T : class
    {
        IList<T> DeserializeList(string path);
        void SerializeList(IList<T> listToSerialize, string path);
    }
}
