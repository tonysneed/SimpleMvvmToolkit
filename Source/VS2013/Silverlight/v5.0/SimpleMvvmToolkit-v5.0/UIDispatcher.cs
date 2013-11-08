using System;
#if NETFX_CORE
using Windows.UI.Core;
using WindowsDispatcher = Windows.UI.Core.CoreDispatcher;
#else
using System.Windows;
using WindowsDispatcher = System.Windows.Threading.Dispatcher;
#endif

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Helper class for dispatching work across threads.
    /// WPF apps should call Initialize from the UI thread in App_Start.
    /// </summary>
    public sealed class UIDispatcher : IDispatcher
    {
        private static volatile IDispatcher _dispatcher;
        private static readonly object SyncRoot = new object(); 
        private readonly WindowsDispatcher _windowsDispatcher;

        private UIDispatcher(WindowsDispatcher windowsDispatcher)
        {
            _windowsDispatcher = windowsDispatcher;
        }

        /// <summary>
        /// Determines whether the calling thread is the thread associated with this Dispatcher.
        /// </summary>
        /// <returns>
        /// true if the calling thread is the thread associated with this UIDispatcher; otherwise, false.
        /// </returns>
        public bool CheckAccess()
        {
            #if NETFX_CORE
            return _windowsDispatcher.HasThreadAccess;
            #else
            return _windowsDispatcher.CheckAccess();
            #endif
        }

        /// <summary>
        /// Executes the specified delegate asynchronously with the specified array of arguments on the thread the Dispatcher is associated with.
        /// </summary>
        /// <param name="action">A delegate to a method that takes multiple arguments, which is pushed onto the Dispatcher event queue.</param>
        public void BeginInvoke(Action action)
        {
            #if NETFX_CORE
            var asyncAction = _windowsDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
            #else
            _windowsDispatcher.BeginInvoke(action);
            #endif
        }

#if !SILVERLIGHT
        /// <summary>
        /// Invoke from main UI thread.
        /// </summary>
        public static void Initialize()
        {
            #if NETFX_CORE
            WindowsDispatcher windowsDispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            #else
            WindowsDispatcher windowsDispatcher = WindowsDispatcher.CurrentDispatcher;
            #endif
            _dispatcher = new UIDispatcher(windowsDispatcher);
        }

#endif
        /// <summary>
        /// Obtain the current dispatcher for cross-thread marshaling
        /// </summary>
        public static IDispatcher Current
        {
            get
            {
                if (_dispatcher == null)
                {
                    lock (SyncRoot)
                    {
                        #if SILVERLIGHT
                        WindowsDispatcher windowsDispatcher = Deployment.Current.Dispatcher; 
                        #endif
                        #if NETFX_CORE
                        WindowsDispatcher windowsDispatcher = null;
                        var coreWindow = CoreWindow.GetForCurrentThread();
                        if (coreWindow != null)
                            windowsDispatcher = coreWindow.Dispatcher;
                        #endif
                        #if !SILVERLIGHT && !NETFX_CORE
                        WindowsDispatcher windowsDispatcher = WindowsDispatcher.CurrentDispatcher;
                        #endif
                        if (windowsDispatcher != null)
                            _dispatcher = new UIDispatcher(windowsDispatcher); 
                    } 
                }
                return _dispatcher;
            }
        }

        /// <summary>
        /// Execute an action on the UI thread.
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action action)
        {
            if (_dispatcher.CheckAccess()) action();
            else _dispatcher.BeginInvoke(action);
        }
    }
}
