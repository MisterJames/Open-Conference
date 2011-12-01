using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Prdc.Conference.Model
{
    public class ConferenceCacheManager : ICacheManager
    {

        public void UpdateCache() {  }


        public bool HasCacheData
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime? LastUpdate
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler OnNetworkIckiness;

        public event EventHandler<CacheUpdateEventArgs> OnRefreshCompleted;

        public event EventHandler<ProgressEventArgs> SessionProgressReported;

        public event EventHandler<ProgressEventArgs> SpeakersProgressReported;
    }
}
