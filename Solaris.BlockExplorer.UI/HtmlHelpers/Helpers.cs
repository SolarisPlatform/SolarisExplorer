using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Solaris.BlockExplorer.UI.HtmlHelpers
{
    public static class Helpers
    {
        public static string IsLinkActive(this IUrlHelper url, string action, string controller)
        {
            if (url.ActionContext.RouteData.Values["controller"].ToString() == controller &&
                url.ActionContext.RouteData.Values["action"].ToString() == action)
            {
                return "active";
            }

            return "";
        }

        public static IHtmlContent AddressLink(this IUrlHelper url, string[] addresses)
        {
            var baseUrl = url.Action("Index", "Address");

            var stringBuilder = new StringBuilder();

            for (long i = 0; i < addresses.LongLength; i++)
            {
                stringBuilder.Append($"<a href=\"{baseUrl}/?address={addresses[i]}\">{addresses[i]}</a>");
                
                if (i < addresses.LongLength)
                    stringBuilder.Append("<br/>");
            }


            return new HtmlString(stringBuilder.ToString());
        }
    }
}
