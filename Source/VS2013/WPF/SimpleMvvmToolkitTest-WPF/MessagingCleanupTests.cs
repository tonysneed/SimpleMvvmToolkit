using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using SimpleMvvmToolkit;
using System.Threading;

namespace SimpleMvvmToolkitTest
{
	[TestClass]
	public class MessagingCleanupTests
	{
		[TestMethod]
		public void TestRepeatSend()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestRepeatSend");

			// Set init value
			int val = new Random().Next(100);

			// Iterate 3 times
			for (int i = 0; i < 3; i++)
			{
				// Set message content
				sender.SentData = val;

                // Listen for response
                var wait = new AutoResetEvent(false);
                receiver.OneWayMessageReceived += (o, args) => wait.Set();
                
                // Send message
                sender.SendOneWayMessageAsync(MessageTokens.Test + "_TestRepeatSend");

                // Wait for response
                if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
                    Assert.Fail("Message not received in timeout");
                
                // Assert message was received
				Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

				// Increment value
				val += 100;
			}

			// Unregister to receive messages
            receiver.Unsubscribe(MessageTokens.Test + "_TestRepeatSend");
		}

		[TestMethod]
		public void TestReceiverCleanup()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestReceiverCleanup");

			// Set message content
			sender.SentMessage = "hello";

			// Listen for response
			var wait = new AutoResetEvent(false);
			receiver.SimpleMessageReceived += (o, args) => wait.Set();

			// Send message
            sender.SendSimpleMessageAsync(MessageTokens.Test + "_TestReceiverCleanup");

			// Wait for response
			if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
				Assert.Fail("Message not received in timeout");
			
			// Assert message was received
			Assert.AreEqual<string>(sender.SentMessage, receiver.ReceivedMessage);

			// Try to GC reciever
			var weakReceiver = new WeakReference(receiver);
			receiver = null;
			GC.Collect();
            #if NETFX_CORE
		    Task.Delay(1000).Wait();
            #else
			Thread.Sleep(1000);
            #endif

            // Assert the receiver was allowed to be GC'd
			Assert.IsFalse(weakReceiver.IsAlive, "Receiver has not been GC'd");

			// Send another message
			sender.SentMessage = "hello again";
            sender.SendSimpleMessage(MessageTokens.Test + "_TestReceiverCleanup");

			// Assert the receiver is still not alive
			Assert.IsFalse(weakReceiver.IsAlive, "Receiver has not been GC'd after sending message");
		}

		[TestMethod]
		public void TestReceiverUnregister()
		{
			// Create sender and receiver
			var sender = new SenderViewModel();
			var receiver = new ReceiverViewModel();

			// Register receiver to get messages
            receiver.Subscribe(MessageTokens.Test + "_TestReceiverUnregister");

			// Set message content
			sender.SentData = new Random().Next(100);

			// Listen for response
			var wait = new AutoResetEvent(false);
			receiver.OneWayMessageReceived += (o, args) => wait.Set();

			// Send message
            sender.SendOneWayMessageAsync(MessageTokens.Test + "_TestReceiverUnregister");

			// Wait for response
			if (!wait.WaitOne(TimeSpan.FromSeconds(15)))
				Assert.Fail("Message not received in timeout");

			// Assert message was received
			Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

			// Unregister receiver
            receiver.Unsubscribe(MessageTokens.Test + "_TestReceiverUnregister");

			// Send another message
			sender.SentData += 100;
            sender.SendOneWayMessage(MessageTokens.Test + "_TestReceiverUnregister");

			// Assert message was NOT received
			Assert.AreNotEqual<int>(sender.SentData, receiver.ReceivedData);
		}
	}
}