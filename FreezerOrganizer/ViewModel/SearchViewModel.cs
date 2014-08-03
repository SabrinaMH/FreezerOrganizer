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
        private ItemRepository itemRepository;
        private ItemViewModel selectedItem;
        private ICommand searchCommand;

        public SearchViewModel() 
        {
            results = new ObservableCollection<ItemViewModel>();
            itemRepository = new ItemRepository();
            UpdateResults(itemRepository.GetAll());
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
                    itemRepository.Add(new Item(itemVM.Name, itemVM.Number, itemVM.DateOfFreezing));
                    itemVM.PropertyChanged += itemViewModel_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (ItemViewModel itemVM in e.OldItems)
                {
                    itemRepository.Remove(itemVM.Name, itemVM.Number, itemVM.DateOfFreezing);
                    itemVM.PropertyChanged -= itemViewModel_PropertyChanged;
                }
            }
        }

        private void itemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var itemVM = (ItemViewModel)sender; // sabrh: differs from selectedItem?
            itemRepository.Update(itemVM.Name, itemVM.Number, itemVM.DateOfFreezing);
            // sabrh: is sender the updated item, while this is the old?
            // sabrh: shit. selectedItem is an itemviewmodel. No help in finding the item in the itemrepository list.
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
            var resultsAsItems = itemRepository.Search(Input);
            UpdateResults(resultsAsItems);
        }

        private void UpdateResults(List<Item> list)
        {
            Results = ConvertToObservableCollection(list);
            SelectedItem = Results.Count > 0 ? Results[0] : null;
        }

        private ObservableCollection<ItemViewModel> ConvertToObservableCollection(List<Item> list)
        {
            var observableCollection = new ObservableCollection<ItemViewModel>();
            foreach (Item item in list)
            {
                observableCollection.Add(new ItemViewModel(item.Name, item.Number, item.DateOfFreezing));
            }

            return observableCollection;
        }

        internal void SaveItems() 
        {
            itemRepository.SaveItems();
        }
    }
}
