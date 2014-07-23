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
    public class SearchViewModel : CommonBase
    {
        private string input;
        private ObservableCollection<ItemViewModel> results; // perhaps it should be of ItemViewModel and not Item
        private ItemViewModel selectedItem;
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
        
        public ObservableCollection<ItemViewModel> Results
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

        public ItemViewModel SelectedItem
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
                    searchCommand = new RelayCommand(param => Search(Input));
                }
                return searchCommand;
            }
        }

        private void Search(string input)
        {
            var resultsAsItemViewModels = new ObservableCollection<ItemViewModel>();
            var resultsAsItems = Services.Search(Input);

            foreach (Item item in resultsAsItems)
            {
                resultsAsItemViewModels.Add(new ItemViewModel(item.Name, item.Number, item.DateOfFreezing));
            }

            Results = resultsAsItemViewModels;
            SelectedItem = Results.Count > 0 ? Results[0] : null;
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
