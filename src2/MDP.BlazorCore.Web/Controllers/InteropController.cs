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
        public Task<object> InvokeAsync([FromBody] InvokeActionModel actionModel)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(actionModel);

            #endregion

            // InvokeAsync
            return _interopManager.InvokeAsync(actionModel.Path, actionModel.Payload, _serviceProvider);
        }

        public class InvokeActionModel 
        {        
            // Properties
            public string Path { get; set; } = null;

            public JsonDocument Payload { get; set; } = null;        
        }
    }
}
