using FreezerOrganizer.Model;
using FreezerOrganizer.ViewModel.Helpers;
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
        private Item _item = null;
        private static ItemRepository _itemRepository;

        public ItemViewModel() // public parameterless constructor needed for users to add rows to the datagrid.
        { }

        public ItemViewModel(Item item) 
        {
            this._item = item;
        }

        public static void SetRepository(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Item Item // make sure that item is never null
        {
            get
            {
                if (_item == null)
                {
                    _item = _itemRepository.CreateNewItem();
                }
                return _item;
            }
        }

        public string Name
        {
            get { return Item.Name; }
            set
            {
                if (Item.Name != value)
                {
                    Item.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public double Number
        {
            get { return Item.Number; }
            set
            {
                if (Item.Number != value)
                {
                    if (value == 0)
                    {
                        DeleteItem();
                    }
                    else
                    {
                        Item.UpdateNumber(value);
                        OnPropertyChanged("Number");
                    }
                }
            }
        }

        public DateTime DateOfFreezing
        {
            get { return Item.DateOfFreezing; }
            set
            {
                if (Item.DateOfFreezing != value)
                {
                    Item.DateOfFreezing = value;
                    OnPropertyChanged("DateOfFreezing");
                }
            }
        }

        internal void DeleteItem()
        {
            _itemRepository.Delete(this.Item);
        }
    }
}
