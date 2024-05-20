using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropManager
    {
        // Methods
        public Task<object> InvokeMethodAsync(string path, JsonDocument parameters)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(parameters);

            #endregion

            // Return
            return Task.FromResult(JsonSerializer.Serialize(parameters) as object);
        }
    }
}
