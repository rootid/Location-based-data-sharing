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
using Microsoft.Phone.Shell; //used for phoneApplication service
using System.Diagnostics;
using MyLocation.Model;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Device.Location;
using System.Threading;

namespace MyLocation
{
    /**
     * This class is used to show received messages 
     * **/
    public partial class MsgRecvPivot : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private ObservableCollection<ChoiceViewModel> ChoicesModel = new ObservableCollection<ChoiceViewModel>();
        private ObservableCollection<Tweet> tweetModel = new ObservableCollection<Tweet>();
        private List<Tweet> tweetList = new List<Tweet>();
        private WebClient webclient;
        protected static String lattitude;
        protected static String longitude;
        private String radius = "1000";
        private String tags;
        protected static GeoCoordinateWatcher geoWatcher;
        private string tweetUrl;
        private Worker workerObject;
        private Thread workerThread;
        public static bool _shouldStop = false;
        public MsgRecvPivot()
        {
            InitializeComponent();
            lstboxMsg.ItemsSource = ChoicesModel;
            lstboxTweets.ItemsSource = tweetModel;
            //get the local storage tweets
            getSavedSetting();
            loadTweetsFromLocal();
            initLocation();
        }

        private void initLocation()
        {
            workerObject = new Worker();
            workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");
        }

        private void stopLoctionUpdate()
        {
            if (workerThread != null)
            {
                workerObject.RequestStop();
                //  workerThread.Abort();
            }
        }
        private void loadTweetsFromLocal()
        {
            List<Tweet> localTweetList = IsolatedStorageCacheManager
                <List<Tweet>>.Retrieve("alltweets.xml");
            if (localTweetList != null)
            {
                foreach (var tweet in localTweetList)
                {
                    tweetModel.Add(new Tweet
                    {
                        tweetTitle = tweet.tweetTitle,
                        tweetMsg = tweet.tweetMsg
                    }
                    );
                }
            }
        }
        private void loadTweetsToLocal()
        {
            //for storing and retrieving a collection of objects
            //  List<Tweet> tweetList = new List<Tweet>();
            //   tweetList.Add(new Tweet { tweetMsg = "First tweetsd", tweetTitle = "Enter" });
            //     tweetList.Add(new Tweet { tweetMsg = "second tweetsd", tweetTitle = "Enter" });

            IsolatedStorageCacheManager<List<Tweet>>.Store("alltweets.xml", tweetList);
            tweetList.Clear();
        }

        #region store and retrieve list object
        public static class IsolatedStorageCacheManager<T>
        {
            public static void Store(string filename, T obj)
            {
                IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication();
                using (IsolatedStorageFileStream fileStream = appStore.OpenFile(filename, FileMode.Create))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(fileStream, obj);
                }
            }

            public static T Retrieve(string filename)
            {
                T obj = default(T);
                IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication();
                if (appStore.FileExists(filename))
                {
                    using (IsolatedStorageFileStream fileStream = appStore.OpenFile(filename, FileMode.Open))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                        obj = (T)serializer.ReadObject(fileStream);
                    }
                }
                return obj;
            }
        }
        #endregion

        private void loadTweets()
        {
            initializeWebClient();
        }

        void downloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
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

        void initializeWebClient()
        {
            //http://23.22.46.152/LBTweets/WebServices.asmx/GetTweetsByTags?
            //latitude=1&longitude=3&radius=10000&tags=shopping,entertainment
            webclient = new WebClient();
            tweetUrl = String.Format(Util.TWEET_BY_TAG + Util.PARA_LATTITUDE + "{0}" +
                Util.PARA_LONGITUDE + "{1}" + Util.PARA_RADIUS + "{2}" + Util.PARA_TAGS + "{3}",
                lattitude, longitude, radius, tags);
            Debug.WriteLine("Tweet url :" + tweetUrl);
            webclient.DownloadStringAsync(new Uri(tweetUrl));
            webclient.DownloadStringCompleted += downloadCompleted;
        }


        void parseResult(String aResult)
        {
            XDocument xmlDoc = XDocument.Parse(aResult);
            XNamespace nameSpace = "http://tempuri.org/";
            var allTweets = from tweets in xmlDoc.Descendants(nameSpace + "tweets")
                            select new
                            {
                                tweet = tweets.Element(nameSpace + "m_tweet_value").Value,
                                m_tags = tweets.Element(nameSpace + "m_tags").Value

                            };

            foreach (var varTweet in allTweets)
            {
                Tweet localTweet = new Tweet
                {
                    tweetTitle = varTweet.m_tags,
                    tweetMsg = varTweet.tweet
                };
                tweetModel.Add(localTweet);
                tweetList.Add(localTweet);
            }
            loadTweetsToLocal();
        }

        #region Get unique saved preferences
        private Boolean isDuplicate(String key)
        {

            Boolean isduplicate = false;
            for (int i = 0; i < ChoicesModel.Count; i++)
            {
                Debug.WriteLine(key);
                if (ChoicesModel[i].choice.Equals(key))
                {
                    isduplicate = true;
                  //  Debug.WriteLine(key);
                }
            }

            return isduplicate;
        }

        private void getRadiusFromSetting()
        {
            radius = "1000";
            if (IsolatedStorageSettings.ApplicationSettings.Keys.Count > 0)
            {
                foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Keys)
                {
                    if (o.Equals(Util.RADIUS))
                    {
                        String localValue;
                        settings.TryGetValue<String>((String)o, out localValue);
                        if (localValue.Equals(Util.UPTO_10M)) {
                            radius = "10";
                        }
                        else if (localValue.Equals(Util.UPTO_100M))
                        {
                            radius = "100";
                        }
                        else if (localValue.Equals(Util.UPTO_10000M))
                        {
                            radius = "10000";
                        }
                    }
                }
            }
        }

        private void getTagsFromSettings()
        {
            bool isOnlyTag = false;
            tags = "";
            if (IsolatedStorageSettings.ApplicationSettings.Keys.Count > 0)
            {
                foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Keys)
                {
                    Debug.WriteLine((String)o);
                    String localValue;
                    settings.TryGetValue<String>((String)o, out localValue);
                    if (localValue.Equals(Util.YES))
                    {
                        if (isOnlyTag)
                        {
                            tags += ",";
                        }
                        tags += (String)o;
                        isOnlyTag = true;
                        //Debug.WriteLine("settings-ISOLATED" + (String)o);

                    }
                }
            }
            else
            {
                tags += Util.CAMPUS_UPDATE;
            }
        }
        //get saved prefrences
        private void getSavedSetting()
        {
            if (settings.Keys.Count != 0)
            {
                foreach (Object o in IsolatedStorageSettings.ApplicationSettings.Keys)
                {
                    String localValue;
                    settings.TryGetValue<String>((String)o, out localValue);
                    if (localValue.Equals(Util.YES))
                    {
                        if (!isDuplicate((String)o))
                        {
                            ChoicesModel.Add(new ChoiceViewModel { choice = (String)o });
                          //  Debug.WriteLine("Added:" + (String)o);
                        }
                    }
                    else
                    {
                        if (isDuplicate((String)o))
                        {
                            for (int i = 0; i < ChoicesModel.Count; i++)
                            {
                                Debug.WriteLine((String)o);
                                if (ChoicesModel[i].choice.Equals((String)o))
                                {
                                    ChoicesModel.RemoveAt(i);
                                   // Debug.WriteLine("Removed :" + (String)o);
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion
        private void LoadFromLocalStorage()
        {
            ChoicesModel.Add(new ChoiceViewModel() { choice = Util.ENTERTAINMENT });
            lstboxMsg.ItemsSource = ChoicesModel;

        }


        #region save state for pivot selected index
        //Start worker thread here
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            var state = PhoneApplicationService.Current.State;
            Debug.WriteLine("In navigation from");
            if (state.ContainsKey("SelectedPivotIndex"))
            {
                state.Remove("SelectedPivotIndex");
            }

            state.Add("SelectedPivotIndex", customPivot.SelectedIndex);
            //Debug.WriteLine("Location stopped in navigation");
           // stopLoctionUpdate();
           // MsgRecvPivot._shouldStop = true;
            stopLoctionUpdate();
        }

        //After 
        //TODO : stop thread here
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var state = PhoneApplicationService.Current.State;

            if (state.ContainsKey("SelectedPivotIndex"))
            {
                customPivot.SelectedIndex = (int)state["SelectedPivotIndex"];
                state.Remove("SelectedPivotIndex");
            }

            base.OnNavigatedTo(e);
           // Debug.WriteLine("Navigation Location started");
            Debug.WriteLine("In navigation TO");
            getSavedSetting();
            clearAllTweets();
            loadTweetsFromLocal();
            MsgRecvPivot._shouldStop = false;
            initLocation();
        }

        #endregion



        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            Debug.WriteLine("Application activated");
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            Debug.WriteLine("Application launched");
        }

        #region Menu bar handlers
        private void onRefreshClickHandler(object sender, EventArgs e)
        {
            //fetch location
            // getLocationUpdate();
            clearAllTweets();
            //fetch tags
            getRadiusFromSetting();
            getTagsFromSettings();
            //send data and accept tweets
            initializeWebClient();
            //load tweets

            // loadTweetsToLocal();
        }

        

        private void clearAllTweets()
        {
            tweetModel.Clear();
        }
        private void onMsgReceiveClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Msg receive works!");
        }

        private void onMsgComposeClickHandler(object sender, EventArgs e)
        {
            //    MessageBox.Show("Msg compose works!");
            NavigationService.Navigate(new Uri("/TextInputView.xaml", UriKind.Relative));
        }

        private void onSettingClickHandler(object sender, EventArgs e)
        {
            // MessageBox.Show("setting button works!");
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void onTestMenuClickHnadler(object sender, EventArgs e)
        {
            MessageBox.Show("test Menu works!");
        }
        #endregion

        
        #region worker Thread to fetch location after specific interval
        public class Worker
        {
            // Volatile is used as hint to the compiler that this data
            // member will be accessed by multiple threads.
           // private bool _shouldStop;
           

            // This method will be called when the thread is started.
            public void DoWork()
            {
                while (!MsgRecvPivot._shouldStop)
                {
                    getLocationUpdate();
                    Debug.WriteLine("location thread: working...");
                    Thread.Sleep(60 * 1000); //update after 40 seconds
                    Debug.WriteLine("location thread: After sleeping...");
                }
               
                Debug.WriteLine("worker thread: terminating.");
                geoWatcher = null;
                
            }
            public void RequestStop()
            {
               MsgRecvPivot._shouldStop = true;
               geoWatcher = null;
            }
          

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
                if (geoWatcher != null)
                {
                    geoWatcher.PositionChanged -= onGeoWatcherPositionChanged;
                    geoWatcher.Stop();

                    GeoCoordinate latlong = args.Position.Location;
                    String locString = String.Format("{0:F2}' {1} {2:F2}' {3}",
                        Math.Abs(latlong.Latitude),
                        latlong.Latitude > 0 ? 'N' : 'S',
                        Math.Abs(latlong.Longitude),
                        latlong.Longitude > 0 ? 'E' : 'W');
                    lattitude = String.Format("{0:F2}", Math.Abs(latlong.Latitude));
                    longitude = String.Format("{0:F2}", Math.Abs(latlong.Longitude));
                    Debug.WriteLine("the location " + locString);
                }
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
        #endregion
    }
}