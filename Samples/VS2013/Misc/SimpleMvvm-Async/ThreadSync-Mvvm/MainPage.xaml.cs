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

// Toolkit namespace
using SimpleMvvmToolkit;
using System.Diagnostics;

namespace ThreadSync
{
    public partial class MainPage : UserControl
    {
        // Reference view model
        MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            // Get model from data context
            viewModel = (MainViewModel)LayoutRoot.DataContext;

            // Subscribe to notifications from the model
            viewModel.MaxReachedNotice += OnMaxReachedNotice;
        }

        void OnMaxReachedNotice(object sender, NotificationEventArgs<bool, bool> e)
        {
            QueryUserDialog dialog = new QueryUserDialog();
            dialog.Closed += (s, ea) =>
            {
                // Set the flag
                e.Completed(dialog.DialogResult == false);
            };
            dialog.Show();
        }
    }
}
