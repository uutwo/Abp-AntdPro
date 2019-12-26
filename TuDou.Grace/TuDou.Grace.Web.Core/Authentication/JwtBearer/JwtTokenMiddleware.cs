using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TuDou.Grace.Web.Authentication.JwtBearer
{
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, string schema = "Bearer")
        {
            return UseExtensions.Use(app, async delegate (HttpContext ctx, Func<Task> next)
            {
                IIdentity identity = ctx.User.Identity;
                if (identity == null || !identity.IsAuthenticated)
                {
                    AuthenticateResult val = await AuthenticationHttpContextExtensions.AuthenticateAsync(ctx, schema);
                    if (val.Succeeded && val.Principal != null)
                    {
                        ctx.User = val.Principal;
                    }
                }
                await next();
            });
        }
    }
}
