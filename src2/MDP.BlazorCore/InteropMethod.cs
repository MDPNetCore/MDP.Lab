using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

            // Default
            _template = template;
            _method = method;

            // TemplateSectionList
            _templateSectionList = template.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }


        // Methods
        public bool CanInvoke(string path, out Dictionary<string, string> pathParameters)
        {
            pathParameters = null;
            return true;
        }
    }
}
