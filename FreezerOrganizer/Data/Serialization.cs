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
    static class Serialization
    {
        internal static List<T> DeserializeList<T>(string path) 
            where T : class
        {
            var deserializedList = new List<T>();

            if (File.Exists(path))
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new DataContractSerializer(typeof(List<T>));
                    deserializedList = (List<T>)serializer.ReadObject(fileStream);
                }
            }

            return deserializedList;
        }

        internal static void SerializeList<T>(List<T> listToSerialize, string path) 
            where T : class
        {
            // FileMode.Create overwrites file if it already exists
            if (path == "")
            {
                path = "temp.xml";
            }

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var serializer = new DataContractSerializer(typeof(List<T>));
                serializer.WriteObject(fileStream, listToSerialize);
            }
        }
    }
}
