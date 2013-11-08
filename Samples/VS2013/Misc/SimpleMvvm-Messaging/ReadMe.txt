Mvvm Messaging Sample

Here we want to communicate between two different view-models, CustomerListViewModel
and CustomerDetailViewModel. When the IncrementOrders method is called in the list VM
we send a message to the detail VM.  However, because the list VM does not have a 
direct reference to the detail VM, we can communicate indirectly via the MessageBus.

This example will show message multicasting, with a notification broadcast to
multiple listeners.  And it will also show two-way communication, with the
listener calling back the notifier with a specified result. For two-way communication
pass two type arguments to the MessageBus.Default.Register: TOutgoing for data sent from
the notifier, and TIncoming for data sent from the subscriber back to the notifier.

VERSION 2 UPDATE: Instead of using the MessageBus directly you now simply call methods
in the ViewModelBase class to send and receive messages:
1) Call RegisterToReceiveMessages to start receveing messages with a specific token
2) Call SendMessage to send a message to recipients that registered with the token

1. Open MessageTokens.cs and define a string constant that will serve as a message 
   token for the increase orders notification.

2. Open CustomerDetailViewModel and add code in the ctor to get notified when
   a request is made to increase orders.
   - Call RegisterToReceiveMessages, specifying two type arguments:
     <IncreaseInfo, ApprovalInfo>
	 > IncreaseInfo is for outgoing data from the message sender
	 > ApprovalInfo is for incoming data from the receiver back to the sender

    RegisterToReceiveMessages<IncreaseInfo, ApprovalInfo>
        (MessageTokens.IncreaseOrders, OnOrdersIncrease);

3. Insert a method that will get invoked when a notification takes place
   using the IncreaseOrders message token, which is just a string const.
   - The method should take two parameters: one for sender (object) and another
     for NotificationEventArgs<IncreaseInfo, ApprovalInfo>.
   - In the method show a ApprovalIncreaseView dialog if the request is
     for the customer belonging to the view-model instance.
	 > In the Closed event of the dialog, you will call back the notifier by
	   invoking the Completed event args property.

	void OnOrdersIncrease(object sender, NotificationEventArgs
		<IncreaseInfo, ApprovalInfo> e)
	{
		if (e.Data.CustomerName != Customer.CustomerName) return;

		ApproveIncreaseView appoveView = new ApproveIncreaseView(e.Data);
		appoveView.Closed += (s, ea) =>
		{
			// Callback notifier with result
			ApprovalInfo approveInfo = new ApprovalInfo
				(Customer.CustomerName, e.Data.Amount,
					(bool)appoveView.DialogResult);
			e.Completed(approveInfo);
		};
		appoveView.Show();
	}

4. Open CustomerListViewModel and add code to the IncreaseOrders method that
   sends a message to subscribers when an customer orders is increased.
   - Specify a method that the subscriber will callback with the result

    SendMessage(MessageTokens.IncreaseOrders, new NotificationEventArgs
        <IncreaseInfo, ApprovalInfo>(null, increaseInfo, OnIncreaseResponse));

5. Insert a method called OnIncreaseResponse that accepts an ApprovalInfo parameter
   - Set the MessageText property depending on information in ApprovalInfo
   - Reverse the increase if the result is false

	void OnIncreaseResponse(ApprovalInfo approve)
	{
		// Set message text
		string resultText = approve.Result ? "approved" : "rejected";
		MessageText = string.Format("Order quantity of {0} {1} for {2}",
			approve.Amount, resultText, approve.CustomerName);
		MessageVisibility = Visibility.Visible;

		// Reverse increase if rejected
		if (!approve.Result)
		{
			SelectedCustomer.Orders -= 1000;
		}
	}

When you run the app, click the Increase Orders button. This will push a message
to each instance of CustomerDetailViewModel. If the name of the current customer
matches that instance's customer name, the view-model will invoke the Completed
property of the NotificationEventArgs, which in this case is the OnIncreaseResponse
method of CustomerListViewModel. 