using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MvvmMessaging
{
    public class ApprovalInfo
    {
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public bool Result { get; set; }
        public ApprovalInfo(string customerName, int amount, bool result)
        {
            CustomerName = customerName;
            Amount = amount;
            Result = result;
        }
    }
}
