using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LBTweetsServer
{
    public class tweets
    {
        public string m_tweet_value;      // 
        public string m_tags;  // entertainment#shopping#sports#campus
    }

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebServices : System.Web.Services.WebService
    {
        [WebMethod]
        public string RegisterUser(   long phone_number, 
                                    string password,
                                    string interests)
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            string result = db_adapter.db_register_user(phone_number, password, interests);
            db_adapter.db_deinit();
            return result;
        }

        [WebMethod]
        public string AuthenticateUser( long phone_number,
                                        string password )
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            string result = db_adapter.db_authenticate_user(phone_number, password);
            db_adapter.db_deinit();
            return result;
        }

        [WebMethod]
        public string Subscribe(long phone_number,
                              string tags)
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            string result = db_adapter.db_subscribe(phone_number, tags);
            db_adapter.db_deinit();
            return result;
        }

        [WebMethod]
        public void Tweet(  string tweet,
                            double latitude,
                            double longitude,
                            string tags)
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            db_adapter.db_tweet(tweet, latitude, longitude, tags);
            db_adapter.db_deinit();
        }

        [WebMethod]
        public tweets[] GetTweet(long phone_number,
                                 double latitude,
                                 double longitude,
                                 int radius)
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            tweets[] t = db_adapter.db_get_tweet(phone_number, latitude, longitude, radius);
            db_adapter.db_deinit();
            return t;
        }

        [WebMethod]
        public tweets[] GetTweetsByTags(double latitude,
                                        double longitude,
                                        int radius, 
                                        string tags)
        {
            db_adapter.db_init("VikNeoAdmin", "Epsxe@193", "mzw1908bpk.database.windows.net", "LBTweets");
            tweets[] t = db_adapter.db_get_tweet_by_tag(latitude, longitude, radius, tags);
            db_adapter.db_deinit();
            return t;
        }
    }
}