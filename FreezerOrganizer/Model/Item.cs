using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Model
{
    [Serializable()]
    public class Item 
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public DateTime DateOfFreezing { get; private set; }
    
        public Item(string name, int number, DateTime dateOfFreezing)
        {
            this.Name = name;
            this.Number = number;
            this.DateOfFreezing = dateOfFreezing;
        }
    }
}
