﻿using MDP.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MDP.BlazorCore.Authentication.Maui
{
    public class AuthenticationFactory : ServiceFactory<IServiceCollection, AuthenticationFactory.Setting>
    {
        // Constructors
        public AuthenticationFactory() : base("Authentication", null, true) { }


        // Methods
        public override void ConfigureService(IServiceCollection serviceCollection, AuthenticationFactory.Setting setting)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");
            if (setting == null) throw new ArgumentException($"{nameof(setting)}=null");

            #endregion

            // AuthenticationManager
            serviceCollection.AddSingleton<AuthenticationManager, AuthenticationManager>();
        }


        // Class
        public class Setting
        {
            // Properties

        }
    }
}