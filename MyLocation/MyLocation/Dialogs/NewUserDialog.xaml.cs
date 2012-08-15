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
using System.Diagnostics;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
namespace MyLocation.Dialogs
{
    /**
     * This class is used to show the user dialog prompt and to create user.
     * **/
    public partial class NewUserDialog : ChildWindow
    {
        private String userNameNew;
        private String passWordNew;
        private WebClient webClient;
        private String tags;
        public NewUserDialog()
        {
            InitializeComponent();
        }

        private void setDefaultSetting()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Clear();
            settings.Add(Util.CAMPUS_UPDATE, Util.YES);
            settings.Add(Util.SPORTS, Util.YES);
            settings.Save();
            tags = Util.CAMPUS_UPDATE + "," + Util.SPORTS;
        }

        void onOkButtonClicked(object sender, EventArgs e) {
            this.DialogResult = true;
            userNameNew = uNameTextBox.Text;
            passWordNew = pwdBox.Password;
            //set default settings
            setDefaultSetting();
            populateUrl();
            Debug.WriteLine("Ok button selected ");
            //If size is zero show error message

        }

    
        private void uNameTextChanged(object sender, RoutedEventArgs e)
        {

        }
        void onCancelButtonClicked(object sender, EventArgs e) {
            this.DialogResult = false;
            Debug.WriteLine("cancel button selected ");
        }

        #region Handle user registration
        private void populateUrl()
        {
            webClient = new WebClient();
            String regiUrl = String.Format(Util.REGISTER_USER + Util.PARA_PHONE_NUMBER + "{0}" +
                Util.PARA_PASSWORD + "{1}"+Util.PARA_INTEREST+"{2}",
                userNameNew, passWordNew,tags);
            Debug.WriteLine("Regi url :" + regiUrl);
            webClient.DownloadStringAsync(new Uri(regiUrl));
            webClient.DownloadStringCompleted += userRegistration;
        }

        private void userRegistration(Object sender,DownloadStringCompletedEventArgs e) {
            if (e.Error == null)
            {
                Debug.WriteLine("Using WebClient: " + e.Result);
                parseResult(e.Result);
            }
            else
            {
                Debug.WriteLine("Using WebClient: " + e.Error.Message);
            }
        }

        private void parseResult(String aResult)
        {
            XDocument document = XDocument.Parse(aResult);
            XNamespace tempuri = "http://tempuri.org/";
            var result = document.Element(tempuri + "string").Value;
            Debug.WriteLine(":Result:" + result);
            if (result.Equals(Util.USER_REGISTRATION_YES))
            {
                MessageBox.Show("User "+userNameNew+ " created");
            }
            else
            {
                MessageBox.Show("User not created");
            }
        }
        #endregion

    }
}
