using ProvaWilliam.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProvaWilliam.Infra
{
    public class Contexto : DbContext
    {

        public Contexto() { }


       

       

        public System.Data.Entity.DbSet<ProvaWilliam.Models.Medicos> Medicos { get; set; }

        public System.Data.Entity.DbSet<ProvaWilliam.Models.Especialidade> Especialidades { get; set; }
    }
}