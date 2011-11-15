using GalaSoft.MvvmLight;
using Prdc.Conference.Model;
using GalaSoft.MvvmLight.Messaging;

namespace Prdc.Conference.ViewModel
{
    public class SpeakerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SpeakerViewModel class.
        /// </summary>
        public SpeakerViewModel()
        {
            Messenger.Default.Register<SpeakerSelectionChangedMessage>(this, delegate(SpeakerSelectionChangedMessage message)
            {
                this.Speaker = message.Speaker;
            });

            if (IsInDesignMode)
            {
                this.Speaker = new Speaker
                {
                    FirstName = "James",
                    LastName = "Chambers",
                    Bio = "There was a lot of text about this guy one time. I honestly didn't understand how many different kinds of things that needed to be said about him, but apparently, these all needed to be said and to be in his bio. Which I though was weird. Did he think anyone would actually read it?  No, he simply wanted something to bind to in Blend. ",
                    Blog = "http://www.jameschambers.com/blog",
                    Email = "james@jameschambers.com",
                    Location = "Manitoba, Canada",
                    Twitter = "@CanadianJames",
                    Picture = new System.Uri(@"images\speakers.png", System.UriKind.Relative).ToString()
                };

            }
            //else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
        }

        public string ApplicationTitle { get { return "PRAIRIE DEV CON"; } }
        public string PageName { get { return "speaker profile"; } }


        public const string SpeakerPropertyName = "Speaker";
        private Speaker _speaker = null;

        public Speaker Speaker
        {
            get { return _speaker; }
            set
            {
                if (_speaker == value)
                {
                    return;
                }

                var oldValue = _speaker;
                _speaker = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SpeakerPropertyName);
            }
        }



        internal void SendSessionNavigatingMessage(Session session)
        {
            Messenger.Default.Send<SessionSelectionChangedMessage>(new SessionSelectionChangedMessage { Session = session });
        }
    }
}