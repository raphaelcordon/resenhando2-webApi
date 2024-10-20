using System.Security.Claims;

namespace Resenhando2.Api.Extensions;

public class ValidateOwnerExtension(IHttpContextAccessor httpContext)
{
    public bool IsOwner(Guid id)
    {
        return Guid.Parse(httpContext.HttpContext.User.FindFirstValue("id")).Equals(id);
    }

    public string GetIdFromClaims()
    {
        return httpContext.HttpContext.User.FindFirstValue("id");
    }
}