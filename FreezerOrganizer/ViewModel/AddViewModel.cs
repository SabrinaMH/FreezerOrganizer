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
        private Item item;
        private ICommand addItemCommand;
        private ICommand deleteItemCommand;
        private ICommand updateItemNumberCommand;

        public AddViewModel()
        {
        }

        public Item Item 
        {
            get
            {
                return item;
            }
            set
            {
                if (item != value)
                {
                    item = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand(
                        param => AddItem(),
                        param => (Item != null)
                        );
                }
                return addItemCommand;
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
                        param => (Item != null)
                        );
                }
                return deleteItemCommand;
            }
        }

        public ICommand UpdateItemNumberCommand
        {
            get
            {
                if (updateItemNumberCommand == null)
                {
                    updateItemNumberCommand = new RelayCommand(
                        param => UpdateItem(),
                        param => (Item != null)
                        );
                }
                return updateItemNumberCommand;
            }
        }

        private void AddItem() { }

        private void DeleteItem() { }

        private void UpdateItem() { }
    }
}
