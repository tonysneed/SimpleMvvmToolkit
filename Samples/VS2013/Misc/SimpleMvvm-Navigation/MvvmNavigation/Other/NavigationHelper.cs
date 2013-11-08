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
        /// Dictionary of page values. Key is page name, value is dictionary of property names / values.
        /// </summary>
        public static Dictionary<string, Dictionary<string, object>> PageValues
        {
            get { return pageValues; }
            set { pageValues = value; }
        }
        private static Dictionary<string, Dictionary<string, object>> pageValues =
            new Dictionary<string,Dictionary<string,object>>();
    }
}
