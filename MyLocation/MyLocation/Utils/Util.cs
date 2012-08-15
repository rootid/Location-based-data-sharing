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
using System.Collections;
using System.Collections.Generic;


namespace MyLocation
{
    /**
     * Helper class to provide common functionality
     * */
    public static class  Util
    {
        public static readonly String SPORTS = "Sports";
        public static readonly String ENTERTAINMENT = "Entertainment";
        public static readonly String SHOPPING = "Shopping";
        public static readonly String CAMPUS_UPDATE = "campus";
        public static readonly String YES = "Yes";
        public static readonly String NO = "No";
        public static readonly String SERVER_NAME = "http://23.22.46.152/LBTweets/WebServices.asmx";
        public static readonly String PARA_PHONE_NUMBER = "&phone_number=";
        public static readonly String PARA_LATTITUDE = "&latitude=";
        public static readonly String PARA_LONGITUDE = "&longitude=";
        public static readonly String PARA_TAGS = "&tags=";
        public static readonly String PARA_PASSWORD = "&password=";
        public static readonly String PARA_INTEREST = "&interests=";
        public static readonly String PARA_RADIUS = "&radius=";
        public static readonly String PARA_TWEET = "&tweet=";
        public static String GET_TWEET = Util.SERVER_NAME + "/GetTweet?";
        public static String TWEET_BY_TAG = Util.SERVER_NAME + "/GetTweetsByTags?";
        public static String TWEET = Util.SERVER_NAME +"/Tweet?";
        public static String REGISTER_USER = Util.SERVER_NAME + "/RegisterUser?";
        public static String SUBSCRIBE_USER = Util.SERVER_NAME + "/Subscribe?";
        public static String AUTHENTICATE_USER = Util.SERVER_NAME + "/AuthenticateUser?";
        public static String AUTHENTICATED_NO = "User authentication failed.";
        public static String AUTHENTICATED_YES = "User authentication successful.";
        public static String USER_REGISTRATION_YES = "User registration successful.";
        public static String UPTO_10M = "Upto 10 m";
        public static String UPTO_100M = "Upto 100 m";
        public static String UPTO_1000M = "Upto 1000 m";
        public static String UPTO_10000M = "Upto 10000 m";
        public static String RADIUS = "radius";
        public static String USER_REGISTRATION_NO = "";
        public static List<String> tweetTagList = new List<String>();
        
    }
}
