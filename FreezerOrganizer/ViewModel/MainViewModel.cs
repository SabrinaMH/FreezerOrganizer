using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreezerOrganizer.Model;

namespace FreezerOrganizer.ViewModel
{
    class MainViewModel : CommonBase
    {
        private ICommand closeCommand;
        private List<Item> allItems;
        private SearchViewModel searchViewModel;
        private AddViewModel addViewModel;

        public MainViewModel()
        {
            searchViewModel = new SearchViewModel(this);
            addViewModel = new AddViewModel(this);
        }

        public List<Item> AllItems
        {
            get
            {
                return allItems;
            }
            set
            {
                allItems = value;
                OnPropertyChanged("AllItems");
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(
                        param => Close(),
                        param => true
                        );
                }
                return closeCommand;
            }
        }

        public void Load()
        {
            //AllItems = Services.LoadItems();
            Services.LoadItems();
        }

        private void Close()
        {
            Services.SaveItems(AllItems);
        }
    }
}
