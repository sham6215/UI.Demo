using DataGrid.Models;
using DataGrid.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        private CollectionShowStatus _showHoldingsStatus = CollectionShowStatus.All;
        public CollectionShowStatus ShowCollectionStatus
        {
            get { return _showHoldingsStatus; }
            set
            {
                if (_showHoldingsStatus != value)
                {
                    _showHoldingsStatus = value;
                    OnPropertyChanged();
                    TpsChartsView.Refresh();
                    TpsView.Refresh();
                }
            }
        }

        private TpShowStatus _showTpStatus = TpShowStatus.All;
        public TpShowStatus ShowTpStatus
        {
            get { return _showTpStatus; }
            set
            {
                if (_showTpStatus != value)
                {
                    _showTpStatus = value;
                    OnPropertyChanged();
                    TpsView?.Refresh();
                }
            }
        }

        #region Tps View

        private ListCollectionView _tpsView;
        public ListCollectionView TpsView
        {
            get { return _tpsView; }
            set { _tpsView = value; }
        }

        public TpNtm CurrentTpNtm
        {
            get
            {
                return TpsView?.CurrentItem as TpNtm;
            }
            set
            {
                TpsView.MoveCurrentTo(value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Chart> ObsTpsCharts { get; set; } = new ObservableCollection<Chart>(new List<Chart>());

        private ListCollectionView _tpsChartsView;
        public ListCollectionView TpsChartsView
        {
            get
            {
                return _tpsChartsView;
            }
            set
            {
                if (_tpsChartsView != value)
                {
                    _tpsChartsView = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

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
            ChartsAll = GenerateChartService.Instanse.GenerateCharts(35);
            LinkTpNtmsToChartsService.Instanse.LinkTpsToCharts(TpsAll, ChartsAll);

            TpsChartsView = CollectionViewSource.GetDefaultView(ObsTpsCharts) as ListCollectionView;
            TpsChartsView.Filter = ChartsViewFilter;

            TpsView = CollectionViewSource.GetDefaultView(TpsAll) as ListCollectionView;
            TpsView.CurrentChanged += (s, e) =>
            {

                OnPropertyChanged(nameof(CurrentTpNtm));
                ObsTpsCharts.Clear();
                CurrentTpNtm?.Charts.ForEach(c => ObsTpsCharts.Add(c));
                TpsChartsView.Refresh();
            };
            TpsView.Filter = TpsViewFilter;


            ChartsView = CollectionViewSource.GetDefaultView(ChartsAll) as ListCollectionView;
        }

        private bool ChartsViewFilter(object obj)
        {
            Chart chart = obj as Chart;
            bool show = true;
            if (chart != null)
            {
                /// Checking Tp status: Cancelled, InForce
                if (show)
                {
                    if ((ShowCollectionStatus == CollectionShowStatus.InCollection && !chart.InCollection) ||
                        (ShowCollectionStatus == CollectionShowStatus.NotInCollection && chart.InCollection))
                    {
                        show &= false;
                    }
                }
            }
            else
            {
                show &= false;
            }
            return show;
        }

        private bool TpsViewFilter(object obj)
        {
            TpNtm tp = obj as TpNtm;
            bool show = true;
            if (tp != null)
            {
                if (show)
                {
                    if (ShowTpStatus != TpShowStatus.All)
                    {
                        var status = Convert(tp.Status);
                        if (status == null || ShowTpStatus != status)
                        {
                            show &= false;
                        }
                    }
                }


                /// Checking Tp InCollection Status
                if (show)
                {
                    if ((ShowCollectionStatus == CollectionShowStatus.InCollection && !tp.InCollection) ||
                        (ShowCollectionStatus == CollectionShowStatus.NotInCollection && tp.InCollection))
                    {
                        show &= false;
                    }
                }
            }
            else
            {
                show = false;
            }
            return show;
        }

        private TpShowStatus? Convert(NtmStatus ntmStatus)
        {
            if (ntmStatus == NtmStatus.Cancelled)
                return TpShowStatus.Cancelled;
            if (ntmStatus == NtmStatus.InForce)
                return TpShowStatus.InForce;
            return null;
        }

        private void TpsGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<string> columns = new List<string>()
            {
                "Number",
                "PublicationDate"
            };
            UpdateViewSorting(TpsView, columns, e.OriginalSource);
        }

        /// <summary>
        /// Adjusts sorting for the passed view
        /// </summary>
        /// <param name="view">The view to adjust sorting</param>
        /// <param name="sortingColumns"></param>
        /// <param name="currentColumnElement"></param>
        private void UpdateViewSorting(ListCollectionView view, List<string> sortingColumns, object currentColumnElement)
        {
            DataGridColumnHeader columnHeader = GetColumnHeaderFromElement(currentColumnElement);
            if (columnHeader != null)
            {
                string header = columnHeader.Column.Header.ToString();
                if (sortingColumns.Exists(c => c == header))
                {
                    var sd = view.SortDescriptions.FirstOrDefault(s => s.PropertyName == header);
                    if (sd.PropertyName == null)
                    {
                        view.SortDescriptions.Add(new SortDescription(header, ListSortDirection.Ascending));
                        columnHeader.Column.SortDirection = ListSortDirection.Ascending;
                    }
                    else if (sd.Direction == ListSortDirection.Ascending)
                    {
                        int index = view.SortDescriptions.IndexOf(sd);
                        view.SortDescriptions[index] = new SortDescription(header, ListSortDirection.Descending);
                        columnHeader.Column.SortDirection = ListSortDirection.Descending;
                    }
                    else
                    {
                        view.SortDescriptions.Remove(sd);
                        columnHeader.Column.SortDirection = null;
                    }
                }
            }
        }

        private DataGridColumnHeader GetColumnHeaderFromElement(object source)
        {
            DependencyObject dep = source as DependencyObject;
            while ((dep != null)
                && !(dep is DataGridCell)
                && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
            {
                return null;
            }
            return dep as DataGridColumnHeader;
        }

        private void TpsChartsGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<string> columns = new List<string>()
            {
                "Number",
                "EditionDate"
            };
            UpdateViewSorting(TpsChartsView, columns, e.OriginalSource);
        }
    }
}
