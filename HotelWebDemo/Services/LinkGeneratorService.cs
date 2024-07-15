using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public class LinkGeneratorService : ILinkGeneratorSerivce
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LinkGenerator linkGenerator;

    public LinkGeneratorService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.linkGenerator = linkGenerator;
    }

    public string GenerateResetPasswordLink(string token)
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new Exception($"{nameof(httpContextAccessor.HttpContext)} is unexpectedly null.");
        }

        string? url = linkGenerator.GetUriByAction(
            httpContextAccessor.HttpContext,
            "ResetPassword",
            "Customer",
            new { token },
            httpContextAccessor.HttpContext.Request.Scheme);

        return url ?? throw new Exception("Failed to generate password reset URL.");
    }
}
