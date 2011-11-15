using Microsoft.Phone.Controls;
using Prdc.Conference.ViewModel;
using Prdc.Conference.Model;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Shell;

namespace Prdc.Conference
{
    /// <summary>
    /// Description for SessionPage.
    /// </summary>
    public partial class SessionsPage : PhoneApplicationPage
    {
        private int _currentSessionId = 0;

        /// <summary>
        /// Initializes a new instance of the SessionPage class.
        /// </summary>
        public SessionsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.NavigationContext.QueryString.ContainsKey("parameter"))
            {
                string newparameter = this.NavigationContext.QueryString["parameter"];
                if (newparameter.Equals("savedPivotItem"))
                {
                    sessionPivot.SelectedItem = savedPivotItem;
                    SessionsViewModel vm = this.DataContext as SessionsViewModel;
                    vm.RefreshSavedSessions(null);
                }
            }

        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SessionsViewModel vm = this.DataContext as SessionsViewModel;
                Session session = e.AddedItems[0] as Session;
                if (session != null)
                {
                    _currentSessionId = session.SessionId;
                    vm.SendSessionNavigatingMessage(session);
                }
                ListBox listbox = sender as ListBox;
                listbox.SelectedIndex = -1;
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
            }
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SessionsViewModel vm = this.DataContext as SessionsViewModel;
                SessionTags st = e.AddedItems[0] as SessionTags;
                Session session = st.Session;
                if (session != null)
                {
                    _currentSessionId = session.SessionId;
                    vm.SendSessionNavigatingMessage(session);
                }
                ListBox listbox = sender as ListBox;
                listbox.SelectedIndex = -1;
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
            }
        }

        private void btnHome_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            var result = MessageBox.Show("Click okay and I'll totally nuke all your saved sessions. Is that what you really want?", "Srsly?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                SessionsViewModel vm = this.DataContext as SessionsViewModel;
                vm.ClearSavedSessions();
            }
        }

        private void sessionPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBarIconButton button =  ApplicationBar.Buttons[1] as ApplicationBarIconButton;

            if (e.AddedItems[0] == savedPivotItem)
                button.IsEnabled = true;
            else
                button.IsEnabled = false;

        }


    }
}