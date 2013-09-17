using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App2.Common;
using SQLite;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace App2
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage1 : App2.Common.LayoutAwarePage
    {
        public BasicPage1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        /// 
        SQLite.SQLiteAsyncConnection conn=new SQLiteAsyncConnection("SampleDB");
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            DefaultViewModel["IsBusy"] = true;
           // DefaultViewModel["Employees1"] =conn.Table<SampleItem>().ToListAsync();
           // EmployeesCombo.ItemsSource = DefaultViewModel["Employees1"];
            DefaultViewModel["IsBusy"] = false;
            //jkajfha
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // this.Frame.Navigate(typeof (BasicPage2));
            this.Frame.Navigate(typeof (BasicPage2));
        }

        private void btnTileUpdate_Click(object sender, RoutedEventArgs e)
        {
            Showtoast("First Tosat Notification By Ramesh");
            UpdateTile("Ramesh");
            UpdateBadge("9");
        }
        private static void UpdateBadge(string text)
        {
            XmlDocument badgexmlDoc = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            XmlElement badgeElement = (XmlElement)badgexmlDoc.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", text);
            //XmlNodeList badgexmlnodes = badgexmlDoc.GetElementsByTagName("badge");
            //badgexmlnodes[0].InnerText = "6";

            BadgeNotification bn = new BadgeNotification(badgexmlDoc);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(bn);
        }

        private static void UpdateTile(string text)
        {
            XmlDocument tilexmlDoc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01);
            XmlNodeList tilexmlnodes = tilexmlDoc.GetElementsByTagName("text");
            tilexmlnodes[0].InnerText = text;
            XmlNodeList tileImageAttributes = tilexmlDoc.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appx:///images/ram.jpg");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "image not found");


            TileNotification tin = new TileNotification(tilexmlDoc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tin);
        }

        private static void Showtoast(string text)
        {
            XmlDocument xmlDoc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            XmlNodeList xmlnodes = xmlDoc.GetElementsByTagName("text");
            xmlnodes[0].InnerText = text;
            ToastNotification tn = new ToastNotification(xmlDoc);
            ToastNotificationManager.CreateToastNotifier().Show(tn);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageDialog md = new MessageDialog("This is a MessageDialog", "Title");
            md.Commands.Add(new UICommand("Hello"));
            md.ShowAsync();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            conn.CreateTableAsync<SampleItem>();

            conn.InsertAsync(new SampleItem() { Name = "Ramesh",Designation = "Software Engineer"});

            conn.InsertAsync(new SampleItem() { Name = "Rajesh", Designation = "Software Engineer" });

            conn.InsertAsync(new SampleItem() { Name = "Somesh", Designation = "Software Engineer" });

            txtDbPath.Text = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

        }
    }
}
