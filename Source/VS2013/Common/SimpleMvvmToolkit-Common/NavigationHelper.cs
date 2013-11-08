using System;
using System.Linq;
using System.Collections.Generic;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Helper for passing types to views instantiated at a later time.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Queue of key-value pairs: key is page name; value is dictionary of properties-values.
        /// </summary>
        public static Queue<KeyValuePair<string, Dictionary<string, object>>> PageValues { get; set; }
    }
}