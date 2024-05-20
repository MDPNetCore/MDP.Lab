using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropManager
    {
        // Fields
        private readonly Dictionary<string, InteropMethod> _interopMethodDictionary = null;


        // Constructors
        public InteropManager(Dictionary<string, InteropMethod> interopMethodDictionary)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(interopMethodDictionary);

            #endregion

            // Default
            _interopMethodDictionary = interopMethodDictionary;
        }


        // Methods
        public Task<object> InvokeAsync(string path, JsonDocument parameters, IServiceProvider serviceProvider)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(parameters);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            #endregion

            // Path
            if (path.StartsWith("/") == false) path = "/" + path;
            if (path.EndsWith("/") == true) path = path.TrimEnd('/');

            // InteropMethod
            var interopMethod = this.FindInteropMethod(path);
            if (interopMethod == null) throw new InvalidOperationException($"{nameof(interopMethod)}=null");

            // Return
            return interopMethod.InvokeAsync(path, parameters, serviceProvider);
        }

        private InteropMethod FindInteropMethod(string path)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);

            #endregion

            // Result
            InteropMethod interopMethod = null;

            // FindByPath
            if (_interopMethodDictionary.TryGetValue(path, out interopMethod) == true) return interopMethod;

            // PathSectionList
            var pathSectionList = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (pathSectionList == null) throw new InvalidOperationException($"{nameof(pathSectionList)}=null");
            if (pathSectionList.Count == 0) throw new InvalidOperationException($"{nameof(pathSectionList)}.Count=0");

            // FindByPathSectionList
            interopMethod = _interopMethodDictionary.Values.FirstOrDefault(interopMethod => interopMethod.CanInvoke(pathSectionList) == true);
            if (interopMethod != null) return interopMethod;

            // Return
            return null;
        }
    }
}
