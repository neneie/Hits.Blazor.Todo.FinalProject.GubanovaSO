using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account
{
    public sealed class IdentityRevalidatingAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly NavigationManager _navigationManager;

        public IdentityRevalidatingAuthenticationStateProvider(
            IServiceProvider serviceProvider,
            NavigationManager navigationManager)
        {
            _serviceProvider = serviceProvider;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }
}
