using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Data;

namespace FreezerOrganizer.Model
{
    static class Services
    {
        private static List<Item> items = new List<Item>();

        internal static void SaveItems(List<Item> items)
        {
            Serialization.SaveItems(items);
        }

        internal static void LoadItems()
        {
            items = Serialization.LoadItems();
        }

        internal static List<Item> Search(string input)
        {
            return items.FindAll(item => item.Name.Contains(input, StringComparison.OrdinalIgnoreCase));
        }

        // extension method to do case insensitive search among the item names.
        private static bool Contains(this string source, string input, StringComparison comp)
        {
            return source.IndexOf(input, comp) >= 0;
        }
    }
}


