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
using System.Device.Location;

namespace MyLocation
{

    /**
     * This class is used to store and get location
     * **/

    public class LocationLog
    {
        LocationLog() { 
        }

        LocationLog(String aLat,String along) {
            lat = aLat;
            longi = along;
        }

        public String lat {
            get { return this.lat; }
            set { this.lat = value; }
        }

        public String longi { 
            get { return this.longi ;} 
            set { this.longi = value;} 
        }

    }

}
