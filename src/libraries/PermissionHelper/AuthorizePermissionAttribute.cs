﻿namespace PermissionHelper
{
    public class AuthorizePermissionAttribute
    {
        //protected override bool AuthorizeCore(HttpContext httpContext)
        //{
        //    if (httpContext == null)
        //    {
        //        throw new ArgumentNullException("httpContext");
        //    }

        //    IPrincipal user = httpContext.User;
        //    if (!user.Identity.IsAuthenticated)
        //    {
        //        return false;
        //    }

        //    if ((_usersSplit.Length > 0 && !_usersSplit.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase)) && (_rolesSplit.Length > 0 && !_rolesSplit.Any(user.IsInRole)))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
