using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezerOrganizer.ViewModel
{
    public class CommonBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

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

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public virtual void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
    }
}
