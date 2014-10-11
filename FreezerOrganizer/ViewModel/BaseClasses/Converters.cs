using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.ViewModel.BaseClasses
{
    internal static class Converters
    {
        internal static ObservableCollection<Tout> ConvertToObservableCollection<Tin, Tout>(IEnumerable<Tin> input)
            where Tin : class 
            where Tout : class
        {
            var observableCollection = new ObservableCollection<Tout>();
            foreach (Tin element in input)
            {
                observableCollection.Add((Tout)Activator.CreateInstance(typeof(Tout), new Object[]{ element }));
            }
            return observableCollection;
        }
    }
}
