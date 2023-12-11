using System.Text.RegularExpressions;

namespace _1.RoutingExample.CustomConstraint
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }

            var regex = new Regex("^(apr|jul|oct|jan)$", RegexOptions.IgnoreCase);
            var month = values[routeKey]?.ToString() ?? "";
            return regex.IsMatch(month);
        }
    }
}
