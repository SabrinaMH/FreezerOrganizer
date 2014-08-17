using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FreezerOrganizer.View
{
    /* Anders: should this be an abstract class and then for each shortcut, 
     * we have a class inheriting from this and overriding Control_KeyDown?
     * */
    class Shortcuts : Behavior<UIElement>
    {
        public static DependencyProperty ExecuteCommandProperty = DependencyProperty.Register(
            "ExecuteCommand", typeof(ICommand), typeof(Shortcuts));

        public static DependencyProperty ExecuteCommandPropertyParameter = DependencyProperty.Register(
            "ExecuteCommandParameter", typeof(object), typeof(Shortcuts));

        private UIElement _control;

        public ICommand ExecuteCommand
        {
            get { return (ICommand)GetValue(ExecuteCommandProperty); }
            set { SetValue(ExecuteCommandProperty, value); }
        }

        public object ExecuteCommandParameter
        {
            get { return GetValue(ExecuteCommandPropertyParameter); }
            set { SetValue(ExecuteCommandPropertyParameter, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            _control = AssociatedObject;
            _control.KeyDown += Control_KeyDown;
        }

        protected override void OnDetaching()
        {
            _control.KeyDown -= Control_KeyDown;
            _control = null;
            base.OnDetaching();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //case Key.Ctr
            }
        }
    }
}
