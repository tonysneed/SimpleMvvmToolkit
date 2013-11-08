using System;
using System.Windows;
using System.ComponentModel;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Base class for view models
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Allows you to provide data at design-time (Blendability)
        /// </summary>
        public static bool IsInDesignMode<TViewModel>(this ViewModelBase<TViewModel> viewModel)
        {
            #if SILVERLIGHT
            return DesignerProperties.IsInDesignTool;
            #endif
            #if NETFX_CORE
            return Windows.ApplicationModel.DesignMode.DesignModeEnabled;
            #endif
            #if !SILVERLIGHT && !NETFX_CORE
            var prop = DesignerProperties.IsInDesignModeProperty;
            bool isInDesignMode = (bool)DependencyPropertyDescriptor
                .FromProperty(prop, typeof(FrameworkElement))
                .Metadata.DefaultValue;
            return isInDesignMode;
            #endif
        }
    }
}
