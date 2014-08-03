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
        private const string fileName = "SavedItems.xml";

        internal static List<T> DeserializeObject<T>() 
            where T : class
        {
            List<T> deserializedObj = null;

            if (File.Exists(fileName))
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new DataContractSerializer(typeof(List<T>));
                    deserializedObj = (List<T>)serializer.ReadObject(fileStream);
                }
            }

            return deserializedObj;
        }

        internal static void SerializeObject<T>(List<T> objectToSerialize) 
            where T : class
        {
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var serializer = new DataContractSerializer(typeof(List<T>));
                serializer.WriteObject(fileStream, objectToSerialize);
            }
        }
    }
}
