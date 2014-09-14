using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Model
{
    class Settings : ISettings
    {
        public object this[string propertyName]
        {
            get 
            {
                return Properties.Settings.Default[propertyName];
            }
            set 
            {
                Properties.Settings.Default[propertyName] = value;
            }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
