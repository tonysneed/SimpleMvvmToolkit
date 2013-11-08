using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace ThreadSync
{
    public partial class MainPage : UserControl
    {
        SynchronizationContext _sync;

        public MainPage()
        {
            InitializeComponent();
            _sync = SynchronizationContext.Current;
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            goButton.IsEnabled = false;
            int count = Convert.ToInt32(valueNumericUpDown.Value);
            Thread worker = new Thread(DoWork);
            worker.Name = "My Worker Thread";
            worker.Start(count);
        }

        // Executed on a worker thread
        void DoWork(object arg)
        {
            // Flag
            bool cancelled = false;

            // Wait handle
            var wait = new AutoResetEvent(false);

            int count = (int)arg;
            for (int i = 0; i < count; i++)
            {
                // Pause
                Thread.Sleep(500);

                // Get max
                int max = 0;
                //Dispatcher.BeginInvoke(() =>
                //    {
                //        int.TryParse(maxTextBox.Text, out max);
                //        wait.Set();
                //    });
                _sync.Post(o =>
                {
                    int.TryParse(maxTextBox.Text, out max);
                    wait.Set();
                }, null);
                wait.WaitOne();

                // See if we're at the max
                int val = (i + 1) * 10;
                if (val == max)
                {
                    // Prompt the user to continue
                    //Dispatcher.BeginInvoke(() =>
                    //{
                    //    QueryUserDialog dialog = new QueryUserDialog();
                    //    dialog.Closed += (s, ea) =>
                    //    {
                    //        // Set the flag
                    //        cancelled = (dialog.DialogResult == false);

                    //        // Signal the wait handle
                    //        wait.Set();
                    //    };
                    //    dialog.Show();
                    //});
                    _sync.Post(o =>
                    {
                        QueryUserDialog dialog = new QueryUserDialog();
                        dialog.Closed += (s, ea) =>
                        {
                            // Set the flag
                            cancelled = (dialog.DialogResult == false);

                            // Signal the wait handle
                            wait.Set();
                        };
                        dialog.Show();
                    }, null);

                    // Here we want to pause for the user
                    wait.WaitOne();

                    // Exit loop if cancelled
                    if (cancelled) break;
                }

                // Marshall call to the UI thread to set result
                //Dispatcher.BeginInvoke((Action<int>)(n => resultLabel.Text = n.ToString()), val);
                _sync.Post(n => resultLabel.Content = n.ToString(), val);
            }

            //Dispatcher.BeginInvoke(() => goButton.IsEnabled = true);
            _sync.Post(o => goButton.IsEnabled = true, null);
        }
    }
}
