using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Queues.Filter
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public string Roles;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authheader = actionContext.Request.Headers.Authorization;
            if (authheader != null)
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                var usernamepasswordArray = decodedAuthenticationToken.Split(':');
                var userName = usernamepasswordArray[0];
                var password = usernamepasswordArray[1];
                var isValid = userName == "sreetama" && password == "sarkar";
                if (isValid)
                {
                    var principal = new GenericPrincipal(new GenericIdentity(userName), requiredRoles(userName));
                }
                else
                    HandleUnathorised(actionContext);

            }
            //base.OnAuthorization(actionContext);
        }

        private void HandleUnathorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Autheticate", "BAsic Scheme='Data' location='http:''localhost:");
            //throw new NotImplementedException();
        }

        private string[] requiredRoles(string userName)
        {
            string[] roles = new string[] { "Admin", "User", "Sales", "Estimator" };
            if (userName == "sreetama")
            {

            }
            return roles;
        }
        private string determineRoles(string userName)
        {
            if (userName == "sreetama")
            {
                Roles = "Admin";
                return "Admin";
            }
            else
            {
                Roles = "User";
                return "User";
            }
        }
    }
}