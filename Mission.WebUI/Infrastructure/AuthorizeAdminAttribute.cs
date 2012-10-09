﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mission.WebUI.Infrastructure
{
    // Källkoden ärligt stulen från: http://stackoverflow.com/questions/10064631/mvc-3-access-for-specific-user-only
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            return AuthorizeHelper.IsAdmin(httpContext);
        }
    }

    public class AuthorizeHelper
	{
        public static bool IsAdmin(HttpContextBase context)
        {
            return context.User.Identity.Name == "Jesper";
        }
	}
}