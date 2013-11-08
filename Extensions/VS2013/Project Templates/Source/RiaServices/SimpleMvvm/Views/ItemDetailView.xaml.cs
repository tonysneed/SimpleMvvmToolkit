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

namespace SimpleMvvm
{
    public partial class ItemDetailView : ChildWindow
    {
        public ItemDetailView()
        {
            InitializeComponent();
        }

        // Set DataContext to ItemDetailViewModel
        public ItemDetailView(ItemDetailViewModel viewModel)
            : this()
        {
            // Set data context to view model
            DataContext = viewModel;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

