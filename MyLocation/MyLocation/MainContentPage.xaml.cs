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
using Microsoft.Phone.Controls;

namespace MyLocation
{
    public partial class MainContentPage : PhoneApplicationPage
    {
        public MainContentPage()
        {
            InitializeComponent();
        }

        private void onRefreshClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Refresh button works!");
        }

        private void onMsgReceiveClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Msg receive works!");
        }

        private void onMsgComposeClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Msg compose works!");
        }

        private void onSettingClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("setting button works!");
        }

        private void onTestMenuClickHnadler(object sender, EventArgs e)
        {
            MessageBox.Show("test Menu works!");
        }

    }
}