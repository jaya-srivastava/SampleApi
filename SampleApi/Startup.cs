using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Sample.Service;


[assembly: OwinStartup(typeof(IPracticeApi.Startup))]

namespace IPracticeApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutoMapperConfigurator.Configure();
            //AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
