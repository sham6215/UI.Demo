using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Styles.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public string Label1Text
        {
            get { return (string)GetValue(Label1TextProperty); }
            set { SetValue(Label1TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label1Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Label1TextProperty =
            DependencyProperty.Register(
                "Label1Text",
                typeof(string),
                typeof(UserControl1),
                new PropertyMetadata(string.Empty, Label1TextChanged),
                new ValidateValueCallback(Label1TextValidate));

        /// <summary>
        /// TODO: Implement Style changing when validation is failed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool Label1TextValidate(object value)
        {
            if ((value as string)?.Equals("123") ?? true)
            {
                return false;
            }
            return true;
        }

        private static void Label1TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControl1 uc = d as UserControl1;
            uc.Label1.Content = e.NewValue as string;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public UserControl1()
        {
            InitializeComponent();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
