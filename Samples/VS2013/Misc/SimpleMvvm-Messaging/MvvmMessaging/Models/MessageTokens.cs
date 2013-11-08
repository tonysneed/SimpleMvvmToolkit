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
    // STEP 1: Define a message token for the increase orders notification
    public class MessageTokens
    {
        public const string IncreaseOrders = "OrdersIncreaseToken";
    }
}
