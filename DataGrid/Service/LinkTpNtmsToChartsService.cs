using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid.Service
{
    public class LinkTpNtmsToChartsService
    {
        private static LinkTpNtmsToChartsService _instanse = new LinkTpNtmsToChartsService();
        public static LinkTpNtmsToChartsService Instanse => _instanse;
        private LinkTpNtmsToChartsService()
        {
        }

        public void LinkTpsToCharts(List<TpNtm> tps, List<Chart> charts)
        {
            int maxTpsInChart = 10;
            int tpsInChart = tps.Count <= maxTpsInChart ? tps.Count : maxTpsInChart;
            int curTpInd = 0;
            if (tpsInChart > 0)
            {
                for (int i = 0; i < charts.Count; ++i)
                {
                    var chart = charts[i];
                    var curTpsInChart = (i + tpsInChart) % (tpsInChart + 1);

                    while (curTpsInChart > 0)
                    {
                        if (curTpInd >= tps.Count)
                        {
                            curTpInd = 0;
                        }
                        var tp = tps[curTpInd];
                        chart.Tps.Add(tp);
                        if (tp.Charts.IndexOf(chart) < 0)
                        {
                            tp.Charts.Add(chart);
                        }
                        ++curTpInd;
                        --curTpsInChart;
                    }
                }
            }
        }
    }
}
