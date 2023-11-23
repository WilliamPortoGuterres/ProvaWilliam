using ProvaWilliam.Interface;
using ProvaWilliam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Unity;

namespace ProvaWilliam
{
    public static class Bootstrapper
    {
        public static IUnityContainer Container { get; private set; }
        public static void Initialise()
        {
            Container = BuildUnityContainer();

      
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IEspecialidadeService, EspecialidadeService>();
            
            container.RegisterType<IMedicoService, MedicoService>();

            return container;
        }
    }
}