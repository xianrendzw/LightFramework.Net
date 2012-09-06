using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace LightFramework.Web.AspNetMvc
{
    using Core;

    public static class UrlHelperExtension
    {
        public static string GetReferUrl(this UrlHelper helper)
        {
            Uri uri = helper.RequestContext.HttpContext.Request.UrlReferrer;

            return uri == null ? "" : helper.Encode(uri.PathAndQuery);
        }

        public static string GetQueryString(this UrlHelper helper, string param)
        {
            string queryString = helper.RequestContext.HttpContext.Request.QueryString[param].NullSafe();

            if (queryString != "")
            {
                return queryString;
            }

            RouteValueDictionary values = helper.RequestContext.RouteData.Values;

            if (values[param] != null)
            {
                return values[param].ToString();
            }

            return "";
        }

        public static int GetQueryInt(this UrlHelper helper, string param, int defaultValue)
        {
            string value = helper.RequestContext.HttpContext.Request.QueryString[param].NullSafe();
            if (value == "")
            {
                RouteValueDictionary values = helper.RequestContext.RouteData.Values;
                if (values[param] != null)
                {
                    value = values[param].ToString();
                }
            }

            if (value == "")
            {
                return defaultValue;
            }

            return ConvertHelper.GetInt32(value);
        }
    }
}
