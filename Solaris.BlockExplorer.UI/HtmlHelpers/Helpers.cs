using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var stringBuilder = new StringBuilder();

            for (long i = 0; i < addresses.LongLength; i++)
            {
                var targetUrl = url.RouteUrl("Address", new {
                    Id = addresses[i]
                });

                if (string.IsNullOrWhiteSpace(targetUrl)) continue;


                stringBuilder.Append($"<a href=\"{targetUrl}\">{addresses[i]}</a>");

                if (i < addresses.LongLength)
                    stringBuilder.Append("<br/>");
            }


            return new HtmlString(stringBuilder.ToString());
        }

        public static string ViewMode(this IHtmlHelper source)
        {
            var viewModel = source.ViewContext.HttpContext.Request.Cookies["day-night-view-mode"] ?? "night-mode";
            return viewModel;
        }
    }
}
