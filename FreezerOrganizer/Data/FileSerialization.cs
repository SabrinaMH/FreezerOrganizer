using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FreezerOrganizer.Data
{
    // Extracted all the stuff that should be mocked in case I want to unit test the Serialization class
    public class FileSerialization<T> : ISerialization<T> where T : class
    {
        private string _codePage;

        public FileSerialization(string codePage = "utf-8")
        {
            this._codePage = codePage;
        }

        public IList<T> DeserializeList(string path)
        {
            IList<T> deserializedList = null;
            if (File.Exists(path))
            {
                // XmlReader chosen over FileStream as this allows one to specify encoding
                using (var xmlReader = XmlReader.Create(new StreamReader(path, Encoding.GetEncoding(_codePage))))
                {
                    var serializer = new DataContractSerializer(typeof(List<T>));
                    deserializedList = (IList<T>)serializer.ReadObject(xmlReader);
                }
            }

            return deserializedList;
        }

        public void SerializeList(IList<T> listToSerialize, string path)
        {   
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("The path is empty.");
            }

            using (var xmlWriter = XmlWriter.Create(path))
            {
                var serializer = new DataContractSerializer(typeof(List<T>));
                /* Serialization<T>.SerializeList just contains a single line calling a method. 
                 * Reason for not just having the call here is, that SerializationWrapper is not used in unit tests.
                 * The purpose of the wrapper is to make it possible to mock the objects that we are not interested in
                 * when unit testing our Serialization class.
                 * */
                serializer.WriteObject(xmlWriter, listToSerialize);
            }
        }
    }
}
