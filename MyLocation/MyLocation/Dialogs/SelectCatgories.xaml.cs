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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyLocation.Dialogs
{
    /***
     * This class is used to provide tweet tags option to user. 
     * **/
    public partial class SelectCatgories : ChildWindow
    {
        ObservableCollection<Categories> categoryCollect = new ObservableCollection<Categories>();
        public SelectCatgories()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData() {

            List<Categories> dataSource = new List<Categories>();
            dataSource.Add(new Categories() { Name = Util.ENTERTAINMENT, IsChecked = false });
            dataSource.Add(new Categories() { Name = Util.SPORTS,IsChecked = false });
            dataSource.Add(new Categories() { Name = Util.SHOPPING,IsChecked = false });
            dataSource.Add(new Categories() { Name = Util.CAMPUS_UPDATE,IsChecked = false });
            this.listBox.ItemsSource = dataSource;

        }
        void onOkButtonClicked(object sender, EventArgs e) {
            this.DialogResult = true;
            Debug.WriteLine("Ok button selected ");
            //If size is zero show error message

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.listBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = true;
                Categories dataSelected = checkedItem.DataContext as Categories;
                Util.tweetTagList.Add(dataSelected.Name);
                Debug.WriteLine("Checked :"+dataSelected.Name);
            }
        }

        //
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedItem = this.listBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;
            if (checkedItem != null)
            {
                checkedItem.IsSelected = false;
                Categories dataSelected = checkedItem.DataContext as Categories;
                Util.tweetTagList.Remove(dataSelected.Name);
                Debug.WriteLine("UnChecked :" + dataSelected.Name);
            }
        }

        void onCancelButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            Debug.WriteLine("cancel button selected ");
            //TODO : if cancel button remove all the elements from list
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
    }
}
