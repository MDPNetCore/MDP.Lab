using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropResource
    {
        // Constructors
        public InteropResource(Uri url)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(url);

            #endregion

            // Default
            this.Url = url;
        }


        // Properties
        public Uri Url { get; private set; }
    }
}
