using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreezerOrganizer.View
{
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, EventArgs e)
        {
            input_TextBox.Focus();
        }

        private void ResultsGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Enter is pressed on the last row with data
                var nextItem = ResultsGrid.Items[ResultsGrid.Items.IndexOf(ResultsGrid.Items.CurrentItem) + 1];
                if (nextItem.ToString() == Converters.IgnoreNewItemPlaceholderConverter._newItemPlaceholderName)
                {
                    if (ResultsGrid.CurrentColumn != ResultsGrid.Columns[0])
                    {
                        ResultsGrid.CurrentColumn = ResultsGrid.Columns[0];
                    }
                }
            }
        }
    }
}
