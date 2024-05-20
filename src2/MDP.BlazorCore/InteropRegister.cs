using MDP.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public static class InteropRegister
    {
        // Methods
        public static void AddInteropManager(this IServiceCollection serviceCollection)
        {
            #region Contracts

            if (serviceCollection == null) throw new ArgumentException($"{nameof(serviceCollection)}=null");

            #endregion

            // InteropServiceTypeList
            var interopServiceTypeList = MDP.Reflection.Type.FindAllApplicationType();
            if (interopServiceTypeList == null) throw new InvalidOperationException($"{nameof(interopServiceTypeList)}=null");

            // InteropServiceTypeList.Filter
            interopServiceTypeList = interopServiceTypeList.AsParallel().Where(interopServiceType =>
            {
                // Require
                if (interopServiceType.IsClass == false) return false;
                if (interopServiceType.IsPublic == false) return false;
                if (interopServiceType.IsAbstract == true) return false;
                if (interopServiceType.IsGenericType == true) return false;
                if (interopServiceType.IsAssignableTo(typeof(InteropService)) == false) return false;

                // Return
                return true;
            }).ToList();

            // InteropMethodDictionary
            var interopMethodDictionary = new Dictionary<string, InteropMethod>(StringComparer.OrdinalIgnoreCase);
            foreach (var interopServiceType in interopServiceTypeList)
            {
                // RegisterType
                serviceCollection.AddScoped(interopServiceType);

                // RegisterMethod
                var interopMethodList = interopServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var interopMethod in interopMethodList)
                {
                    // RouteAttribute
                    var routeAttribute = interopMethod.GetCustomAttribute<InteropRouteAttribute>();
                    if (routeAttribute == null) continue;
                    if (interopMethodDictionary.ContainsKey(routeAttribute.Template) == true) throw new InvalidOperationException($"Duplicate route detected: {routeAttribute.Template}");

                    // Add
                    interopMethodDictionary.Add(routeAttribute.Template, new InteropMethod(routeAttribute.Template, interopMethod));
                }
            }

            // InteropManager
            serviceCollection.AddSingleton(new InteropManager(interopMethodDictionary));
        }
    }
}
