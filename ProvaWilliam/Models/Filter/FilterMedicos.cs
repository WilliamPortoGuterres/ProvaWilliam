using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaWilliam.Models.Filter
{
    public class FilterMedicos
    {

        public int id { get; set; }
        public string nome { get; set; }
        public string crm { get; set; }

        public int id_especialidade { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}