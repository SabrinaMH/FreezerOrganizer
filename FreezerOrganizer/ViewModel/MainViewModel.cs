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
            _settingsViewModel = new SettingsViewModel();
            _settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
            // explicit call because if the Path setting is prefilled, then PropertyChanged won't be raised.
            _searchViewModel.SourceUpdated(_settingsViewModel.Path);
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Path")
            {
                SearchViewModel.SourceUpdated(_settingsViewModel.Path);
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
            SearchViewModel.Save(SettingsViewModel.Path);
            SettingsViewModel.Save();
        }
    }
}