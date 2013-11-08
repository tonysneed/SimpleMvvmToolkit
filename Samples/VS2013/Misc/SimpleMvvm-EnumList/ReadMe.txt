Enum Lists Sample

This sample shows how to use the GetEnumItems method on the static
Extensions class to convert an enum to a list of values that can 
by displayed in an items control such as a combo box.

1. Create a Weekday enum with values for the days of the week.

2. Create a MainPageViewModel class that extends ViewModelBase<MainPageViewModel>
   - Using the mvvmprop code snippet, add a Weekdays property that returns 
     List<EnumItem<int>>
     > EnumItem is a class that has a value property and a name property
	   to represent an enum item
   - Add a SelectedWeekday property (EnumItem<int>)
   - Add read-only WeekdayValue (int) and WeekdayName (string) properties

3. Add a combo box and two text blocks to the MainPage view and bind them
   to properties on the view-model.