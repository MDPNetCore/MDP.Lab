using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropRequest
    {
        // Constructors
        public InteropRequest(Uri url, JsonDocument payload, ClaimsPrincipal user, IServiceProvider serviceProvider)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(payload);
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            #endregion

            // Default
            this.Url = url;
            this.Payload = payload;
            this.User = user;
            this.ServiceProvider = serviceProvider;
        }


        // Properties
        public Uri Url { get; private set; }
        
        public JsonDocument Payload { get; private set; }

        public ClaimsPrincipal User { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }
    }
}
