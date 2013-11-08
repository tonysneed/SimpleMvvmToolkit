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

namespace MvvmMessaging
{
    public partial class ApproveIncreaseView : ChildWindow
    {
        public ApproveIncreaseView(IncreaseInfo info)
        {
            InitializeComponent();
            quantityText.Text = info.Amount.ToString();
            customerText.Text = info.CustomerName;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

