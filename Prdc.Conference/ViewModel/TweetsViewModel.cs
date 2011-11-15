using GalaSoft.MvvmLight;
using Prdc.Conference.Model;

namespace Prdc.Conference.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class TweetsViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the TweetsViewModel class.
        /// </summary>
        public TweetsViewModel()
        {


            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
        }

        public void RefreshTweets()
        {
            TwitterSearchMap resultMap = new TwitterSearchMap();



            TweetList = resultMap;
        }

        public string ApplicationTitle { get { return "PRAIRIE DEV CON"; } }

        /// <summary>
        /// The <see cref="TweetList" /> property's name.
        /// </summary>
        public const string TweetListPropertyName = "TweetList";

        private TwitterSearchMap _tweetList;

        /// <summary>
        /// Gets the TweetList property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public TwitterSearchMap TweetList
        {
            get
            {
                return _tweetList;
            }

            set
            {
                if (_tweetList == value)
                {
                    return;
                }

                var oldValue = _tweetList;
                _tweetList = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TweetListPropertyName);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}