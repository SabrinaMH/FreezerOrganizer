 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Model
{
    [DataContract()]
    public class Item 
    {
        [DataMember()]
        public string Name { get; private set; }
        [DataMember()]
        public int Number { get; private set; }
        [DataMember()]
        public DateTime DateOfFreezing { get; private set; }

        private Item() { }

        public Item(string name, int number, DateTime dateOfFreezing)
        {
            this.Name = name;
            this.Number = number;
            this.DateOfFreezing = dateOfFreezing;
        }

        internal void Update(string name, int number, DateTime dateOfFreezing)
        {
            this.Name = name;
            this.Number = number;
            this.DateOfFreezing = dateOfFreezing;
        }

        internal bool IsMatch(string name, int number, DateTime dateOfFreezing)
        {
            return this.Name == name && this.Number == number && this.DateOfFreezing == dateOfFreezing;
        }

        internal void CreateItem
    }
}
