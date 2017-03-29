using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Reichinger.Masterarbeit.PK_4_0.Test.ApplicationModelProvider
{
    public class RemoveAuthorizationModelProvider : IApplicationModelProvider
    {
        private readonly IHostingEnvironment _environment;

        private readonly IConfigurationRoot _configuration;

        public RemoveAuthorizationModelProvider(IHostingEnvironment env)
        {
            _environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
        }


        // Removes the Authorization Attribute if config parameter is set and
        // environment is Development
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            bool authorizationIsDisabled;
            bool.TryParse(_configuration["Authentication:Disabled"], out authorizationIsDisabled);

            var skipAuthorization = _environment.IsEnvironment("Development") && authorizationIsDisabled;
            if (!skipAuthorization)
            {
                return;
            }

            foreach (var controllerModel in context.Result.Controllers)
            {
                foreach (var action in controllerModel.Actions)
                {
                    if (action.Filters.OfType<IAsyncAuthorizationFilter>().FirstOrDefault() != null)
                    {
                        //default policy only used when there is no authorize filter in the controller
                        action.Filters.Remove(action.Filters.OfType<IAsyncAuthorizationFilter>().FirstOrDefault());
                    }
                }
                
            }
        }

        public int Order { get { return -9999; } }
    }
}
