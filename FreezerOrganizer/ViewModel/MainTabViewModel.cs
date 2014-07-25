using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.ViewModel
{
    class MainTabViewModel : CommonBase
    {
        private ObservableCollection<TabItemViewModel> tabItems = new ObservableCollection<TabItemViewModel>();

        public ObservableCollection<TabItemViewModel> TabItems
        {
            get
            {
                return tabItems;
            }
        }
    }
}
