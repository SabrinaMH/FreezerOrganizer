using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Windows.Input;

namespace FreezerOrganizer.ViewModel
{
    class AddViewModel : CommonBase
    {
        private string name;
        private int number;
        private DateTime dateOfFreezing;
        private ICommand addItemCommand;
        private MainViewModel parentViewModel;

        public AddViewModel() { }

        public AddViewModel(MainViewModel parentViewModel)
        {
            this.ParentViewModel = parentViewModel;
        }
        
        public MainViewModel ParentViewModel
        {
            get
            {
                return parentViewModel;
            }
            set
            {
                if (parentViewModel != value)
                {
                    parentViewModel = value;
                    OnPropertyChanged("ParentViewModel");
                }
            }
        }

        public string Name
        {
            get;
            set;
        }

        // Add a 'No portions' at a later time to supplement Number.
        public int Number
        {
            set { number = value; }
        }

        public DateTime DateOfFreezing
        {
            get { if (dateOfFreezing == null) return DateTime.Today; else return dateOfFreezing; }
            set { dateOfFreezing = value; }
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand(
                        param => AddItem(),
                        param => (Name != null)
                        );
                }
                return addItemCommand;
            }
        }

        private void AddItem() { }

    }
}
