using FreezerOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Item item;
        private ICommand deleteItemCommand;
        private ICommand updateItemCommand;
        private static ItemRepository itemRepository = new ItemRepository();
        private static List<ItemViewModel> itemVMRespository = new List<ItemViewModel>();

        public ItemViewModel() // public parameterless constructor needed for users to add rows to the datagrid.
        {
            itemVMRespository.Add(this);
        }

        public ItemViewModel(string name, int number, DateTime dateOfFreezing)
            : this()
        {
            this.name = name;
            this.number = number;
            this.dateOfFreezing = dateOfFreezing;
        }

        public ItemViewModel(Item item) 
            : this(item.Name, item.Number, item.DateOfFreezing)
        {
            this.item = item;
        }

        private Item Item // makes sure that item is never null
        {
            get
            {
                if (item == null)
                {
                    item = new Item(this.Name, this.Number, this.DateOfFreezing);
                }
                return item;
            }
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
                        param => (true)
                        );
                }
                return deleteItemCommand;
            }
        }

        internal void DeleteItem()
        {
            itemRepository.Delete(this.Item);
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (updateItemCommand == null)
                {
                    updateItemCommand = new RelayCommand(
                        param => UpdateItem(),
                        param => (true)
                        );
                }
                return updateItemCommand;
            }
        }

        internal void UpdateItem() 
        {
            Item.Update(name, number, dateOfFreezing);
        }

        internal static bool Exists(ItemViewModel itemVM)
        {
            return itemVMRespository.Contains(itemVM);
        }

        internal static ObservableCollection<ItemViewModel> GetAll()
        {
            return ConvertToObservableCollection(itemRepository.GetAll());
        }

        internal static ObservableCollection<ItemViewModel> Search(string input)
        {
            return ConvertToObservableCollection(itemRepository.Search(input));
        }

        internal static void SaveItems()
        {
            itemRepository.SaveItems();
        }

        internal static ObservableCollection<ItemViewModel> ConvertToObservableCollection(List<Item> itemList)
        {
            var observableCollection = new ObservableCollection<ItemViewModel>();
            foreach (Item item in itemList)
            {
                observableCollection.Add(new ItemViewModel(item));
            }
            return observableCollection;
        }
    }
}
