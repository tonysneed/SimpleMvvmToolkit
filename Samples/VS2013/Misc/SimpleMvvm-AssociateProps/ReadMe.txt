Associate Properties

Sometimes it's helpful for a view-model to wrap properties of a model.
For example, you may have a Customer model with FirstName and LastName
properties, but you have a CustomerName property in your view-model that
combines FirstName and LastName.  The CustomerName property would then only
consiste of a getter that accesses FirstName and LastName from the Customer model.
However, updating the Customer model properties will not automatically update
a TextBox in the view that is bound to CustomerName.  That's because the view-model
needs to raise the PropertyChanged event in response to a change in the model
properties.

For this reason I added an AssociateProperties method in ViewModelDetailBase
that will fire PropertyChanged for a view-model property whenever one or more
properties in the model changes.  To use it, just pass in two lamdba expressions:
one for the model property and another for the view-model property.

1. Create a Customer model with FirstName, LastName, Orders, IsActive properties
   - Derive it from ModelBase for data binding support

2. Create a CustomerViewModel that derives from ViewModelDetailBase
   - Add a CustomerName property that returns a formatted string using
     Model.FirstName and Model.LastName
   - Add an OrdersVisibility propery that returns Visible if customer is active,
     or Collapsed if inactive.

3. Add a Customer property that wraps the Model property.
   - In the setter call AssociateProperties so that changes to model properties
     will fire PropertyChanged for dependent properties in the view-model.

	AssociateProperties(m => m.FirstName, vm => vm.CustomerName);
	AssociateProperties(m => m.LastName, vm => vm.CustomerName);
	AssociateProperties(m => m.IsActive, vm => vm.OrdersVisibility);

4. Create a CustomerView setting a new CustomerViewModel as the DataContext
   - Set the Customer property to a new customer
   - Bind controls in the view to corresponding properties in the customer
     model and view-model
	 > Add a TextBlock that is bound to CustomerName in the VM
	 > Bind the Visibility property of orders to OrdersVisibility in the VM

If you run the app you'll notice that updating the FirstName or LastName
text boxes will update the CustomerName text block.  Checking or unchecking
IsActive will show or hide the orders up down control.  If you comment out
calls to AssociateProperties in the view-model, the updates will not take place.
