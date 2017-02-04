using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using OpenChurchManagementSystem.WebApi.Models.Identities;
using SkyWeb.DatVM.WebApi;

namespace OpenChurchManagementSystem.WebApi.Models.Config
{
    public class StartupConfig
    {
        
        public static void Config()
        {
            ConfigAutofac();
        }

        private static void ConfigAutofac()
        {
            var dbContextType = typeof(Entities.OcmsEntities);

            WebApiAutofacIntializer.Initialize(
                dbContextType.Assembly,
                dbContextType,
                null,
                new IdentityChurchModule());
        }
        
    }

    class IdentityChurchModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityChurch>()
                .InstancePerRequest();
        }

    }

}