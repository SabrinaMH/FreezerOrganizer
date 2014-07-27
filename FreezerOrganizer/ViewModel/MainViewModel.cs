using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreezerOrganizer.Model;

namespace FreezerOrganizer.ViewModel
{
    public class MainViewModel : CommonBase
    {
        private ICommand closeCommand;
        private SearchViewModel resultsViewModel;

        public MainViewModel()
        {
            resultsViewModel = new SearchViewModel();
        }

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(
                        param => Close(),
                        param => true
                        );
                }
                return closeCommand;
            }
        }

        public void Load()
        {
            //AllItems = Services.LoadItems();
            ItemRepository.LoadItems();
        }

        private void Close()
        {
            ItemRepository.SaveItems();
        }
    }
}
