using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Windows;

using SimpleMvvmToolkit;

namespace SimpleMvvm
{
    // Add ServiceAgentExport attribute, setting AgentType to Mock
    public class MockItemListServiceAgent : IItemListServiceAgent
    {
        // Mock data
        List<Item> mockItems = MockItems.GetItems();

        // Get items asynchonously using BackgroundWorker
        public void GetItems(Action<List<Item>, Exception> completed)
        {
            // Use background worker to simulate async operation
            var bw = new BackgroundWorker();

            // Handle DoWork event to perform task on a background thread
            bw.DoWork += (s, ea) =>
                {
                    // Simulate work by sleeping
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    // Set result to mock categories
                    ea.Result = mockItems;
                };

            // Handle RunWorkerCompleted event by invoking completed callback
            bw.RunWorkerCompleted += (s, ea) =>
                {
                    if (ea.Error != null)
                        completed(null, ea.Error);
                    else
                        completed((List<Item>)ea.Result, null);
                };

            // Call RunWorkerAsync to begin operation
            bw.RunWorkerAsync();
        }

        // Add item
        public void AddItem(Item item)
        {
            mockItems.Add(item);
        }

        // Remove item
        public void RemoveItem(Item item)
        {
            mockItems.Remove(item);
        }

        // TODO: Implement mock saves (optional)
        public void SaveChanges(Action<Exception> completed)
        {
            MessageBox.Show("SaveChanges not implemented");
        }

        // TODO: Implement mock change rejection (optional)
        public void RejectChanges()
        {
            MessageBox.Show("RejectChanges not implemented");
        }
    }
}
