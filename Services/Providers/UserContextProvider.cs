using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyWalletApp.Services.Providers
{
    public class UserContextProvider: IUserContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextProvider(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetUser => _httpContextAccessor.HttpContext.User;
    }
}