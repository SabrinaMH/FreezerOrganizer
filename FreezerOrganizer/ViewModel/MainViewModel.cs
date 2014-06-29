using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreezerOrganizer.Model;

namespace FreezerOrganizer.ViewModel
{
    class MainViewModel : CommonBase
    {
        private ICommand loadCommand, closeCommand;

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

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => Close(),
                        param => true
                        );
                }
                return loadCommand;
            }
        }

        private void Load()
        {
            Services.LoadAllItems();
        }

        private void Close()
        {
            Services.SaveAllItems();
        }
    }
}
