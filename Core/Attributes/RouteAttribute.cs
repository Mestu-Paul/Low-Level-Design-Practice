using System.Reflection;

namespace Core.Attributes
{
    public class RouteMethodAttribute : Attribute
    {
        public string RouteName { get; }

        public RouteMethodAttribute(string routeName)
        {
            RouteName = routeName;
        }   
    }

    public static class RouterMethodProvider
    {
        public static Dictionary<string, MethodInfo> GetRoutedMethods(Type assemblyType)
        {
            var methods =  assemblyType.GetMethods()
                .Where(m => m.GetCustomAttribute<RouteMethodAttribute>() != null)
                .ToDictionary(
                    x => x.GetCustomAttribute<RouteMethodAttribute>()!.RouteName,
                    x => x
                );
            return methods;
        }
    }
}
