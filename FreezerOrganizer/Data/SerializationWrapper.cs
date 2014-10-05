using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Data
{
    // Extracted all the stuff that should be mocked in case I want to unit test the Serialization class
    public class SerializationWrapper<T> : ISerialization<T> where T : class
    {
        public IList<T> DeserializeList(string path)
        {
            IList<T> deserializedList = null;

            if (File.Exists(path))
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new DataContractSerializer(typeof(List<T>));
                    deserializedList = Serialization<T>.DeserializeList(path, fileStream, serializer);
                }
            }

            return deserializedList;
        }

        public void SerializeList(IList<T> listToSerialize, string path)
        {   
            // FileMode.Create overwrites file if it already exists
            if (path == "")
            {
                path = "temp.xml";
            }

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var serializer = new DataContractSerializer(typeof(List<T>));
                /* Serialization<T>.SerializeList just contains a single line calling a method. 
                 * Reason for not just having the call here is, that SerializationWrapper is not used in unit tests.
                 * The purpose of the wrapper is to make it possible to mock the objects that we are not interested in
                 * when unit testing our Serialization class.
                 * */
                Serialization<T>.SerializeList(listToSerialize, fileStream, serializer);
            }
        }
    }
}
