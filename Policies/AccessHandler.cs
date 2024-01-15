using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ChargesWFM.UI.Policies
{
    public class AccessHandler : IAuthorizationHandler
    {
        private readonly ILogger<AccessHandler> logger;
        private readonly AuthStateProvider authStateProvider;

        public AccessHandler(AuthStateProvider authStateProvider, ILogger<AccessHandler> logger)
        {
            this.authStateProvider = authStateProvider;
            this.logger = logger;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User is null)
            {
                _ = authStateProvider.GetAuthenticationStateAsync();
                return Task.CompletedTask;
            }

            var screens = authStateProvider.User?.Screens;
            if (screens?.Any() != true)
             {
                _ = authStateProvider.GetAuthenticationStateAsync();
                return Task.CompletedTask;
             }

            var requirement = context.Requirements
                .ToList()
                .Find(requirement => SatisfiesRequirement(requirement));
            if (requirement is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

            bool SatisfiesRequirement(IAuthorizationRequirement requirement)
            {
                if (requirement is not IAccessRequirement accessRequirement)
                {
                    return false;
                }

                return screens
                    .Any(screen => accessRequirement.Module.Equals(screen.Name, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}