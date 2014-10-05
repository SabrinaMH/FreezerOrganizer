using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Runtime.Serialization;

namespace FreezerOrganizer.Data
{
    internal static class Serialization<T> where T : class
    {
        // fileResource parameter only for unit testing purposes
        internal static IList<T> DeserializeList(string path, FileStream fileStream, DataContractSerializer serializer) 
        {
            return (IList<T>)serializer.ReadObject(fileStream);
        }

        internal static void SerializeList(IList<T> listToSerialize, FileStream fileStream, DataContractSerializer serializer)
        {
            serializer.WriteObject(fileStream, listToSerialize);
        }
    }
}
