using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ComStar.ElDabaa.Service.Core.Http.HttpAccessor;

public interface IUserIdentityProvider
{
    Guid GetCurrentUserId();
    IHeaderDictionary GetHttpHeader();
    HttpContext GetHttpContext();
}

internal class UserIdentityProvider : IUserIdentityProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var currentUserId = _httpContextAccessor.HttpContext?.
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(currentUserId);
    }

    public IHeaderDictionary GetHttpHeader()
    {
        return _httpContextAccessor.HttpContext.Request.Headers;
    }

    public HttpContext GetHttpContext()
    {
        return _httpContextAccessor.HttpContext;
    }
}