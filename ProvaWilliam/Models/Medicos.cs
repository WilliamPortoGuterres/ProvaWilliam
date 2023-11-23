using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ProvaWilliam.Models
{
    public class Medicos
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set;}
        public string crm { get; set;}

        public int id_especialidade { get; set; }
        
        [ForeignKey("id_especialidade")]
        public virtual Especialidade Especialidade { get; set; }
    }
}