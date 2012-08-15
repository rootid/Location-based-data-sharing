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
using MyLocation.Dialogs;
using System.Diagnostics;
using System.Device.Location;
namespace MyLocation
{
    /**
     * This class is used to provide user input text screen
     * **/
    public partial class TextInputView : PhoneApplicationPage
    {
        //TODO : code refactoring for geolocationwatcher make it singelton
        private String tags;
        private String tweet;
        private String locString;
        private String lattitude;
        private String longitude;
        private GeoCoordinateWatcher geoWatcher;
        private WebClient webclient;
        public TextInputView()
        {
            InitializeComponent();
            getLocationUpdate();
        }

        private void onTweetClick(object sender, RoutedEventArgs e) 
        {
            tweet = txtBoxMultiLine.Text;
            txtBoxMultiLine.Text = "";
            SelectCatgories selectWindow = new SelectCatgories();
            selectWindow.Show();
            selectWindow.Closed += new EventHandler(selectWindowClosed);
        }


        void selectWindowClosed(object sender, EventArgs e)
        {
            SelectCatgories filterWindow = sender as SelectCatgories;
            bool isOnlyTag = false;
            Debug.WriteLine("child window closed");
            if (Util.tweetTagList.Count > 0) {
                Debug.WriteLine("Selected tag :");
                foreach (String str in Util.tweetTagList) {
                    if (isOnlyTag)
                    {
                        tags += ",";
                    }
                    tags += str;
                    isOnlyTag = true;
                    
                }
            } 
            else {
                Debug.WriteLine("Select at least one tag");
            }
            if (filterWindow.DialogResult.HasValue && filterWindow.DialogResult.Value)
            {
                
            }
            Debug.WriteLine( "\n+username "+ MainPage.userName + "\ntweet" +tweet+ "\ntags :" +
                tags + "\nlocation" +locString);

            initializeHttpRequest();
        }


        #region HTTP connection and send request
        
        void initializeHttpRequest()
        {
           // http://107.20.103.38/LBTweets/WebServices.asmx
           // /Tweet?tweet=sample_tweet&latitude=1&longitude=2&tags=shopping,sports,etc,
            String tweetURI = String.Format(Util.TWEET + Util.PARA_TWEET + "{0}" +
                Util.PARA_LATTITUDE + "{1}" + Util.PARA_LONGITUDE + "{2}" +
                Util.PARA_TAGS + "{3}", tweet, lattitude, longitude, tags);
            webclient = new WebClient();
            Debug.WriteLine("url " + tweetURI);
            webclient.DownloadStringAsync(new Uri(tweetURI));
            webclient.DownloadStringCompleted += sendHttpGetRequest;
        }


        void sendHttpGetRequest(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Debug.WriteLine("Using WebClient: " + e.Result);
                // testMain();
                MessageBox.Show("Your tweet sent sucessfully !");
            }
            else
            {
                Debug.WriteLine("Using WebClient: " + e.Error.Message);
            }
        }

        #endregion

        #region location and status Update
        private void getLocationUpdate()
        {
            geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.PositionChanged += onGeoWatcherPositionChanged;
            geoWatcher.StatusChanged += onGeoWatcherStatusChanged;
            geoWatcher.Start();
        }


        void onGeoWatcherPositionChanged(object sender,
            GeoPositionChangedEventArgs<GeoCoordinate> args)
        {
            // Turn off GeoWatcher
            geoWatcher.PositionChanged -= onGeoWatcherPositionChanged;
            geoWatcher.Stop();

            GeoCoordinate latlong = args.Position.Location;
            locString = String.Format("{0:F2}' {1} {2:F2}' {3}",
                Math.Abs(latlong.Latitude),
                latlong.Latitude > 0 ? 'N' : 'S',
                Math.Abs(latlong.Longitude),
                latlong.Longitude > 0 ? 'E' : 'W');
            lattitude = String.Format("{0:F2}", Math.Abs(latlong.Latitude));
            longitude = String.Format("{0:F2}", Math.Abs(latlong.Longitude));
            Debug.WriteLine("the location \n" + locString +"lattitude :\n"+lattitude+
                "longi \n"+longitude);

        }

        void onGeoWatcherStatusChanged(object sender,
            GeoPositionStatusChangedEventArgs args)
        {
            //Debug.WriteLine("hi ");
            switch (args.Status)
            {
                case GeoPositionStatus.Initializing:
                    Debug.WriteLine("Initalizing connection");
                    break;
                case GeoPositionStatus.Ready:
                    Debug.WriteLine("Now ready");
                    break;
                case GeoPositionStatus.Disabled:
                    Debug.WriteLine("disabled");
                    break;
                case GeoPositionStatus.NoData:
                    Debug.WriteLine("No data");
                    break;

            }
        }

        #endregion
    }
}