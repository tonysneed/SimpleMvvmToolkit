using System;
using System.Net;
using System.Threading;
using System.Windows;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using SimpleMvvmToolkit;

namespace SimpleMvvmToolkitTest
{
	[TestClass]
	public class MessagingTests
	{
		[TestMethod]
		public void TestSimpleMessage()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestSimpleMessage");

			// Set message content
			sender.SentMessage = "hello";

			// Capture orig values
			int origReceiverData = receiver.ReceivedData;
			bool origSenderData = sender.ReceivedData;

			// Listen for response
			var wait = new AutoResetEvent(false);
			receiver.SimpleMessageReceived += (o, args) => wait.Set();

			// Send message
            sender.SendSimpleMessageAsync(MessageTokens.Test + "_TestSimpleMessage");

			// Wait for response
			if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
				Assert.Fail("Message not received in timeout");

			// Unregister to receive messages
            receiver.Unsubscribe(MessageTokens.Test + "_TestSimpleMessage");

			// Assert message was received
			Assert.AreEqual<string>(sender.SentMessage, receiver.ReceivedMessage);

			// Assert receiver and sender data have not changed
			Assert.AreEqual<int>(origReceiverData, receiver.ReceivedData);
			Assert.AreEqual<bool>(origSenderData, sender.ReceivedData);
		}

		[TestMethod]
		public void TestOneWayMessage()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestOneWayMessage");

			// Set message content
			sender.SentData = new Random().Next(100);

			// Capture orig values
			string origReceiverMessage = receiver.ReceivedMessage;
			bool origSenderData = sender.ReceivedData;

			// Listen for response
			var wait = new AutoResetEvent(false);
			receiver.OneWayMessageReceived += (o, args) => wait.Set();

			// Send message
            sender.SendOneWayMessageAsync(MessageTokens.Test + "_TestOneWayMessage");

			// Wait for response
			if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
				Assert.Fail("Message not received in timeout");

			// Unregister to receive messages
            receiver.Unsubscribe(MessageTokens.Test + "_TestOneWayMessage");

			// Assert message was received
			Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

			// Assert receiver and sender data have not changed
			Assert.AreEqual<string>(origReceiverMessage, receiver.ReceivedMessage);
			Assert.AreEqual<bool>(origSenderData, sender.ReceivedData);
		}

		[TestMethod]
		public void TestTwoWayMessage()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestTwoWayMessage");

			// Capture orig values
			string origReceiverMessage = receiver.ReceivedMessage;

			// Set message content
			sender.SentData = new Random().Next(100);
			receiver.SentData = true;

			// Listen for response
			var wait = new AutoResetEvent(false);
			receiver.TwoWayMessageReceived += (o, args) => wait.Set();

			// Send message
            sender.SendTwoWayMessageAsync(MessageTokens.Test + "_TestTwoWayMessage");

			// Wait for response
			if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
				Assert.Fail("Message not received in timeout");

			// Unregister to receive messages
            receiver.Unsubscribe(MessageTokens.Test + "_TestTwoWayMessage");

			// Assert data was received
			Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

			// Assert data was sent
			Assert.AreEqual<bool>(receiver.SentData, sender.ReceivedData);

			// Assert receiver and sender data have not changed
			Assert.AreEqual<string>(origReceiverMessage, receiver.ReceivedMessage);
		}
	}
}