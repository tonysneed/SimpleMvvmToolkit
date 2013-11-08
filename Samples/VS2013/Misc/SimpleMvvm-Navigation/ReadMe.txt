Simple Mvvm Navigation Sample

We start out with a solution that contains the SimpleMvvmToolkit project, then we add
a new Silverlight app using the Silverlight Navigation Application project template.
Notice how MainPage.xaml contains a navigation:Frame element, which has a UriMapper
that will take a path with the view name and map it to a xaml file in the Views folder.

To convert this to use MVVM all we need to do is set the DataContext of the main page
to a MainPageViewModel that has a SelectedPage (Uri) property. We'll bind the Source
property of the navigation Frame to SelectedPage, then create a command that calls
a Navigate method, passing a string parameter to set SelectedPage. Hyperlink buttons
on the main page can then bind their commands to the NavigateCommand in the view-model.
Using commands here instead of Blend event triggers makes sense because we want to pass
a parameter with the view name. (CallMethodAction does not take a parameter, so we would
have to have a separate navigate method for each button.)

1. Let's start out by adding a reference to the SimpleMvvmToolkit project.
   - Then add a class by right-clicking on the Navigation project, selecing 
     Add New Item, then choosing the Silverlight / Mvvm category and clicking on 
	 SimpleMvvmViewModel.
	 > Give it the name MainPageViewModel.cs.
   a. Expand the Properties region, then type mvvmprop and press Tab to expand a
      property code snippet.  Give it the name SelectedPage with a type of Uri.

	private Uri selectedPage;
	public Uri SelectedPage
	{
		get { return selectedPage; }
		set
		{
			selectedPage = value;
			NotifyPropertyChanged(m => m.SelectedPage);
		}
	}

   b. Add a Navigate method accepting a string parameter
      > Create a new Uri using the string parameter and pass it to the Navigate method

	public void Navigate(string pageName)
	{
		Uri pageUri = new Uri("/" + pageName, UriKind.Relative);
		this.SelectedPage = pageUri;
	}

   c. Add a command using the mvvmcommand snippet that calls Navigate:

	public ICommand NavigateCommand
	{
		get
		{
			return new DelegateCommand<string>(Navigate);
		}
	}

2. Edit MainPage.xaml by adding MainPageViewModel as a resource.
   NOTE: Normally we would use a view-model locator, but we'll keep it simple for now.
   a. Create UserControl.Resources element and MainPageViewModel from current namespace
      > Assign vm as the key

	<UserControl.Resources>
		<my:MainPageViewModel x:Key="vm" xmlns:my="clr-namespace:MvvmNavigation"/>
	</UserControl.Resources>  

   b. Set the DataContext of the Grid to the vm static resource

   <Grid x:Name="LayoutRoot" Style="{StaticResource LayoutRootGridStyle}"
      DataContext="{StaticResource vm}">

   c. Bind the Source property of navigation:Frame to the SelectedPage property

	<navigation:Frame x:Name="ContentFrame" Style="{StaticResource ContentFrameStyle}" 
		Source="{Binding Path=SelectedPage, Mode=TwoWay}" 
		Navigated="ContentFrame_Navigated" NavigationFailed="ContentFrame_NavigationFailed">

  d. Bind Command property of each HyperlinkButton to the NavigateCommand in the view-model
     - Set CommandProperty to the name of the view you want to navigagte to.
	   > The uri mapper resolves the uri to point to the Views folder
	   > Leave out the .xaml extension

	<HyperlinkButton x:Name="homeLink" Style="{StaticResource LinkStyle}" 
		Command="{Binding Path=NavigateCommand}" CommandParameter="Home" 
		TargetName="ContentFrame" Content="home"/>
									 
	<Rectangle x:Name="Divider1" Style="{StaticResource DividerStyle}"/>
					
	<HyperlinkButton x:Name="aboutLink" Style="{StaticResource LinkStyle}" 
		Command="{Binding Path=NavigateCommand}" CommandParameter="About"
		TargetName="ContentFrame" Content="about"/>

You can now run the app and click on the home and about buttons to see navigation
take place.

3. Add customer model, view, and view-model.
   - It doesn't matter how you put these together, except that the view will
     have a Save button. In a subsequent step you will navigate back to home
	 after saving.

   a. Add a HyperLinkButton to the main page xaml that navigates to CustomerView

	<HyperlinkButton x:Name="customerLink" Style="{StaticResource LinkStyle}" 
		Command="{Binding Path=NavigateCommand}" CommandParameter="CustomerView"
		TargetName="ContentFrame" Content="customer"/>

4. Flesh out the Save method in CustomerViewModel
   - Pop up a message box to confirm the save
   - If user clicked OK, then send a message to navigate to home
     > You will need to create a MessageTokens class with a const called
	   Navigation (doesn't really matter what the string value is)
	 > Optionally you can also create a PageNames class with constants
	   for the name of each view.

	public void Save()
	{
		// If user confirms save, send a message to navigate home
		if (MessageBox.Show("Click OK to confirm save", "Save Customer",
			MessageBoxButton.OKCancel) == MessageBoxResult.OK)
		{
			MessageBus.Default.Notify(MessageTokens.Navigation, this,
				new NotificationEventArgs(PageNames.Home));
		}
	}

5. Now to to MainPageViewModel and register to recieve navigation messages

   a. Create a callback method (EventHandler<NotificationEventArgs) in which 
      you call Navigate, passing e.Message

	void OnNavigationRequested(object sender, NotificationEventArgs e)
	{
		Navigate(e.Message);
	}

   b. Add a ctor to the class in which you call MessageBus.Default.Register,
      passing MessageTokens.Navigation and the OnNavigationRequested method.

	public MainPageViewModel()
	{
		// STEP 5: Subscribe to navigation message, passing OnNavigationRequested
		// for the callback method
		MessageBus.Default.Register(MessageTokens.Navigation, OnNavigationRequested);
	}

