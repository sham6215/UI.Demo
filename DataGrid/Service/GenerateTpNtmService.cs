using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid.Service
{
    public class GenerateTpNtmService
    {
        private static GenerateTpNtmService _instanse = new GenerateTpNtmService();
        public static GenerateTpNtmService Instanse => _instanse;

        private GenerateTpNtmService()
        { 
        }

        public List<TpNtm> GenerateCharts(int count)
        {
            List<TpNtm> list = new List<TpNtm>();

            for (int i = 0; i < count; ++i)
            {
                list.Add(GenerateChart(i));
            }

            return list;
        }

        public TpNtm GenerateChart(int id = 1)
        {
            bool isCancelled = id % 4 == 0 ? true : false;

            return new TpNtm()
            {
                Id = id,
                Charts = new List<Chart>(),
                Number = id + 1000,
                Type = id % 2 == 0 ? NtmType.Preliminary : NtmType.Temporary,
                Status = isCancelled ? NtmStatus.Cancelled : NtmStatus.InForce,
                CancelDate = isCancelled ? DateTime.Today.AddDays((id%100 + 50) * 7) : default(DateTime?),
                PublicationDate = DateTime.Today.AddDays((id % 10 + 1) * 7)
            };
        }
    }
}
