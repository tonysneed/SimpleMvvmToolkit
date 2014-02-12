ReadMe for Simple Mvvm Toolkit iOS Project Template

This provides a starter project with sample code files for models, view-models,
views, and services using Xamarin and MvvmCross Core.

The project was created using Xamarin Studio on Mac OSX with the Alpha update channel.
The project type for iOS was a Universal Storyboard with a Single View Application,
in order to support both iPhone and iPad deployment with a storyboard designer.

MvvmCross Core 3.1.1 (Hot Tuna) is used for data binding support. It requires initialization
of the IOC and binding context from a setup class.  Then bindings are applied in the
ViewDidLoad method of the main view controller, which is specially configured.

Once the project is added via Visual Studio, it can be opened using Xamarin Studio
on the Mac in order to alter views from the story boards.

The following steps were followed in order to create this project. You may replicate them
in a new project, but this is not necessary when using the Visual Studio project template.

1. Create the project using Xamarin Studio on the Mac, in order to select a template
   that uses story boards
   - This will allow you to use a graphical designer for iPhone and iPad apps
   - Drag visual elements from the toolbox onto the story boards
   - Give names to all elements you wish to bind to a view model

2. You can switch back to Windows and use Visual Studio 2013 to open the solution
   - This is mainly for convenience, because adding NuGet packages is also possible
     in Xamarin Studio by adding the NuGet add-in.

3. Add the NuGet package for the Simple MVVM Toolkit
   - You do not need to select the "portable" version of the toolkit unless you want to
     create common view models for both Windows Phone and other platforms.
     > The portable view models project template is intended for this purpose.

4. Add the NuGet package for Mvvm Cross Core v 3.1.1 or greater
   - At this time the package is still in beta, so you will beed to select
     Include Prerelease in the NuGet package manageer.

5. Add a reference to the System.Windows assembly version 2.0.5.0
   - This is required to support INotifyDataErrorInfo for validation

6. Add a class to handle setting up the Mvx IOC container and register the Touch
   Binding Builder
   - Add a Framework folder with a BindingSetup class in it
   - Add a public static Instance method to create a new BindingSetup
   - Add an Initialize method with code to set up the IOC container and binding builder

7. Add models and view model classes to the project
   - Derive each model from ModelBase<T>
   - Use the mvvmprop code snippet to add bindable properties
   - Derive view models from either ViewModelBase or ViewModelDetailBase

8. Add service agent classes to the project
   - Create one or more service agent interfaces
   - Create implementation classes

9. Add code to AppDelegate.cs to initialize the binding setup
   - Override FinishedLaunching
   - Call BindingSetup.Instance.Initialize

10. Configure the view controller class to create bindings to the view model
    - Alter the class definition to implement IMvxBindable
	  > This allows for calling extension methods to create binding context and set
	- Add a BindingContext property of type IMvxBindingContext
	- Add a DataContext property of type object that wraps BindingContext.DataContext
	- Override Dispose to clear all bindings on the BindingContext
	- Add code to the ctor to create the binding context, then set the DataContext
	  to a new view model
	  > First create the service agent, then pass it to the view model ctor

11. Add code to the ViewDidLoad method to set up the view model bindings
    - Call this.CreateBindingSet<TViewController, TViewModel>
	- Call Bind on the binding set, passing VM property via a lambda expression
	  > Check for null on visual element
	- Call Apply on the binding set

12. Build the project then test using the iPhone or iPad simulator
    - At this point, you can switch back to Xamarin Studio on the Mac for testing
	  with both iPhone and iPad simulators