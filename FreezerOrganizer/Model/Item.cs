using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public enum Units { bag, dl, g, pc, serv };

        // sabrh todo: research - want to update model object. Are public setters the way to go?
        [DataMember()]
        public string Name { get; set; }
        [DataMember()]
        public double Number { get; private set; }
        [DataMember()]
        public DateTime DateOfFreezing { get; set; } // nullable such that the default value is NULL and not some weird date
        [DataMember()]
        public Units Unit { get; set; }

        public static readonly DateTime defaultDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public Item(string name, double number, Units unit)
        {
            this.Name = name;
            this.Number = number;
            this.Unit = unit;
            this.DateOfFreezing = defaultDate;
        }

        [JsonConstructor]
        public Item(string name, double number, Units unit, DateTime dateOfFreezing) : this(name, number, unit)
        {
            this.DateOfFreezing = dateOfFreezing.Date;
        }

        internal bool Equals(Item item)
        {
            bool sameDate = (item.DateOfFreezing.Month == this.DateOfFreezing.Month && item.DateOfFreezing.Year == this.DateOfFreezing.Year);
            return item.Name == this.Name && item.Unit == this.Unit && sameDate;
        }

        // created an update method, because it's used several places (extra benefit: enables private setter)
        internal void UpdateNumber(double newNumber)
        {
            Number = newNumber;
        }
    }
}
