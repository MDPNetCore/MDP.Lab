using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropManager
    {
        // Fields
        private readonly Dictionary<string, InteropMethod> _interopMethodDictionary = null;


        // Constructors
        public InteropManager(Dictionary<string, InteropMethod> interopMethodDictionary)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(interopMethodDictionary);

            #endregion

            // Default
            _interopMethodDictionary = interopMethodDictionary;
        }


        // Methods
        public Task<object> InvokeMethodAsync(string path, JsonDocument parameters, IServiceProvider serviceProvider)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(parameters);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            #endregion

            // Return
            return Task.FromResult(JsonSerializer.Serialize(parameters) as object);
        }
    }
}
