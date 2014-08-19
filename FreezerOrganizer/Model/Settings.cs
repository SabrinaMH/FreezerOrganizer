using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.Model
{
    class Settings : ISettings
    {
        internal object this[string propertyName]
        {
            get 
            {
                return Properties.Settings.Default[propertyName]; 
            }
            private set 
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
