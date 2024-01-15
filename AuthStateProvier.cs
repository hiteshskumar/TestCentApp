using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using MessagePack;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ChargesWFM.UI.Services;
using Microsoft.Extensions.Logging;
using ChargesWFM.UI.Exceptions;
using System.Collections.Generic;
// using System.Security.Cryptography

namespace ChargesWFM.UI
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthStateProvider> _logger;

        public string Token { get; set; }
        public event EventHandler<AuthenticationErrorTypeChanged> AuthenticationErrorTypeChanged;

        public AuthenticatedUser User { get; set; }

        public AuthStateProvider(NavigationManager navigationManager, IJSRuntime jsRuntime, ILocalStorageService localStorageService, HttpClient httpClient, ILogger<AuthStateProvider> logger)
        {
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                // User = await _localStorageService.GetAsync<AuthenticatedUser>();
                // var authCookieValue = await ValidateSession();
                // var expiryValue = ParseExpiry(Token);
                // var authenticatedUser = await _localStorageService.GetAsync<AuthenticatedUser>();
                // var sessionValid = !string.IsNullOrEmpty(Token)
                //     && !string.IsNullOrEmpty(authenticatedUser?.EncryptedKey)
                //     && authenticatedUser?.EncryptedKey.Equals(authCookieValue, StringComparison.CurrentCultureIgnoreCase) == true
                //     && expiryValue.HasValue
                //     && expiryValue > DateTime.Now;
                // if (sessionValid)
             // {
                //     var claims = GetClaims(Token);
                //     identity = CreateIdentity(claims);
                //     AuthenticateWhenExpired(expiryValue.Value);
                // }
                // else
                // {
                //     identity = await Authenticate();
                // }
                User = await _localStorageService.GetAsync<AuthenticatedUser>();
                var authCookieValue = await ValidateSession();
                var expiryValue = ParseExpiry(Token);
                var sessionValid = !string.IsNullOrEmpty(Token)
                    && !string.IsNullOrEmpty(User?.EncryptedKey)
                    && User?.EncryptedKey.Equals(authCookieValue, StringComparison.CurrentCultureIgnoreCase) == true
                    && expiryValue.HasValue
                    && expiryValue > DateTime.Now;
                if (sessionValid)
                {
                    var claims = GetClaims(Token);
                    identity = CreateIdentity(claims);
                    AuthenticateWhenExpired(expiryValue.Value);
                }
                else
                {
                    identity = await Authenticate();
                }
            }
            catch (NetworkException)
            {
                AuthenticationErrorTypeChanged?.Invoke(this, new AuthenticationErrorTypeChanged { ErrorType = AuthenticationErrorType.Network });
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch (UnauthorizedException ex)
            {
                AuthenticationErrorTypeChanged?.Invoke(this, new AuthenticationErrorTypeChanged { ErrorType = AuthenticationErrorType.Unauthorized });
                _logger.LogError(ex.Message, ex);
                await ClearLocalValues();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                AuthenticationErrorTypeChanged?.Invoke(this, new AuthenticationErrorTypeChanged { ErrorType = AuthenticationErrorType.Unknown });
                _logger.LogError(ex.Message, ex);
                await ClearLocalValues();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            if (identity.IsAuthenticated)
            {
                AuthenticationErrorTypeChanged?.Invoke(this, new AuthenticationErrorTypeChanged { ErrorType = AuthenticationErrorType.None });
            }
            var state = new AuthenticationState(new ClaimsPrincipal(identity));
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        public async Task<string> ValidateSession()
        {
            var cookie = await GetAuthCookie();
            if (string.IsNullOrEmpty(cookie))
            {
                await LogOut();
                return null;
            }
            return cookie;
        }

        public async Task LogOut()
        {
            await ClearLocalValues();
            if(_navigationManager.BaseUri.Contains("dev.centaur.com"))
            {
                _navigationManager.NavigateTo("https://dev.centaur.com/signout.aspx", true);
            }
            else if(_navigationManager.BaseUri.Contains("staging.centaur.com"))
            {
                _navigationManager.NavigateTo("https://staging.centaur.com/signout.aspx", true);
            }
            else
            {
                _navigationManager.NavigateTo("https://o365.microsoft.com", true);
            }
        }

        private async Task ClearLocalValues()
        {
            await _localStorageService.DeleteAsync<AuthenticatedUser>();
            await _localStorageService.DeleteAsync("Token");
        }

        private async Task<ClaimsIdentity> Authenticate()
        {
            try
            {
            await ClearLocalValues();
            var authCookieValue = await ValidateSession();
            if (string.IsNullOrEmpty(authCookieValue))
            {
                throw new Exception("Session is not available");
            }
            var authenticationResponse = await RefreshToken(authCookieValue);
            if (string.IsNullOrEmpty(authenticationResponse.Token))
            {
                return null;
            }
            var claims = GetClaims(authenticationResponse.Token);
            Token = authenticationResponse.Token;
            await _localStorageService.SetAsync("Token", Token);
            await _localStorageService.SetAsync(authenticationResponse.User);
            return CreateIdentity(claims);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"Failed Token creation", ex);
            }
        }

        private ClaimsIdentity CreateIdentity(IEnumerable<Claim> claims)
        {
            return new ClaimsIdentity(claims, "Authentication");
        }

        private void AuthenticateWhenExpired(DateTime expiry)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                var diff = expiry - DateTime.Now;
                await Task.Delay(Convert.ToInt32(diff.TotalMilliseconds));
                await GetAuthenticationStateAsync();
            });
        }

        private async Task<AuthenticationResponse> RefreshToken(string authCookieValue)
        {
            if (string.IsNullOrEmpty(authCookieValue))
            {
                throw new ArgumentNullException(nameof(authCookieValue), "Failed to validate session");
            }
            try
            {
                var request = new AuthenticationRequest { Key = authCookieValue };
                return await ApiHelper.PostUsingMsgPackAsync<AuthenticationResponse>("/authentication/authenticate", request, _httpClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new NetworkException();
            }
        }

        private DateTime? ParseExpiry(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var securtyToken = tokenHandler.ReadToken(token);
            return securtyToken.ValidTo;
        }

        private IEnumerable<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, token));
            return claims.AsEnumerable();
        }

        private async Task<string> GetAuthCookie()
        {
            var cookieValue = string.Empty;
            var text = await _jsRuntime.InvokeAsync<string>("readCookie");
            var cookies = text.Split(';');
            var authCookie = Array.Find(cookies, cookie => cookie.Trim().StartsWith("encrypted"));
            if (!string.IsNullOrEmpty(authCookie))
            {
                cookieValue = authCookie.Replace("encryptedAuthCookie=", string.Empty);
            }
            return cookieValue;
        }
    }

    [MessagePackObject]
    public class AuthenticationRequest
    {
        [Key(0)]
        public string Key { get; set; }
    }

    [MessagePackObject]
    public class AuthenticationResponse
    {
        [Key(0)]
        public AuthenticatedUser User { get; set; }
        [Key(1)]
        public string Token { get; set; }
    }

    public enum AuthenticationErrorType
    {
        Unauthorized,
        Network,
        Unknown,
        None
    }

    public class AuthenticationErrorTypeChanged : EventArgs
    {
        public AuthenticationErrorType ErrorType { get; set; }
    }
}