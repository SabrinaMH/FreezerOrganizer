using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FreezerOrganizer.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        private string input;
        private ObservableCollection<ItemViewModel> results;
        private ItemViewModel selectedItem;
        private ICommand searchCommand;

        public SearchViewModel() 
        {
            results = new ObservableCollection<ItemViewModel>();
            UpdateResults(ItemViewModel.GetAll());
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
            private get
            {
                return results;
            }
            set
            {
                results = value;
                results.CollectionChanged += results_CollectionChanged;
                // the event subscriptions should also be updated, when the reference of the OC is changed.
                UpdateEventSubscriptions(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, results)); 
                OnPropertyChanged("Results");
            }
        }

        // gets called whenever items are added/removed from the list, as the collection is then changed.
        private void results_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateEventSubscriptions(e);
        }

        private void UpdateEventSubscriptions(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ItemViewModel itemVM in e.NewItems)
                {
                    itemVM.Add();
                    itemVM.PropertyChanged += itemViewModel_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (ItemViewModel itemVM in e.OldItems)
                {
                    itemVM.DeleteItem();
                    itemVM.PropertyChanged -= itemViewModel_PropertyChanged;
                }
            }
        }

        private void itemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var itemVM = (ItemViewModel)sender; 
            itemVM.UpdateItem();
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
            var resultsAsItems = ItemViewModel.Search(Input);
            UpdateResults(resultsAsItems);
        }

        private void UpdateResults(ObservableCollection<ItemViewModel> results)
        {
            Results = results;
            SelectedItem = Results.Count > 0 ? Results[0] : null;
        }

        internal void SaveItems() 
        {
            ItemViewModel.SaveItems();
        }
    }
}
