using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace SimpleMvvmToolkitTest
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// </summary>
	public class ReceiverViewModel : ViewModelBase<ReceiverViewModel>
	{
		#region Initialization and Cleanup

		// Default ctor
		public ReceiverViewModel() { }

		#endregion

		#region Notifications

		public event EventHandler<NotificationEventArgs> SimpleMessageReceived;
		public event EventHandler<NotificationEventArgs<int>> OneWayMessageReceived;
		public event EventHandler<NotificationEventArgs<int, bool>> TwoWayMessageReceived;
		public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

		#endregion

		#region Properties

		// Message values
		public string ReceivedMessage { get; set; }
		public int ReceivedData { get; set; }
		public bool SentData { get; set; }

		#endregion

		#region Methods

		public void Subscribe(string token)
		{
			// Register receive methods
			RegisterToReceiveMessages(token, ReceiveSimpleMessage);
            RegisterToReceiveMessages<int>(token, ReceiveOneWayMessage);
            RegisterToReceiveMessages<int, bool>(token, ReceiveTwoWayMessage);
		}

		public void Unsubscribe(string token)
		{
			// Unregister receive methods
            UnregisterToReceiveMessages(token, ReceiveSimpleMessage);
            UnregisterToReceiveMessages<int>(token, ReceiveOneWayMessage);
            UnregisterToReceiveMessages<int, bool>(token, ReceiveTwoWayMessage);
		}

		// Receive methods
		private void ReceiveSimpleMessage(object sender, NotificationEventArgs e)
		{
			ReceivedMessage = e.Message;
			if (SimpleMessageReceived != null) SimpleMessageReceived(this, e);
		}

		private void ReceiveOneWayMessage(object sender, NotificationEventArgs<int> e)
		{
			ReceivedData = e.Data;
			if (OneWayMessageReceived != null) OneWayMessageReceived(this, e);
		}

		private void ReceiveTwoWayMessage(object sender, NotificationEventArgs<int, bool> e)
		{
			ReceivedData = e.Data;
			e.Completed(SentData);
			if (TwoWayMessageReceived != null) TwoWayMessageReceived(this, e);
		}

		#endregion

		#region Completion Callbacks

		// TODO: Optionally add callback methods for async calls to the service agent
		
		#endregion

		#region Helpers

		// Helper method to notify View of an error
		private void NotifyError(string message, Exception error)
		{
			// Notify view of an error
			Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
		}

		#endregion
	}
}