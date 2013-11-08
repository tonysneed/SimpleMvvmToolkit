using System;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Provides an abstraction over UI platform-specific threading models.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Determines whether the calling thread is the thread associated with this Dispatcher.
        /// </summary>
        /// <returns>
        /// True if the calling thread is the thread associated with this UIDispatcher; otherwise, false.
        /// </returns>
        bool CheckAccess();

        /// <summary>
        /// Executes the specified delegate asynchronously with the specified array of arguments on the thread the Dispatcher is associated with.
        /// </summary>
        /// <param name="action">A delegate to a method that takes multiple arguments, which is pushed onto the Dispatcher event queue.</param>
        void BeginInvoke(Action action);
    }
}
