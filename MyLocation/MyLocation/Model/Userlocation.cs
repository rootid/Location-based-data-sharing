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
     * This class is used to store and get user location.
     * **/
    public class Userlocation
    {
        private String _lati;
        private String _longi;
        private String _description;

        public String lati { 
            get { return _lati;} 
            set { _lati = value ;}
        }

        public String longi
        {
            get { return _longi; }
            set { _longi = value; }
        }

        public String description { 
            get { return _description; } 
            set { _description = description;} 
        }
    }
}
