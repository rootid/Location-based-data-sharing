using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace LBTweetsServer
{
    public partial class request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: call web service to get tweets from server
        }

        protected void get_tweets_button_Click(object sender, EventArgs e)
        {
            string latitude = latitude_textbox.Text;
            string longitude = longitude_textbox.Text;

            string phone_number = RadioButtonList1.Text;

            //string url = "http://localhost:51246/WebServices.asmx/GetTweet?phone_number="
            //    + phone_number + "&latitiude=" + latitude + "&longitude=" + longitude;
            string url = "http://23.22.46.152/LBTweets/WebServices.asmx/GetTweet?phone_number="
                + phone_number + "&latitude=" + latitude + "&longitude=" + longitude;
            Response.Redirect(url);
        }
    }
}