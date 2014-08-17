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
        public int Number { get; private set; }
        [DataMember()]
        public DateTime DateOfFreezing { get; set; }

        private Item() { }

        public Item(string name, int number, DateTime dateOfFreezing)
        {
            this.Name = name;
            this.Number = number;
            this.DateOfFreezing = dateOfFreezing.Date;
        }

        internal bool IsMatch(string name, int number, DateTime dateOfFreezing)
        {
            // not interested in the specific time, only the date.
            return this.Name == name && this.Number == number && this.DateOfFreezing == dateOfFreezing;
        }

        // created an update method, because it's used several places (extra benefit: enables private setter)
        internal void UpdateNumber(int newNumber)
        {
            Number = newNumber;
        }
    }
}
