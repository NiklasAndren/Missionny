﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mission.Domain.Entities;
using Mission.Domain.Repositories;

namespace Mission.WebUI.Infrastructure
{

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
            return context.User.Identity.Name.ToLower() == "jesper";
        }


        public static bool IsAuthorizedCompany(HttpContextBase context/*, Guid EventID*/)
        {
            var userRepo = new Repository<User>();
            var user = userRepo.FindAll(u => u.UserName.ToLower() == context.User.Identity.Name.ToLower()).FirstOrDefault();
            //var EventOwnerID = user.UserName;
            if (user == null)
                return false;
            else
                return true;
                //user == null ? false : user.ID == EventID;
        }
    }


}