using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Prdc.Conference.ViewModel;
using System.Windows;

namespace Prdc.Conference
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/SettingsPage.xaml", System.UriKind.Relative));
        }

        private void btnFeedback_Click(object sender, System.EventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.To = "support@firmicollective.com";
            email.Subject = "PrDC WP7 Feedback";
            email.Body = "Hey! This WP7 app (is great|needs work|would be better if written by my dog)!";
            email.Show();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            MainViewModel vm = this.DataContext as MainViewModel;
            if (!vm.HasCache)
            {
                var result = MessageBox.Show("There is no data in the application. Can we go fetch the conference information?", "Whoa!", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new System.Uri("/DataRefreshPage.xaml", System.UriKind.Relative));
                }
            }

            base.OnNavigatedTo(e);
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Views/DataRefreshPage.xaml", System.UriKind.Relative));

        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Views/VenuePage.xaml", System.UriKind.Relative));
            
        }

        private void HubTile_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Views/SessionsPage.xaml", System.UriKind.RelativeOrAbsolute));

        }

        private void HubTile_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Views/SpeakersPage.xaml", System.UriKind.Relative));
        }

        private void HubTile_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new System.Uri("http://twitter.com/#!/search/prdc11");
            task.Show();
        }
    }
}
