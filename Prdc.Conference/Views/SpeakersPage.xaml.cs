using Microsoft.Phone.Controls;
using Prdc.Conference.ViewModel;
using Prdc.Conference.Model;
using System.Windows.Controls;

namespace Prdc.Conference
{
    /// <summary>
    /// Description for SpeakersPage.
    /// </summary>
    public partial class SpeakersPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the SpeakersPage class.
        /// </summary>
        public SpeakersPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);
        }

        private void SessionSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SessionSpeakers sessionSpeakers = e.AddedItems[0] as SessionSpeakers;

                if (sessionSpeakers != null)
                {
                    SpeakersViewModel vm = this.DataContext as SpeakersViewModel;
                    Session session = sessionSpeakers.Session;
                    vm.SendSessionNavigatingMessage(session);
                    ListBox listbox = sender as ListBox;
                    listbox.SelectedIndex = -1;
                }

                NavigationService.Navigate(new System.Uri("/Views/SessionPage.xaml", System.UriKind.Relative));
            }

        }

        private void SpeakerListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Speaker speaker = e.AddedItems[0] as Speaker;

                if (speaker != null) 
                {
                    SpeakersViewModel vm = this.DataContext as SpeakersViewModel;
                    vm.SendSpeakerNavigatingMessage(speaker);
                    ListBox listbox = sender as ListBox;
                    listbox.SelectedIndex = -1;
                }

                NavigationService.Navigate(new System.Uri("/Views/SpeakerPage.xaml", System.UriKind.Relative));
            }
            
        }
    }
}