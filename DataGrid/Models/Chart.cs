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
        public string[] Folios { get; set; }
        public List<TpNtm> Tps { get; set; }
    }
}