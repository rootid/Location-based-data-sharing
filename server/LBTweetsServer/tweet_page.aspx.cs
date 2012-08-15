using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace LBTweetsServer
{
    public partial class tweet_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tweet_button_Click(object sender, EventArgs e)
        {
            string tweet = tweet_textbox.Text;
            string interests = "";
            string latitude = latitude_textbox.Text;
            string longitude = longitude_textbox.Text;

            bool entertainment = entertainment_checkbox.Checked;
            bool sports = sporsts_checkbox.Checked;
            bool shopping = shopping_checkbox.Checked;
            bool campus = campus_checkbox.Checked;

            if (entertainment == true)
            {
                interests = interests + "entertainment,";
            }
            if (sports == true)
            {
                interests = interests + "sports,";
            }
            if (shopping == true)
            {
                interests = interests + "shopping,";
            }
            if (campus == true)
            {
                interests = interests + "campus,";
            }

            //string url = "http://localhost:51246/WebServices.asmx/Tweet?tweet="
            //    + tweet + "&latitiude=" + latitude + "&longitude=" + longitude + "&tags=" + interests;
            string url = "http://23.22.46.152/LBTweets/WebServices.asmx/Tweet?tweet="
                + tweet + "&latitude=" + latitude + "&longitude=" + longitude + "&tags=" + interests;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}