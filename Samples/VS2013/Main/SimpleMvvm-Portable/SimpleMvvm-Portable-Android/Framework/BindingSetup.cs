using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Droid;

namespace SimpleMvvm_Android
{
    // Sets up IOC container and binding builder
    // Implement IMvxAndroidGlobals
    public class BindingSetup : IMvxAndroidGlobals
    {
        // Singleton
        public static readonly BindingSetup Instance = new BindingSetup();

        // Prevent direct instantiation
        private BindingSetup() { }

        public void Initlialize(Context context)
        {
            // Return if IOC container already initialized
            if (MvxSimpleIoCContainer.Instance != null)
                return;

            // Set application context
            ApplicationContext = context;

            // Init IOC container
            MvxSimpleIoCContainer.Initialize();

            // Register Android globals with IOC container
            Mvx.RegisterSingleton<IMvxAndroidGlobals>(this);

            // Register binding builder
            new MvxAndroidBindingBuilder().DoRegistration();

            // Add Android widget assembly to the view cache
            var viewCache = Mvx.Resolve<IMvxTypeCache<View>>();
            viewCache.AddAssembly(typeof(LinearLayout).Assembly);
        }

        // IMvxAndroidGlobals implementation
        public string ExecutableNamespace { get { return "SimpleMvvm_Android"; } }
        public Assembly ExecutableAssembly { get { return GetType().Assembly; } }
        public Context ApplicationContext { get; private set; }
    }
}