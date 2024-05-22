using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.BlazorCore
{
    public class InteropResponse
    {
        // Properties
        public bool Succeeded { get; internal set; }

        public object Result { get; internal set; }

        public string ErrorMessage { get; internal set; }
    }
}
