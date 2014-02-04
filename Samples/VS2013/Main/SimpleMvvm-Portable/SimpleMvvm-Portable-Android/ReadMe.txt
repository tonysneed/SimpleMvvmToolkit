ReadMe for Simple Mvvm Toolkit Android Project Template

This provides a starter project with sample code files for models, view-models,
views, and services using Xamarin and MvvmCross Core.

If you have the business license or trial for Xamarin Visual Studio integration,
you can use Visual Studio 2013 exclusively for this project.  Otherwise, you
can use Xamarin Studio (either Mac or Windows), which is free, for Andriod development.

MvvmCross Core 3.1.1 (Hot Tuna) is used for data binding support. It requires initialization
of the IOC and binding context from a setup class.  Then bindings are applied in the
ViewDidLoad method of the main view controller, which is specially configured.

The following steps were followed in order to create this project. You may replicate them
in a new project, but this is not necessary when using the Visual Studio project template.

1. Add the following file to the Resources/Values folder: MvxBindingAttributes.xml
   - Content should match that of the file in this project
   - Build action should be set to AndroidResource

2. Add the NuGet package for the Simple MVVM Toolkit
   - You do not need to select the "portable" version of the toolkit unless you want to
     create common view models for both Windows Phone and other platforms.
     > The portable view models project template is intended for this purpose.

3. Add the NuGet package for Mvvm Cross Core v 3.1.1 or greater
   - At this time the package is still in beta, so you will beed to select
     Include Prerelease in the NuGet package manageer.

4. Add models and view model classes to the project
   - Derive each model from ModelBase<T>
   - Use the mvvmprop code snippet to add bindable properties
   - Derive view models from either ViewModelBase or ViewModelDetailBase

5. Add service agent classes to the project
   - Create one or more service agent interfaces
   - Create implementation classes

6. Add a class to handle setting up the Mvx IOC container and register the Binding Builder
   - Add a Framework folder with a BindingSetup class in it
   - Implement IMvxAndroidGlobals
     > ExecutableNamespace returns the namespace name
	 > ExecutableAssembly returns GetType().Assembly
	 > ApplicationContext has public getter and private setter
   - Add a public static Instance method to create a new BindingSetup
   - Add an Initialize method with code to set up the IOC container and binding builder
     > Initialize MvxSimpleIoCContainer
	 > Register Android globals
	 > Register binding builder
	 > Add Android widget assembly to the view cache

7. Configure the main Activity class to wire up various parts needed for data binding
   - Modify Activity1 class to implement IMvxLayoutInflater
   - Add field: private MvxAndroidBindingContext _bindingContext
   - Add code to OnCreate after calling base.OnCreate
     > Initialize binding setup
	 > Create service agent and view model
	 > Create binding context, passing view model
	 > Create view by inflating binding on binding context
	 > Set content view passing inflated view with bindings

8. Add bindings to elements on Main view
   - Open Main.axml with XML text editor
   - Add namespace declaration: xmlns:local="http://schemas.android.com/apk/res-auto"
   - Add element bindings
     > local:MvxBind="Text BannerText"
	 > local:MvxBind="Text Model.CustomerId" (etc)
	 > local:MvxBind="Click NewCustomerCommand"