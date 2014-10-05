using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.ViewModel.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        protected virtual void OnPropertyChanged(params string[] parameters)
        {
            if (PropertyChanged != null)
            {
                foreach (var param in parameters)
                {
                    VerifyPropertyName(param);
                    PropertyChanged(this, new PropertyChangedEventArgs(param));
                }
            }
        }

        // Verify that the property name matches a real, public instance property on this object.
        [Conditional("DEBUG")]
        //[DebuggerStepThrough]
        public virtual void VerifyPropertyName(string propertyName)
        {
            bool foundProperty = false;
            var properties = this.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name == propertyName)
                {
                    foundProperty = true;
                    break;
                }
            }

            if (!foundProperty)
            {
                string msg = "Invalid property name: " + propertyName;
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}
