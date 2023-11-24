using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace TAIBackend.routes.account;

[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme), Route("account")]
public class AccountController : Controller
{
    [AllowAnonymous]
    [HttpGet("facebook-login")]
    public IActionResult FacebookLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = "https://localhost:3000" };
        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    private class AccountDetails
    {
        [JsonProperty("firstName")]
        public string? FirstName;
        [JsonProperty("fullName")]
        public string? FullName;
        [JsonProperty("profilePictureSrc")]
        public string? ProfilePictureSrc;
        [JsonProperty("email")]
        public string? Email;
    };
    
    [HttpGet("details")]
    public IActionResult Details()
    {
        var accountDetails = new AccountDetails
        {
            FirstName = User.FindFirst(ClaimTypes.GivenName)?.Value,
            FullName = User.FindFirst(ClaimTypes.Name)?.Value,
            ProfilePictureSrc = User.FindFirst("urn:facebook:picture")?.Value,
            Email = User.FindFirst(ClaimTypes.Email)?.Value
        };

        return Content(accountDetails.ToJson(), "application/json");
    }
}