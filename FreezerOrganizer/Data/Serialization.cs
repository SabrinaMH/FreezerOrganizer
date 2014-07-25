using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Xml.Serialization;

namespace FreezerOrganizer.Data
{
    static class Serialization
    {
        private const string fileName = "SavedItems.xml";

        internal static List<Item> LoadItems()
        {
            var deserializedItems = new List<Item>();
            if (File.Exists(fileName))
            {
                using (StreamReader fileStream = new StreamReader(fileName))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Item>));
                    deserializedItems = (List<Item>)deserializer.Deserialize(fileStream);
                }
            }
            return new List<Item>() { new Item("test", 2, DateTime.Now) };
            // return deserializedItems; todo test
        }

        internal static void SaveItems(List<Item> items)
        {
            using (StreamWriter fileStream = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
                serializer.Serialize(fileStream, items);
            }
        }
    }
}
