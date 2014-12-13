using System.Windows.Input;
using System.ComponentModel;
using FreezerOrganizer.ViewModel.BaseClasses;

namespace FreezerOrganizer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private SettingsViewModel _settingsViewModel;
        private SearchViewModel _searchViewModel;
        private ICommand _closingCommand;
        private bool databaseSerialization = true; // todo: perhaps this choice shouldn't be stored here.
        private string resourcePath;
        private bool _isDatabaseSerializationEnabled = false;

        public MainViewModel()
        {
            /* Necessary to check, because in SourceUpdated the _itemRepository hasn't been created at design time.
             * Do not check in release mode as it slows down production code.
             * */
            #if DEBUG
            if (IsInDesignMode())
            {
                return;
            }
            #endif
            
            // todo - maybe use DI?
            // problem here. It's the views responsibility to initialize viewmodels - not other viewmodels.
            _searchViewModel = new SearchViewModel();

            if (!databaseSerialization)
            {
                _settingsViewModel = new SettingsViewModel();
                _settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
                resourcePath = _settingsViewModel.Path;
                isDatabaseSerializationEnabled = false;
            }
            else 
            {
                isDatabaseSerializationEnabled = true;
                resourcePath = "http://cssx.dk/Sabrina/?action=/items"; // todo: clean up. the path to database shouldn't be hardcoded here.
            }

            // explicit call because if the resourcePath is prefilled, then PropertyChanged won't be raised.
            _searchViewModel.SourceUpdated(resourcePath);
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Path")
            {
                SearchViewModel.SourceUpdated(resourcePath);
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return _settingsViewModel; }
        }

        public SearchViewModel SearchViewModel
        {
            get { return _searchViewModel; }
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
            if (SettingsViewModel != null)
            {
                SettingsViewModel.Save();
            }

            SearchViewModel.Save(resourcePath);
        }

        public bool isDatabaseSerializationEnabled
        {
            get { return _isDatabaseSerializationEnabled; }
            set
            {
                if (_isDatabaseSerializationEnabled != value)
                {
                    _isDatabaseSerializationEnabled = value;
                    OnPropertyChanged("isDatabaseSerializationEnabled");
                }
            }
        }
    }
}