using Microsoft.Phone.Controls;
using Prdc.Conference.ViewModel;
using System.Collections.Generic;
using Prdc.Conference.Model;
using System.IO.IsolatedStorage;
using System;
using Microsoft.Phone.Tasks;

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
            NavigationService.Navigate(new Uri(string.Format("/SessionsPage.xaml?parameter={0}", parameter), UriKind.Relative));
        }

        private void shareButton_Click(object sender, EventArgs e)
        {
            SessionViewModel vm = this.DataContext as SessionViewModel;

            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "Check out this session at PrDC";
            email.Body = string.Format("Hey, thought you might like to check out '{0}'. Visit http://www.prairiedevcon.com/sessions to see all the info.", vm.Session.Title);
            email.Show();
        }


    }
}