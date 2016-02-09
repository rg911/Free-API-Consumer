using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Extensions
{
    public static class UrlActionExtensions
    {
        public static string GetLanguageFromRoute(this UrlHelper urlHelper)
        {
            var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
            return routeValueDictionary["controller"].ToString();
        }
    }
}