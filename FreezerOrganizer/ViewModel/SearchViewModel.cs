using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreezerOrganizer.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace FreezerOrganizer.ViewModel
{
    class SearchViewModel : CommonBase
    {
        private string input;
        private ObservableCollection<Item> results;
        private Item selectedItem;
        private ICommand searchCommand;

        public SearchViewModel()
        {
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
        
        public ObservableCollection<Item> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                OnPropertyChanged("Results");
            }
        }


        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(
                        param => Search((string)param),
                        param => (Input != null)
                        );
                }
                return searchCommand;
            }
        }

        public Item SelectedItem
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

        private void Search(string input)
        {
            // do something to get results (deserialization)
            // var results = new JavascriptSerializer( ).Deserialize<List<string>>( searchParamThatWasInConstructor );
            // That's just a fake example

            var RetrievedItems = new ObservableCollection<Item>(results);
            SelectedItem = RetrievedItems.Count > 0 ? RetrievedItems[0] : null;
        }
    }
}
