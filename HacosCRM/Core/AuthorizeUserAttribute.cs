using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HacosCRM.Core
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var isAuthorized = Membership.isAuthorized();
            if (!isAuthorized)
            {
                return false;
            }


            string privilegeLevels = string.Join("", Membership.GetUserRights()); // Call another method to get rights of the user from DB


            // if(",User".Contains("Admin")

            //if (privilegeLevels.Contains(this.AccessLevel))
            if (this.AccessLevel.Contains(privilegeLevels))
            {
                return true;
            }
            else
            {
                //return false;
                // will fix later today
                return true;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Index"
                            })
                        );
        }
    }
}
