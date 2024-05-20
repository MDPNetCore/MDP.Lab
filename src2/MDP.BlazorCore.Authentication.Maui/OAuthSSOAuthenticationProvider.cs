using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls.Handlers;
using System;
using System.Threading.Tasks;
using MDP.BlazorCore.Maui;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;

namespace MDP.BlazorCore.Authentication.Maui
{
    public class OAuthSSOAuthenticationProvider : IAuthenticationProvider
    {
        // Fields
        private readonly OAuthSSOOptions _authOptions = null;

        private readonly IHostEnvironment _hostEnvironment = null;

        private readonly UserManager _userManager = null;

        private readonly NavigationManager _navigationManager = null;


        // Constructors
        public OAuthSSOAuthenticationProvider(OAuthSSOOptions authOptions, IHostEnvironment hostEnvironment, UserManager userManager, NavigationManager navigationManager)
        {
            #region Contracts

            if (authOptions == null) throw new ArgumentException($"{nameof(authOptions)}=null");
            if (hostEnvironment == null) throw new ArgumentException($"{nameof(hostEnvironment)}=null");
            if (userManager == null) throw new ArgumentException($"{nameof(userManager)}=null");
            if (navigationManager == null) throw new ArgumentException($"{nameof(navigationManager)}=null");

            #endregion

            // Default
            _authOptions = authOptions;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _navigationManager = navigationManager;
        }


        // Properties
        public string Name { get; private set; } = "OAuthSSO";


        // Methods
        public async Task LoginAsync(string returnUrl = null)
        {
            // Require
            if (string.IsNullOrEmpty(returnUrl) == true) returnUrl = "/";

            // AuthHandler
            using(var authHandler = new OAuthSSOHandler(_authOptions, _hostEnvironment))
            {
                // AuthenticateToken
                var authenticateToken = await authHandler.AuthenticateAsync();
                if (string.IsNullOrEmpty(authenticateToken) == true) throw new InvalidOperationException($"{nameof(authenticateToken)}=null");

                // AccessToken
                var accessToken = await authHandler.GetAccessTokenAsync(authenticateToken);
                if (string.IsNullOrEmpty(accessToken) == true) throw new InvalidOperationException($"{nameof(accessToken)}=null");

                // ClaimsIdentity
                var claimsIdentity = await authHandler.GetUserInformationAsync(authenticateToken);
                if (claimsIdentity == null) throw new InvalidOperationException($"{nameof(claimsIdentity)}=null");
                await _userManager.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                // Redirect
                _navigationManager.NavigateTo(returnUrl);
            }            
        }

        public async Task LogoutAsync(string returnUrl = null)
        {
            // Require
            if (string.IsNullOrEmpty(returnUrl) == true) returnUrl = "/";

            // AuthHandler
            using (var authHandler = new OAuthSSOHandler(_authOptions, _hostEnvironment))
            {
                // AuthenticateToken

                // AccessToken

                // ClaimsIdentity
                await _userManager.SignOutAsync();

                // Redirect
                _navigationManager.NavigateTo(returnUrl);
            }
        }
    }
}
