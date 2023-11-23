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

        public DbSet<Medicos> Medico { get; set; }

        public DbSet<Especialidade> Especialidade { get; set; }
    }
}