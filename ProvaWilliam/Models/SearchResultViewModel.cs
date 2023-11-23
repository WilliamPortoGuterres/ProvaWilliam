using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaWilliam.Models
{
    public class SearchResultViewModel
    {

        public List<Medicos> Medicos { get; set; }
        public List<Especialidade> Especialidades { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
    }
}