using System;
using System.Security.Claims;

namespace MyWalletApp.Extensions
{
    public static class UserExtensions
    {
        public static T GetId<T>(this ClaimsPrincipal principal)
    {
        var castToType = typeof(T);

        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if(string.IsNullOrEmpty(loggedInUserId))
        {
            throw new Exception("No User Id");
        }

        if (castToType == typeof(string) || 
        castToType == typeof(int) || 
        castToType == typeof(long))
        {
            return (T)Convert.ChangeType(loggedInUserId, castToType);
        } 
        else 
        {
            throw new Exception("Invalid type provided");
        }
    }

    public static string GetName(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Name);
    }

    public static string GetEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Email);
    }
    }
}

// https://stackoverflow.com/questions/30701006/how-to-get-the-current-logged-in-user-id-in-asp-net-core
