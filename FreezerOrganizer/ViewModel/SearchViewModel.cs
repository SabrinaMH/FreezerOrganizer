using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace FreezerOrganizer.ViewModel
{
    class SearchViewModel : CommonBase
    {
        private string input;
        private ObservableCollection<Item> results;
        private Item selectedItem;
        private ICommand searchCommand;
        private ICommand deleteItemCommand;
        private ICommand updateItemNumberCommand;
        private MainViewModel parentViewModel;

        public SearchViewModel() { }

        public SearchViewModel(MainViewModel parentViewModel)
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

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                if (input != value)
                {
                    input = value;
                    OnPropertyChanged("Input");
                }
            }
        }
        
        public ObservableCollection<Item> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                OnPropertyChanged("Results");
            }
        }

        public Item SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(param => Search((string)param));
                }
                return searchCommand;
            }
        }

        private void Search(string input)
        {
            results = new ObservableCollection<Item>(Services.Search(input));
            SelectedItem = results.Count > 0 ? results[0] : null;
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
