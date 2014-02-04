using System;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Facilitates communication among view-models.
    /// To prevent memory leaks weak references are used.
    /// </summary>
    public class MessageBus : MessageBusCore
    {
        private static MessageBus _instance;
        private static readonly object StaticLock = new object();

        // Prevent direct instantiation
#if PORTABLE || XAMARIN
        private MessageBus()
            : base(null)
        {
        }
#else
        private MessageBus()
            : base(UIDispatcher.Current)
        {
        }
#endif

        /// <summary>
        /// Singleton of MessageBus.
        /// </summary>
        public static MessageBus Default
        {
            get
            {
                lock (StaticLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageBus();
                    }
                    return _instance;
                }
            }
        }
    }
}
