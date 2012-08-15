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

namespace MyLocation
{
    /**
     * This class is used to store and retreive tweets with respect to location
     * **/
    public class Tweet
    {
        private String _tweetMsg;
        private String _date;
        private Userlocation _userLocation;
        private String _tweetTitle;
        public enum TweetType
        {
            Sports,
            Entertainment,
            Shopping
        }


        public string tweetMsg {
            get { return _tweetMsg; }
            set{
                _tweetMsg = value; 
            }
        }

        public string tweetTitle
        {
            get { return _tweetTitle; }
            set
            {
                _tweetTitle = value;
            }
        }

        public string date
        {
            get { return _date; }
            set
            {
                _date = value;
            }
        }

        public Userlocation userLocation
        {
            get { return _userLocation; }
            set
            {
                _userLocation = value;
            }
        }



        
    }
}
