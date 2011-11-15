
namespace Prdc.Conference.ViewModel
{

    public class ViewModelLocator
    {

        private static MainViewModel _main;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            CreateMain();
            CreateSessions();
            CreateSession();
            CreateSpeakers();
            CreateTweets();
            CreateSpeaker();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (_main == null)
                {
                    CreateMain();
                }

                return _main;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            _main.Cleanup();
            _main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
            ClearSessions();
            ClearSession();
            ClearSpeakers();
            ClearTweets();
            ClearSpeaker();
        }


        private static SessionsViewModel _sessions;

        /// <summary>
        /// Gets the Sessions property.
        /// </summary>
        public static SessionsViewModel SessionsStatic
        {
            get
            {
                if (_sessions == null)
                {
                    CreateSessions();
                }

                return _sessions;
            }
        }

        /// <summary>
        /// Gets the Sessions property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SessionsViewModel Sessions
        {
            get
            {
                return SessionsStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Sessions property.
        /// </summary>
        public static void ClearSessions()
        {
            _sessions.Cleanup();
            _sessions = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Sessions property.
        /// </summary>
        public static void CreateSessions()
        {
            if (_sessions == null)
            {
                _sessions = new SessionsViewModel();
            }
        }

        private static SessionViewModel _session;

        /// <summary>
        /// Gets the Session property.
        /// </summary>
        public static SessionViewModel SessionStatic
        {
            get
            {
                if (_session == null)
                {
                    CreateSession();
                }

                return _session;
            }
        }

        /// <summary>
        /// Gets the Session property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SessionViewModel Session
        {
            get
            {
                return SessionStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Session property.
        /// </summary>
        public static void ClearSession()
        {
            _session.Cleanup();
            _session = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Session property.
        /// </summary>
        public static void CreateSession()
        {
            if (_session == null)
            {
                _session = new SessionViewModel();
            }
        }


        private static SpeakersViewModel _speakers;

        /// <summary>
        /// Gets the Speakers property.
        /// </summary>
        public static SpeakersViewModel SpeakersStatic
        {
            get
            {
                if (_speakers == null)
                {
                    CreateSpeakers();
                }

                return _speakers;
            }
        }

        /// <summary>
        /// Gets the Speakers property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SpeakersViewModel Speakers
        {
            get
            {
                return SpeakersStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Speakers property.
        /// </summary>
        public static void ClearSpeakers()
        {
            _speakers.Cleanup();
            _speakers = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Speakers property.
        /// </summary>
        public static void CreateSpeakers()
        {
            if (_speakers == null)
            {
                _speakers = new SpeakersViewModel();
            }
        }

        // ============]============]============]============]============]
        private static TweetsViewModel _tweets;

        /// <summary>
        /// Gets the Tweets property.
        /// </summary>
        public static TweetsViewModel TweetsStatic
        {
            get
            {
                if (_tweets == null)
                {
                    CreateTweets();
                }

                return _tweets;
            }
        }

        /// <summary>
        /// Gets the Tweets property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public TweetsViewModel Tweets
        {
            get
            {
                return TweetsStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Tweets property.
        /// </summary>
        public static void ClearTweets()
        {
            _tweets.Cleanup();
            _tweets = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Tweets property.
        /// </summary>
        public static void CreateTweets()
        {
            if (_tweets == null)
            {
                _tweets = new TweetsViewModel();
            }
        }

        private static SpeakerViewModel _speaker;

        /// <summary>
        /// Gets the Speaker property.
        /// </summary>
        public static SpeakerViewModel SpeakerStatic
        {
            get
            {
                if (_speaker == null)
                {
                    CreateSpeaker();
                }

                return _speaker;
            }
        }

        /// <summary>
        /// Gets the Speaker property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SpeakerViewModel Speaker
        {
            get
            {
                return SpeakerStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Speaker property.
        /// </summary>
        public static void ClearSpeaker()
        {
            _speaker.Cleanup();
            _speaker = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Speaker property.
        /// </summary>
        public static void CreateSpeaker()
        {
            if (_speaker == null)
            {
                _speaker = new SpeakerViewModel();
            }
        }



    }
}