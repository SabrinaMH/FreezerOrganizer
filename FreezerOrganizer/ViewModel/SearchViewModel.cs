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
        private ICommand deleteItemCommand; // should perhaps be placed on ItemViewModel and merged with the method below.
        private ICommand updateItemNumberCommand;

        public SearchViewModel() 
        {
            itemRepository = new ItemRepository();
            UpdateResultsAndSelectedItem(itemRepository.GetAll());
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
                results.CollectionChanged += results_CollectionChanged;
                OnPropertyChanged("Results");
            }
        }

        // gets called whenever items are added/removed from the list, as the collection is then changed.
        private void results_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
            System.Diagnostics.Debug.WriteLine("in propertyChanged");
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
            UpdateResultsAndSelectedItem(resultsAsItems);
        }

        private void UpdateResultsAndSelectedItem(List<Item> list)
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

        internal void SaveItems() 
        {
            itemRepository.SaveItems();
        }
    }
}
