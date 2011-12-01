using System;
namespace Prdc.Conference.Model
{
    interface ICacheManager
    {
        bool HasCacheData { get; }
        DateTime? LastUpdate { get; }
        event EventHandler OnNetworkIckiness;
        event EventHandler<CacheUpdateEventArgs> OnRefreshCompleted;
        event EventHandler<ProgressEventArgs> SessionProgressReported;
        event EventHandler<ProgressEventArgs> SpeakersProgressReported;
        void UpdateCache();
    }
}
