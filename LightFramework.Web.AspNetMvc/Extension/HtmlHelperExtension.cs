using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace LightFramework.Web.AspNetMvc
{
    public static class HtmlHelperExtension
    {
        public static string Pager(this HtmlHelper helper, string routeName, string actionName, string controllerName,
            IDictionary<string, object> values, string pageParamName, bool appendQueryString, int pageCount,
            int noOfPageToShow, int noOfPageInEdge, int currentPage)
        {
            Func<string, int, string> getPageLink = (text, page) =>
            {
                RouteValueDictionary newValues = new RouteValueDictionary();

                foreach (KeyValuePair<string, object> pair in values)
                {
                    if ((string.Compare(pair.Key, "controller", StringComparison.OrdinalIgnoreCase) != 0) &&
                        (string.Compare(pair.Key, "action", StringComparison.OrdinalIgnoreCase) != 0))
                    {
                        newValues[pair.Key] = pair.Value;
                    }
                }

                if (page > 0)
                {
                    newValues[pageParamName] = page;
                }

                if (appendQueryString)
                {
                    NameValueCollection queryString = helper.ViewContext.HttpContext.Request.QueryString;

                    foreach (string key in queryString)
                    {
                        if (key != null)
                        {
                            if (!newValues.ContainsKey(key))
                            {
                                if (!string.IsNullOrEmpty(queryString[key]))
                                {
                                    newValues[key] = queryString[key];
                                }
                            }
                        }
                    }
                }

                string link;

                if (!string.IsNullOrEmpty(routeName))
                {
                    link = helper.RouteLink(text, routeName, newValues).ToHtmlString();
                }
                else
                {
                    actionName = actionName ?? values["action"].ToString();
                    controllerName = controllerName ?? values["controller"].ToString();

                    link = helper.ActionLink(text, actionName, controllerName, newValues, null).ToHtmlString();
                }

                return string.Concat(" ", link);
            };

            if (pageCount <= 1)
            {
                return "";
            }

            StringBuilder pagerHtml = new StringBuilder();

            pagerHtml.Append("<div class=\"pagelistbox\">");

            double half = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(noOfPageToShow) / 2));

            int start = Convert.ToInt32((currentPage > half) ? Math.Max(Math.Min((currentPage - half), (pageCount - noOfPageToShow)), 0) : 0);
            int end = Convert.ToInt32((currentPage > half) ? Math.Min(currentPage + half, pageCount) : Math.Min(noOfPageToShow, pageCount));

            if (currentPage > 1)
            {
                pagerHtml.Append(getPageLink("上一页", currentPage - 1));
            }
            else
            {
                pagerHtml.Append(" <span class=\"indexPage\">上一页 </span>");
            }

            if (start > 0)
            {
                int startingEnd = Math.Min(noOfPageInEdge, start);

                for (int i = 0; i < startingEnd; i++)
                {
                    int pageNo = (i + 1);

                    pagerHtml.Append(getPageLink(pageNo.ToString(), pageNo));
                }

                if (noOfPageInEdge < start)
                {
                    pagerHtml.Append(" ...");
                }
            }

            for (int i = start; i < end; i++)
            {
                int pageNo = (i + 1);

                if (pageNo == currentPage)
                {
                    pagerHtml.Append(string.Format(" <span class=\"current\">{0}</span>", pageNo));
                }
                else
                {
                    pagerHtml.Append(getPageLink(pageNo.ToString(), pageNo));
                }
            }

            if (end < pageCount)
            {
                if ((pageCount - noOfPageInEdge) > end)
                {
                    pagerHtml.Append(" ...");
                }

                int endingStart = Math.Max(pageCount - noOfPageInEdge, end);

                for (int i = endingStart; i < pageCount; i++)
                {
                    int pageNo = (i + 1);
                    pagerHtml.Append(getPageLink(pageNo.ToString(), pageNo));
                }
            }

            if (currentPage < pageCount)
            {
                pagerHtml.Append(getPageLink("下一页", currentPage + 1));
            }
            else
            {
                pagerHtml.Append(" <span class=\"disabled\">下一页</span>");
            }

            pagerHtml.Append("</div>");

            return pagerHtml.ToString();
        }
    }
}
