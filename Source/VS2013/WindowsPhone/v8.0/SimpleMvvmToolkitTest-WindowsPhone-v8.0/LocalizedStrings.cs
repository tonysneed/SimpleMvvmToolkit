using SimpleMvvmToolkitTest_WindowsPhone_v8._0.Resources;

namespace SimpleMvvmToolkitTest_WindowsPhone_v8._0
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}