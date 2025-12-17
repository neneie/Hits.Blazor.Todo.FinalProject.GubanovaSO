using Microsoft.AspNetCore.Components;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account
{
    internal sealed class IdentityRedirectManager
    {
        public const string StatusCookieName = "Identity.StatusMessage";

        private static readonly CookieBuilder CookieBuilder = new()
        {
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true,
            MaxAge = TimeSpan.FromSeconds(5),
        };

        private readonly NavigationManager _navigationManager;

        public IdentityRedirectManager(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void RedirectTo(string? uri)
        {
            uri = uri ?? "";
            // Prevent open redirects.
            if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
            {
                uri = _navigationManager.BaseUri;
            }

            _navigationManager.NavigateTo(uri);
        }

        public void RedirectToWithStatus(string uri, string message, HttpContext context)
        {
            context.Response.Cookies.Append(StatusCookieName, message, CookieBuilder.Build(context));
            RedirectTo(uri);
        }
    }
}
