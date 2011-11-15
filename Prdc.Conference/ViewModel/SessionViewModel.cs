using GalaSoft.MvvmLight;
using Prdc.Conference.Model;
using GalaSoft.MvvmLight.Messaging;

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
    public class SessionViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SessionViewModel class.
        /// </summary>
        public SessionViewModel()
        {
            Messenger.Default.Register<SessionSelectionChangedMessage>(this, delegate(SessionSelectionChangedMessage message) {
                this.Session = message.Session;
            });

            if (IsInDesignMode)
            {
                Session = new Session
                {
                    Title = "Some Really Big Long Title",
                    TagList = "FOO, ASP.NET, MICROSOFT",
                    Room = "Pootzy",
                    Abstract = @"<p>Some <b>great</b> stuff in this one!</p><p>Read all about the great stuff that is great and plus it's not bad too (so, it's really awesome!)</p>"
                    //SessionSpeakers = new System.Data.Linq.EntitySet<SessionSpeakers>() { new SessionSpeakers { Session = Session, Speaker = new Speaker { FirstName = "Jar Jar", LastName = "Binks" } } }
                };
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
            }
        }

        public string ApplicationTitle { get { return "PRAIRIE DEV CON"; } }
        public string PageName { get { return Session.Title; } }

        /// <summary>
        /// The <see cref="Session" /> property's name.
        /// </summary>
        public const string SessionPropertyName = "Session";

        private Session _session;

        /// <summary>
        /// Gets the Session property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Session Session
        {
            get
            {
                return _session;
            }

            set
            {
                if (_session == value)
                {
                    return;
                }

                var oldValue = _session;
                _session = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SessionPropertyName);

            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}