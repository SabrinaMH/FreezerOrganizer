using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;

namespace FreezerOrganizer.ViewModel
{
    class TabItemViewModel : CommonBase
    {
        private string name;

        public TabItemViewModel(string tabName)
        {
            TabName = tabName;
        }

        public string TabName
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
    }
}
