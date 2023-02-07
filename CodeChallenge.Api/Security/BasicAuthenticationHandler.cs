using CodeChallenge.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CodeChallenge.Api.Security;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserRepository userRepository) : base(options, logger, encoder, clock)
    {
        _userRepository = userRepository;
        _logger = logger.CreateLogger(nameof(BasicAuthenticationHandler));
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Authorization header was not found");
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            var result = await IsAuthorized(username, password);

            if (!result)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            return Authenticate(username, password);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while parsing Authorization header");
            return AuthenticateResult.Fail("Invalid Authorization header");
        }
    }

    public AuthenticateResult Authenticate(string username, string password)
    {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username),
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private async Task<bool> IsAuthorized(string username, string password)
    {
        return await _userRepository.FindAsync(username, password);
    }

}
