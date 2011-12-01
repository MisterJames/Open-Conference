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
using Prdc.Conference.Model;
using Ninject;

namespace Prdc.Conference
{
    public partial class DataRefresh : PhoneApplicationPage
    {
        private static StandardKernel _kernel = null;

        public DataRefresh()
        {
            InitializeComponent();
            _kernel = new StandardKernel(new LegacyCacheModule());
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ICacheManager manager = _kernel.Get<ICacheManager>();
            manager.SessionProgressReported += new EventHandler<ProgressEventArgs>(manager_SessionProgressReported);
            manager.SpeakersProgressReported += new EventHandler<ProgressEventArgs>(manager_SpeakersProgressReported);
            manager.OnRefreshCompleted += new EventHandler<CacheUpdateEventArgs>(manager_OnRefreshCompleted);
            manager.OnNetworkIckiness += new EventHandler(manager_OnNetworkIckiness);
            manager.UpdateCache();
            base.OnNavigatedTo(e);
        }

        void manager_OnNetworkIckiness(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>{
                MessageBox.Show("Please check your network...we're having trouble getting at the interwebs!", "Lamesauce!", MessageBoxButton.OK);
                txtSpeaker.Text = "How embarassing! That is unawesome.";
                txtSession.Text = "You're probably going to have to hit the back key and try again. So sorry!  (Now I have to go give the hampsters lashings...)";
            }));
        }

        void manager_OnRefreshCompleted(object sender, CacheUpdateEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                txtSpeaker.Text = "All data synchronized.";
                txtSession.Text = string.Format("There are {0} speakers presenting {1} sessions. Press back to return to the conference start screen.", e.SpeakerCount, e.SessionCount);
            }));
        }

        void manager_SpeakersProgressReported(object sender, ProgressEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { txtSpeaker.Text = e.Message; }));
        }

        void manager_SessionProgressReported(object sender, ProgressEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { txtSession.Text = e.Message; }));
        }
    }
}