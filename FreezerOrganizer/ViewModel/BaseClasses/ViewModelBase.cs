using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreezerOrganizer.ViewModel.BaseClasses
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool ThrowOnInvalidPropertyName;

        public virtual void OnPropertyChanged(params string[] parameters)
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
        private void VerifyPropertyName(string propertyName)
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

        internal bool IsInDesignMode()
        {
            var designProperty = DesignerProperties.IsInDesignModeProperty;
            return (bool)DependencyPropertyDescriptor.FromProperty(designProperty, typeof(FrameworkElement)).Metadata.DefaultValue;
        }
    }
}
