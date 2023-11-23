using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProvaWilliam.Models
{
    public class Especialidade
    {
        [Key]
        public int id {  get; set; }
        public string descricao { get; set; }

        public virtual ICollection<Medicos> Medicos { get; set; }
    }
}