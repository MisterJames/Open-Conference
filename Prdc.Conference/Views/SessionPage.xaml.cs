using Microsoft.Phone.Controls;
using Prdc.Conference.ViewModel;
using System.Collections.Generic;
using Prdc.Conference.Model;
using System.IO.IsolatedStorage;
using System;
using Microsoft.Phone.Tasks;
using System.Windows.Controls;

namespace Prdc.Conference
{
    /// <summary>
    /// Description for SessionPage.
    /// </summary>
    public partial class SessionPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the SessionPage class.
        /// </summary>
        public SessionPage()
        {
            InitializeComponent();
        
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            SessionViewModel vm = this.DataContext as SessionViewModel;

            List<int> savedSessions = new List<int>();
            
            // load existing ones if they're there
            if (IsolatedStorageSettings.ApplicationSettings.Contains("FavouriteSessions"))
                savedSessions = (List<int>)IsolatedStorageSettings.ApplicationSettings["FavouriteSessions"];

            // add the new one and save it
            if (!savedSessions.Contains(vm.Session.SessionId))
            {
                savedSessions.Add(vm.Session.SessionId);
                IsolatedStorageSettings.ApplicationSettings["FavouriteSessions"] = savedSessions;
            }

            // go show the goods
            string parameter = "savedPivotItem";
            NavigationService.Navigate(new Uri(string.Format("/Views/SessionsPage.xaml?parameter={0}", parameter), UriKind.Relative));
        }

        private void shareButton_Click(object sender, EventArgs e)
        {
            SessionViewModel vm = this.DataContext as SessionViewModel;

            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "Check out this session at PrDC";
            email.Body = string.Format("Hey, thought you might like to check out '{0}'. Visit http://www.prairiedevcon.com/sessions to see all the info.", vm.Session.Title);
            email.Show();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SessionSpeakers ss = e.AddedItems[0] as SessionSpeakers;

                if (ss != null)
                {
                    Speaker speaker = ss.Speaker;
                    SessionViewModel vm = this.DataContext as SessionViewModel;
                    vm.SendSpeakerNavigatingMessage(speaker);
                    ListBox listbox = sender as ListBox;
                    listbox.SelectedIndex = -1;
                }

                NavigationService.Navigate(new System.Uri("/Views/SpeakerPage.xaml", System.UriKind.Relative));
            }
        }


    }
}