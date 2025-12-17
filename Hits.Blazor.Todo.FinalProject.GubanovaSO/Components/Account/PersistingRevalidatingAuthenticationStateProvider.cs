using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account
{
    internal sealed class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly PersistentComponentState _state;
        private Task<AuthenticationState>? _authenticationStateTask;

        public PersistingRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory serviceScopeFactory,
            PersistentComponentState state)
            : base(loggerFactory)
        {
            _scopeFactory = serviceScopeFactory;
            _state = state;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> IsAuthenticatedAsync()
        {
            var state = await GetAuthenticationStateAsync();
            return state.User.Identity?.IsAuthenticated ?? false;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _authenticationStateTask ??= base.GetAuthenticationStateAsync();

            return await _authenticationStateTask;
        }

        protected override async Task RevalidateAuthenticationStateAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateAsyncScope();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.GetUserAsync(new ClaimsPrincipal());
                if (user is null)
                {
                    await signInManager.SignOutAsync();
                }
            }
            catch
            {
            }
        }
    }
}
