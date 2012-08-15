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
     * This class is used to store common string used in application.
     * **/
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {

        }
        private static MyLocation.StringLibrary resource1 = new MyLocation.StringLibrary();

        public MyLocation.StringLibrary Resource1 { get { return resource1; } }
    }
}
