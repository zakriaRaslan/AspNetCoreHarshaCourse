
using System.Text.RegularExpressions;

namespace RoutingSection.CustomConstrains
{
    public class MonthsCustomConstrain : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(routeKey))
            {
                Regex regex = new Regex("^(apr|jul|aug|jan|oct)$"); 
               string? monthValue = Convert.ToString(values[routeKey]);
                if (regex.IsMatch(monthValue))
                {
                    return true;
                }
                else { return false; }

            }else return false;
        }
    }
}
