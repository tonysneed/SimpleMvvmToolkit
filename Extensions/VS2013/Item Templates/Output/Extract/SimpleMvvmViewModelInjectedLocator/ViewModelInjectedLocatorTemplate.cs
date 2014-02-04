/*
  In App.xaml:
  <Application.Resources>
      <vm:$safeitemname$ xmlns:vm="clr-namespace:$rootnamespace$"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
 
  Add references to: System.ComponentModel.Composition
                     System.ComponentModel.Composition.Initialization
*/

using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace $rootnamespace$
{
    /// <summary>
    /// <para>
    /// This class creates ViewModels on demand for Views, supplying a
    /// ServiceAgent to the ViewModel if required.
    /// It uses Managed Extensibility Framework (MEF) to inject service agents,
    /// which need to decorate themselves with this attribute:
    /// [ServiceAgentExport(typeof(IXXXServiceAgent), AgentType = AgentType.Real)]
    /// AgentType can be set to Real or Mock.
    /// Place the ViewModelLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:$safeitemname$ xmlns:vm="clr-namespace:$rootnamespace$"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// Use the <strong>mvvminjectedlocator</strong> or <strong>mvvmlocatornosa</strong>
    /// code snippets to add ViewModels to this locator.
    /// </para>
    /// </summary>
    public class $safeitemname$
    {
        static $safeitemname$()
        {
            // Expose parts in the current XAP to MEF
            if (!DesignerProperties.IsInDesignTool)
                CompositionHost.Initialize(new DeploymentCatalog());
        }

        // TODO: Specify default service agent type
        private AgentType agentType = AgentType.Real;
        public AgentType AgentType
        {
            get { return agentType; }
            set { agentType = value; }
        }

        /// <summary>
        /// MEF will create service agents based on ServiceAgentExport attribute.
        /// </summary>
        public $safeitemname$()
        {
            // Only inject service agents at runtime
            if (!DesignerProperties.IsInDesignTool)
            {
                // Use MEF to inject service agents
                CompositionInitializer.SatisfyImports(this);

                // TODO: Verify creation of service agents
            }
        }

        // TODO: Use mvvminjectedlocator or mvvmlocatornosa code snippets
        // to add ViewModels to the locator.
    }
}