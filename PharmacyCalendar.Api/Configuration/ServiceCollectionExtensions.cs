using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using Utilities.Framework.Exceptions;

namespace PharmacyCalendar.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, SiteSettings siteSettings)
        {
            var authority = siteSettings.SSO_Api_Url;
            var cookieName = siteSettings.JwtBearer.CookieName;
            var refreshName = siteSettings.JwtBearer.RefreshName;

            services
             .AddAuthentication(option =>
             {
                 option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddCookie()
            .AddJwtBearer(o =>
            {
                o.Authority = authority;

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidTypes = new[] { "at+jwt" },
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = authority
                };
                o.TokenValidationParameters = tokenValidationParameters;

                o.RequireHttpsMetadata = false;
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        context.Token = context.Request.Cookies[cookieName];
                    },

                    OnTokenValidated = context =>
                    {
                        var claims = context.Principal.Identity as ClaimsIdentity;
                        if (!claims.Claims.Any())
                            context.Fail("This token has no claims.");
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new AppException("Authenticate failure.", HttpStatusCode.Unauthorized);
                        throw new AppException("You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }
                };
            });


        }
    }
}
