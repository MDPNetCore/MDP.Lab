using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore.Web
{
    public class InteropController : Controller
    {
        // Fields
        private readonly InteropManager _interopManager = null;

        private readonly IServiceProvider _serviceProvider = null;


        // Constructors
        public InteropController(InteropManager interopManager, IServiceProvider serviceProvider)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(interopManager);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            #endregion

            // Default
            _interopManager = interopManager;
            _serviceProvider = serviceProvider;
        }


        // Methods
        [AllowAnonymous]
        [Route("/.blazor/interop/invoke")]
        public Task<InteropResponse> InvokeAsync([FromBody] InvokeActionModel actionModel)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(actionModel);

            #endregion

            // RootUri
            var rootUri = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host}{this.HttpContext.Request.PathBase}";
            if (string.IsNullOrEmpty(rootUri) == true) throw new InvalidOperationException($"{nameof(rootUri)}=null");

            // User
            var user = this.User;
            if (user == null) throw new InvalidOperationException($"{nameof(user)}=null");

            // InvokeAsync
            return _interopManager.InvokeAsync(new InteropRequest
            (
                new Uri(new Uri(rootUri), actionModel.Path),
                actionModel.Payload,
                user,
                _serviceProvider
            ));
        }

        public class InvokeActionModel 
        {        
            // Properties
            public string Path { get; set; } = null;

            public JsonDocument Payload { get; set; } = null;        
        }
    }
}
