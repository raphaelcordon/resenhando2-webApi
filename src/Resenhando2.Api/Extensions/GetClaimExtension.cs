using System.Security.Claims;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Extensions;

public class GetClaimExtension(IHttpContextAccessor httpContext) : IGetClaimExtension
{
    public bool IsOwner(Guid id)
    {
        return Guid.Parse(httpContext.HttpContext!.User.FindFirstValue("id")!).Equals(id);
    }

    public string GetUserIdFromClaims()
    {
        return httpContext.HttpContext!.User.FindFirstValue("id")!;
    }
}