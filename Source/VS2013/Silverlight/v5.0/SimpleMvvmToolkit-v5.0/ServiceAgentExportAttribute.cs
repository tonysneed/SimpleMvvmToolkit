using System;
#if NETFX_CORE
using System.Composition;
#else
using System.ComponentModel.Composition;
#endif

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Apply this attribute to a service agent class.
    /// Specify the service agent interface type as the contract type,
    /// as well as the agent type. For example:
    /// [ServiceAgentExport(typeof(ICustomerServiceAgent), AgentType = AgentType.Mock)]
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAgentExportAttribute : ExportAttribute
    {
        /// <summary>
        /// Constructor for service agent export attribute.
        /// </summary>
        /// <param name="contractType">Service agent interface type</param>
        public ServiceAgentExportAttribute(Type contractType) :
            base(contractType) { }

        /// <summary>
        /// Service agent type.
        /// </summary>
        public AgentType AgentType { get; set; }
    }
}
