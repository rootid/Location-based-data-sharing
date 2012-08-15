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
using System.Device.Location;
using System.Diagnostics;
using MyLocation.Dialogs;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.Xml;

namespace MyLocation
{
    /***
     *Provide start point of application
     * **/
    public partial class MainPage : PhoneApplicationPage
    {

        //private GeoCoordinateWatcher geoWatcher;
        public static String userName;
        private String passWord;
       
        private WebClient webClient;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //InitializeList();
            // showLoginPopUp();
            // getLocationUpdate();

        }

        private void uNameTextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void onClickNewUserBtn(object sender, RoutedEventArgs e)
        {
            NewUserDialog newUserWindow = new NewUserDialog();
            newUserWindow.Show();
           // newUserWindow.Closed += new EventHandler(newUserWindowClosed);
        }
   
        private void onClickLogInBtn(object sender, RoutedEventArgs e)
        {
            //get the username and password 

            //After sucessful login show the user next page
            userName = this.uNameTextBox.Text;
            passWord = this.pwdBox.Password;
            if (userName.Equals("") || passWord.Equals(""))
            {
                MessageBox.Show("Please provide username and password");
            }
            else
            {
                initializeWebClient();
            }
        }

        #region HTTP authentication
        private void initializeWebClient()
        {
            //http://23.22.46.152/LBTweets/WebServices.asmx/GetTweetsByTags?
            //latitude=1&longitude=3&radius=10000&tags=shopping,entertainment
            webClient = new WebClient();
            String regiUrl = String.Format(Util.AUTHENTICATE_USER + Util.PARA_PHONE_NUMBER + "{0}" +
                Util.PARA_PASSWORD + "{1}",
                userName, passWord);
            Debug.WriteLine("Regi url :" + regiUrl);
            webClient.DownloadStringAsync(new Uri(regiUrl));
            webClient.DownloadStringCompleted += userAuthentication;
        }

        private void userAuthentication(Object sender ,DownloadStringCompletedEventArgs e) {
            if (e.Error == null)
            {
                Debug.WriteLine("Using WebClient: " + e.Result);
                parseResult(e.Result);
            }
            else
            {
                Debug.WriteLine("Using WebClient: " + e.Error.Message);
                clearAllFields();
                MessageBox.Show("Please connect to internet");
            }
          
        }

        private void parseResult(String aResult) {
            try
            {
                XDocument document = XDocument.Parse(aResult);
                XNamespace tempuri = "http://tempuri.org/";
                var result = document.Element(tempuri + "string").Value;
                Debug.WriteLine(":Result:" + result);
                if (result.Equals(Util.AUTHENTICATED_YES))
                {
                    NavigationService.Navigate(new Uri("/MsgRecvPivot.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Please provide correct username and password!");
                }
                clearAllFields();
            }
            catch (Exception e) {
                clearAllFields();
                MessageBox.Show("Please connecto to internet");

            }
        }

        private void clearAllFields() { 
            this.uNameTextBox.Text = "";
            this.pwdBox.Password = "";
        }
        #endregion
    }
}