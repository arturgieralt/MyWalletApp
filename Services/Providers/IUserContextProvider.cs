using System.Security.Claims;

namespace MyWalletApp.Services.Providers
{
    public interface IUserContextProvider
    {
         ClaimsPrincipal GetUser {get; }
    }
}