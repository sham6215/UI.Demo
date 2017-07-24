﻿using System;
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

namespace DropDownButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DButton.Content = ((RadioButton)sender).Tag;
            DButton.IsOpen = false;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            SButton.Content = ((RadioButton)sender).Tag;
            SButton.IsOpen = false;
        }
    }
}
