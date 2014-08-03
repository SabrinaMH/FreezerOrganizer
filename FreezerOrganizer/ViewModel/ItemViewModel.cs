using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FreezerOrganizer.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private string name;
        private int number;
        private DateTime dateOfFreezing;
        private ICommand deleteItemCommand;
        private ICommand updateItemCommand;

        public ItemViewModel() { } // public empty constructor needed for users to add rows to the datagrid.

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



        public ICommand DeleteItemCommand
        {
            get
            {
                if (deleteItemCommand == null)
                {
                    deleteItemCommand = new RelayCommand(
                        param => DeleteItem(),
                        param => (SelectedItem != null)
                        );
                }
                return deleteItemCommand;
            }
        }

        private void DeleteItem()
        {

        }

        public ICommand UpdateItemNumberCommand
        {
            get
            {
                if (updateItemNumberCommand == null)
                {
                    updateItemNumberCommand = new RelayCommand(
                        param => UpdateItem(),
                        param => (SelectedItem != null)
                        );
                }
                return updateItemNumberCommand;
            }
        }

        private void UpdateItem() { }

    }
}
