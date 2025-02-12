using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WPF_TEST.Extends
{
    public class PasswordExtend
    {
        #region IsPasswordBindable

        public static bool GetIsPasswordBindable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindableProperty);
        }

        public static void SetIsPasswordBindable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindableProperty, value);
        }

        public static readonly DependencyProperty IsPasswordBindableProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindable", typeof(bool), typeof(PasswordExtend), new PropertyMetadata(false, IsPasswordBindableChanged));

        private static void IsPasswordBindableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PasswordBox passwordBox)) return;

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
            else
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            }
        }
        #endregion


        #region BindablePassword

        public static string GetBindablePassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindablePasswordProperty);
        }

        public static void SetBindablePassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindablePasswordProperty, value);
        }

        public static readonly DependencyProperty BindablePasswordProperty =
            DependencyProperty.RegisterAttached("BindablePassword", typeof(string), typeof(PasswordExtend), new PropertyMetadata(string.Empty));

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            SetBindablePassword(passwordBox, passwordBox.Password);
        }

        #endregion
    }
}
