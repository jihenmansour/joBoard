using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;


[assembly: OwinStartup(typeof(JoBoard.App_Start.Startup))]

namespace JoBoard.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            CookieAuthenticationOptions cookieAuthOptions = new CookieAuthenticationOptions();
            cookieAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthOptions.LoginPath = new PathString("/account/login");

            app.UseCookieAuthentication(cookieAuthOptions);
        }
    }
}
