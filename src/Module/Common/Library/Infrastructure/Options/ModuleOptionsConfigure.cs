﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tm.Lib.Utils.Core.Options;

namespace Tm.Module.Common.Infrastructure.Options
{
    public class ModuleOptionsConfigure : IModuleOptionsConfigure
    {
        public void ConfigOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CommonOptions>(configuration);
        }
    }
}
