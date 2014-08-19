using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreezerOrganizer.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FreezerOrganizer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _input = "";
        private ObservableCollection<ItemViewModel> _results;
        private ItemViewModel _selectedItem;
        private ItemRepository _itemRepository;
        private SettingsViewModel _settingsViewModel;
        private ICommand _searchCommand;
        private ICommand _closingCommand;

        public MainViewModel(string path)
        { 
            _itemRepository = new ItemRepository(); 
            Results = ConvertToObservableCollection(_itemRepository.Load(path));
            SelectedItem = Results.Count > 0 ? Results[0] : null;
            ItemViewModel.SetRepository(_itemRepository);
            _settingsViewModel = new SettingsViewModel();
            
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return _settingsViewModel;
            }
        }

        public string Input
        {
            get { return _input; }
            set
            {
                if (_input != value)
                {
                    _input = value;
                    OnPropertyChanged("Input");
                }
            }
        }

        public ObservableCollection<ItemViewModel> Results
        {
            private get { return _results; }
            set
            {
                _results = value;
                _results.CollectionChanged += results_CollectionChanged;
                OnPropertyChanged("Results");
            }
        }

        // gets called whenever items are added/removed from the list, as the collection is then changed.
        private void results_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                DeleteItems(e);
            }
        }

        private void DeleteItems(NotifyCollectionChangedEventArgs e)
        {
            foreach (ItemViewModel itemVM in e.OldItems)
            {
                itemVM.DeleteItem();
            }
        }

        public ItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => Search(Input));
                }
                return _searchCommand;
            }
        }

        private void Search(string input)
        {
            var resultsAsItems = _itemRepository.Search(Input);
            Results = ConvertToObservableCollection(resultsAsItems);
        }

        private ObservableCollection<ItemViewModel> ConvertToObservableCollection(List<Item> itemList)
        {
            var observableCollection = new ObservableCollection<ItemViewModel>();
            foreach (Item item in itemList)
            {
                observableCollection.Add(new ItemViewModel(item));
            }
            return observableCollection;
        }

        public ICommand ClosingCommand
        {
            get
            {
                if (_closingCommand == null)
                {
                    _closingCommand = new RelayCommand(
                        param => Closing(),
                        param => true
                        );
                }
                return _closingCommand;
            }
        }

        private void Closing()
        {
            _itemRepository.Save();
            SettingsViewModel.Save();
        }
    }
}