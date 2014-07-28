using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreezerOrganizer.Model;

namespace FreezerOrganizer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand closingCommand;
        private SearchViewModel searchViewModel;

        public MainViewModel()
        {
            searchViewModel = new SearchViewModel();
        }

        public ICommand ClosingCommand
        {
            get
            {
                if (closingCommand == null)
                {
                    closingCommand = new RelayCommand(
                        param => Closing(),
                        param => true
                        );
                }
                return closingCommand;
            }
        }

        private void Closing()
        {
            searchViewModel.SaveItems();
        }
    }
}
