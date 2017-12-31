using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid.Service
{
    public class GenerateChartService
    {
        private static GenerateChartService _instanse = new GenerateChartService();
        public static GenerateChartService Instanse => _instanse;

        private GenerateChartService()
        {
        }

        public List<Chart> GenerateCharts(int count)
        {
            List<Chart> list = new List<Chart>();

            for (int i = 0; i < count; ++i)
            {
                list.Add(GenerateChart(i));
            }

            return list;
        }

        public Chart GenerateChart(int id = 1)
        {
            var folios = Enumerable
                .Range(1, id%3)
                .Select(i => i > 0 ? i.ToString() : null)
                .Where(i => !string.IsNullOrEmpty(i))
                .ToArray();

            return new Chart()
            {
                Id = id,
                Tps = new List<TpNtm>(),
                Number = id + 100,
                Folios = folios,
                EditionDate = DateTime.Today.AddDays((id % 10 - 100) * 7)
            };
        }
    }
}
