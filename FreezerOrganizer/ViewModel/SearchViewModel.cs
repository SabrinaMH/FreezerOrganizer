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
        private ObservableCollection<ItemViewModel> results; 
        private ItemViewModel selectedItem;
        private ICommand searchCommand;
        private ICommand deleteItemCommand; // should perhaps be placed on ItemViewModel and merged with the method below.
        private ICommand updateItemNumberCommand;

        public SearchViewModel() { }

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
            var resultsAsItems = ItemRepository.Search(Input);

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
