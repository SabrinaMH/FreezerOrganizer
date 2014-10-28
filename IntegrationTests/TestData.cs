using FreezerOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    internal static class TestData
    {
        internal static List<Item> GetList()
        {
            return new List<Item>()
            {
                // new TimeSpan(days, hours, minutes, seconds)
                new Item("chokolade cookie", 10, DateTime.Today.Subtract(new TimeSpan(5,0,0,0))),
                new Item("fløde", 0.5, DateTime.Today),
                new Item("carob cookies", 1001, DateTime.Today.Subtract(new TimeSpan(2,0,0,0)))
            };
        }
    }
}
