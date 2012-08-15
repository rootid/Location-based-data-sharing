using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace LBTweetsServer
{
    public partial class new_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_button_Click(object sender, EventArgs e)
        {
            string phone_number = phone_number_textbox.Text;
            string password = password_textbox.Text;

            bool entertainment = entertainment_checkbox.Checked;
            bool sports = sporsts_checkbox.Checked;
            bool shopping = shopping_checkbox.Checked;
            bool campus = campus_checkbox.Checked;

            string interests = "";
            if(entertainment == true)
            {
                interests = interests + "entertainment,";
            }
            if(sports == true)
            {
                interests = interests + "sports,";
            }
            if(shopping == true)
            {
                interests = interests + "shopping,";
            }
            if(campus == true)
            {
                interests = interests + "campus,";
            }

            //string url = "http://localhost:51246/WebServices.asmx/RegisterUser?phone_number="
            //    + phone_number + "&password=" + password + "&interests=" + interests;
            string url = "http://23.22.46.152/LBTweets/WebServices.asmx/RegisterUser?phone_number="
                + phone_number + "&password=" + password + "&interests=" + interests;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}