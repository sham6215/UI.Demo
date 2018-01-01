using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid
{
    public class Chart
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime EditionDate { get; set; }
        public string EditionDateShow => EditionDate.ToString("yyyy-MM-dd");
        public string[] Folios { get; set; }
        public bool InCollection => Folios?.Count() > 0;
        public string FoliosShow => string.Join(",", Folios);
        public List<TpNtm> Tps { get; set; }
    }
}