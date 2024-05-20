﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.BlazorCore.Authentication.Maui
{
    public interface IAuthenticationProvider
    {
        // Properties
        string Name { get; }


        // Methods
        Task LoginAsync(string returnUrl = null);

        Task LogoutAsync(string returnUrl = null);
    }
}
