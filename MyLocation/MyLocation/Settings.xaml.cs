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
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace MyLocation
{
    /**
     * This class provides setting page to user
     * **/
    public partial class Settings : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        delegate Boolean customCheckbox(String input);
        delegate Boolean customOptionBox(String input);
        public Settings()
        {
            InitializeComponent();
            populateList();
            populateRadiusList();
        }

        private void populateRadiusList()
        {
            customOptionBox isSelectedButton = optionBoxString => {
                String localValue;
                settings.TryGetValue<String>(Util.RADIUS, out localValue);
                if (localValue != null && localValue.Equals(optionBoxString))
                    return true;
                else
                    return false;
            };
            List<Radius> radiiList = new List<Radius>();
            radiiList.Add(new Radius()
            {
                radiName = Util.UPTO_10M,
                radiIsChecked = isSelectedButton(Util.UPTO_10M)
            });
            radiiList.Add(new Radius()
            {
                radiName = Util.UPTO_100M,
                radiIsChecked = isSelectedButton(Util.UPTO_100M)
            });
            radiiList.Add(new Radius()
            {
                radiName = Util.UPTO_1000M,
                radiIsChecked = isSelectedButton(Util.UPTO_1000M)
            });
            radiiList.Add(new Radius()
            {
                radiName = Util.UPTO_10000M,
                radiIsChecked = isSelectedButton(Util.UPTO_10000M)
            });
            this.listBox1.ItemsSource = radiiList;

        }
        private void optionBoxChecked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.listBox1.ItemContainerGenerator.ContainerFromItem((sender as RadioButton).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = true;
                Radius dataSelected = checkedItem.DataContext as Radius;
                removeSettings(Util.RADIUS);
                addSettings(Util.RADIUS, dataSelected.radiName);
                Debug.WriteLine("string " + dataSelected.radiName);
            }

        }


        private void populateList()
        {
            //use of delegate customCheckbox
            customCheckbox isBoxChecked = checkBoxString =>
            {
                String localValue;
                settings.TryGetValue<String>(checkBoxString, out localValue);
                if (localValue != null && localValue.Equals(Util.YES))
                    return true;
                else
                    return false;

            };

            List<Categories> dataSource = new List<Categories>();
            dataSource.Add(new Categories()
            {
                Name = Util.ENTERTAINMENT,
                Icon = "images/movie.png",
                IsChecked = isBoxChecked(Util.ENTERTAINMENT)
            });
            dataSource.Add(new Categories()
            {
                Name = Util.SPORTS,
                Icon = "images/sports.png",
                IsChecked = isBoxChecked(Util.SPORTS)
            });
            dataSource.Add(new Categories()
            {
                Name = Util.SHOPPING,
                Icon = "images/shop.png",
                IsChecked = isBoxChecked(Util.SHOPPING)
            });
            dataSource.Add(new Categories()
            {
                Name = Util.CAMPUS_UPDATE,
                Icon = "images/campus.png",
                IsChecked = isBoxChecked(Util.CAMPUS_UPDATE)
            });
            this.listBox.ItemsSource = dataSource;

        }



        /*
         * A delegate is an object which represents a method and,
         * optionally, the "this" object associated with that method
         * Events are a specialized delegate type primarily 
         * used for message or notification passing
         *  
         * Anonymous methods are expressions that can be converted to delegates. 
         * 
         */
        //TODO :Notify the observers 
        //
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.listBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = true;
                Categories dataSelected = checkedItem.DataContext as Categories;
                removeSettings(dataSelected.Name);
                addSettings(dataSelected.Name, Util.YES);
                Debug.WriteLine("string " + dataSelected.Name);
            }
        }

        //
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.listBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = false;
                String localValue;
                Categories dataSelected = checkedItem.DataContext as Categories;
                settings.TryGetValue<String>(dataSelected.Name, out localValue);
                Debug.WriteLine(" local value b4 remove :" + localValue);
                removeSettings(dataSelected.Name);
                addSettings(dataSelected.Name, Util.NO);
                //settings.TryGetValue<String>(Util.ENTERTAINMENT, out localValue);
                Debug.WriteLine(" local value after remove :" + localValue);
                Debug.WriteLine("un selected " + dataSelected.Name);
            }
        }

        private void removeSettings(String key)
        {
            settings.Remove(key);
            settings.Save();
        }

        private void addSettings(String key, String value)
        {
            settings.Add(key, value);
            settings.Save();
        }
    }




    public class Categories
    {
        public bool IsChecked
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Icon
        {
            get;
            set;
        }

    }


    public class Radius
    {
        public bool radiIsChecked
        {
            get;
            set;
        }

        public string radiName
        {
            get;
            set;
        }

    }
}