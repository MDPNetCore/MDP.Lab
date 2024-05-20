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
        public Task<object> InvokeMethodAsync(string path, JsonDocument arguments)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(arguments);

            #endregion

            // Return
            return Task.FromResult(JsonSerializer.Serialize(arguments) as object);
        }
    }
}
