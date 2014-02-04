using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Binding.Touch;

namespace SimpleMvvmiOS
{
    // Sets up IOC container and binding builder
    public class BindingSetup
    {
        public static readonly BindingSetup Instance = new BindingSetup();

        public void Initialize()
        {
            // Return if IOC container already initialized
            if (MvxSimpleIoCContainer.Instance != null)
                return;

            // Init IOC container
            MvxSimpleIoCContainer.Initialize();

            // Register binding builder
            new MvxTouchBindingBuilder().DoRegistration();
        }
    }
}