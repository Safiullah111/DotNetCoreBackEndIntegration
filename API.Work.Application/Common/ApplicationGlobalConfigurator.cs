using API.Work.Application.Common.Mapping;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Application.Common;

public static class ApplicationGlobalConfigurator
{
    public static void ConfigureGlobals(IServiceProvider serviceProvider)
    {
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("GlobalLogger");

        LoggerAccessor.Configure(logger);
        ObjectMapper.Configure(mapper);
    }
}
