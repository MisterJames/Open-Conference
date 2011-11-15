using Microsoft.Phone.Controls;
using Prdc.Conference.ViewModel;
using Prdc.Conference.Model;
using System.Windows.Controls;

namespace Prdc.Conference
{
    /// <summary>
    /// Description for SpeakerPage.
    /// </summary>
    public partial class SpeakerPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the SpeakerPage class.
        /// </summary>
        public SpeakerPage()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SpeakerViewModel vm = this.DataContext as SpeakerViewModel;
            if (e.AddedItems.Count > 0)
            {
                SessionSpeakers session = e.AddedItems[0] as SessionSpeakers;
                if (session != null)
                {
                    vm.SendSessionNavigatingMessage(session.Session);
                    ListBox listbox = sender as ListBox;
                    listbox.SelectedIndex = -1;
                    NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
                }
            }
        }
    }
}