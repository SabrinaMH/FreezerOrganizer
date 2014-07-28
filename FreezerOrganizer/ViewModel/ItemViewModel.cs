using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private string name;
        private int number;
        private DateTime dateOfFreezing;

        public ItemViewModel() { } // Empty constructor needed for users to add rows to the datagrid.

        public ItemViewModel(string name, int number, DateTime dateOfFreezing) 
        {
            this.name = name;
            this.number = number;
            this.dateOfFreezing = dateOfFreezing;
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

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public DateTime DateOfFreezing
        {
            get
            {
                return dateOfFreezing;
            }
            set
            {
                if (dateOfFreezing != value)
                {
                    dateOfFreezing = value;
                    OnPropertyChanged("DateOfFreezing");
                }
            }
        }
    }
}
