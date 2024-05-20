using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropMethod
    {
        // Fields
        private readonly string _template = null;

        private readonly MethodInfo _method = null;

        private readonly List<string> _templateSectionList = null;


        // Constructors
        public InteropMethod(string template, MethodInfo method)
        {
            #region Contracts

            if (string.IsNullOrEmpty(template) == true) throw new ArgumentException($"{nameof(template)}=null");
            if (method == null) throw new ArgumentException($"{nameof(method)}=null");

            #endregion

            // Template
            if (template.StartsWith("/") == false) template = "/" + template;
            if (template.EndsWith("/") == true) template = template.TrimEnd('/');
            _template = template;

            // Method
            _method = method;
            
            // TemplateSectionList
            _templateSectionList = template.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }


        // Properties
        public string Template { get { return _template; } }


        // Methods
        public bool CanInvoke(List<string> pathSectionList)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNull(pathSectionList);

            #endregion

            return false;
        }

        public Task<object> InvokeAsync(string path, JsonDocument payload, IServiceProvider serviceProvider)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(path);
            ArgumentNullException.ThrowIfNull(payload);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            #endregion

            // Instance
            var instance = serviceProvider.GetService(_method.DeclaringType);
            if (instance == null) throw new InvalidOperationException($"{nameof(instance)}=null");

            // ExecuteMethod
            var result = MDP.Reflection.Activator.ExecuteMethod(instance, _method.Name, new InteropParameterProvider());
            if (result is Task task)
            {
                if (task.GetType().IsGenericType == false)
                {
                    return task.ContinueWith(o => (object)null);
                }
                else
                {
                    return task.ContinueWith(o => (object)(task.GetType().GetProperty("Result").GetValue(o)));
                }
            }

            // Return
            return Task.FromResult(result);
        }
    }
}
