using DataGrid.Models;
using System;
using System.Collections.Generic;
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

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Order> Orders { get; set; }

        public MainWindow()
        {
            Init();
            InitializeComponent();
        }

        private void Init()
        {
            Orders = new List<Order>() {
                new Order() { Id = 1, Guid = "Guid1", Name = "Name1", Reference = "Ref1" },
                new Order() { Id = 2, Guid = "Guid2", Name = "Name2", Reference = "Ref2" },
                new Order() { Id = 3, Guid = "Guid3", Name = "Name3", Reference = "Ref3" },
                new Order() { Id = 4, Guid = "Guid4", Name = "Name4", Reference = "Ref4" },
                new Order() { Id = 5, Guid = "Guid5", Name = "Name5", Reference = "Ref5" },
            };
        }
    }
}
