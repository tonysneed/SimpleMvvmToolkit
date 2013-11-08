using System;
using System.Linq;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Metadata indicating the kind of service agent used.
    /// </summary>
    public interface IServiceAgentMetadata
    {
        /// <summary>
        /// Service agent type.
        /// </summary>
        AgentType AgentType { get; }
    }

    /// <summary>
    /// Service agent type.
    /// </summary>
    public enum AgentType
    {
        /// <summary>
        /// Unspecified service agent.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Real service agent.
        /// </summary>
        Real,

        /// <summary>
        /// Mock service agent.
        /// </summary>
        Mock
    }
}
