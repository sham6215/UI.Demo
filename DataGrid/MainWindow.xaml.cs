using DataGrid.Models;
using DataGrid.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// This list will not be changed an it is the reason why its is List.
        /// If the list is changable, it should be ObservableCollection
        /// </summary>
        List<TpNtm> TpsAll { get; set; } = null;
        /// <summary>
        /// This list will not be changed an it is the reason why its is List.
        /// If the list is changable, it should be ObservableCollection
        /// </summary>
        List<Chart> ChartsAll { get; set; } = null;

        public List<Order> Orders { get; set; }

        private ListCollectionView _tpsView;
        public ListCollectionView TpsView
        {
            get { return _tpsView; }
            set { _tpsView = value; }
        }

        private ListCollectionView _chartsView;
        public ListCollectionView ChartsView
        {
            get { return _chartsView; }
            set { _chartsView = value; }
        }


        public MainWindow()
        {
            Init();
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Init()
        {
            TpsAll = GenerateTpNtmService.Instanse.GenerateCharts(50);
            ChartsAll = GenerateChartService.Instanse.GenerateTpNtms(35);
            LinkTpNtmsToChartsService.Instanse.LinkTpsToCharts(TpsAll, ChartsAll);

            TpsView = CollectionViewSource.GetDefaultView(TpsAll) as ListCollectionView;
            ChartsView = CollectionViewSource.GetDefaultView(ChartsAll) as ListCollectionView;
        }
    }
}
