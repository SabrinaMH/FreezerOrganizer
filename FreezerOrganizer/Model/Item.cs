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
        // sabrh todo: research - want to update model object. Are public setters the way to go?
        [DataMember()]
        public string Name { get; set; }
        [DataMember()]
        public double Number { get; private set; }
        [DataMember()]
        public DateTime DateOfFreezing { get; set; }

        private Item() { }

        public Item(string name, double number, DateTime dateOfFreezing)
        {
            this.Name = name;
            this.Number = number;
            this.DateOfFreezing = dateOfFreezing.Date;
        }

        internal bool Equals(Item item)
        {
            bool sameDate = item.DateOfFreezing.Month == this.DateOfFreezing.Month && item.DateOfFreezing.Year == this.DateOfFreezing.Year;
            return item.Name == this.Name && sameDate;
        }

        // created an update method, because it's used several places (extra benefit: enables private setter)
        internal void UpdateNumber(double newNumber)
        {
            Number = newNumber;
        }
    }
}
