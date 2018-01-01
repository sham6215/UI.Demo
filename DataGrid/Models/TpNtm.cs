using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid
{
    public class TpNtm
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public NtmType Type { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string PublicationDateShow => PublicationDate?.ToString("yyyy-MM-dd");
        public NtmStatus Status { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelDateShow => CancelDate?.ToString("yyyy-MM-dd");
        public List<Chart> Charts { get; set; }
        public bool InCollection => Charts?.Exists(c => c.InCollection) ?? false;
    }
}