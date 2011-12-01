using GalaSoft.MvvmLight;
using Prdc.Conference.Model;
using GalaSoft.MvvmLight.Messaging;
using System;

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
                this.Speaker.SessionSpeakers = new System.Data.Linq.EntitySet<SessionSpeakers>();
                this.Speaker.SessionSpeakers.Add(new SessionSpeakers
                {
                    Speaker = this.Speaker,
                    Session = new Session
                    {
                        Abstract = "In this session you will find that there are things that you can do.",
                        Title = "Session of Awesome",
                        Start = new DateTime(2012,2,1,10,0,0),
                        Room = "Wooty Froo Froo Ballroom"
                    }
                });

                this.Speaker.SessionSpeakers.Add(new SessionSpeakers
                {
                    Speaker = this.Speaker,
                    Session = new Session
                    {
                        Abstract = "Things that make you go, oh-yeah.",
                        Title = "The Mega Oh-Yeah Session",
                        Start = new DateTime(2012, 2, 2, 14, 0, 0),
                        Room = "Salle de Classe A"
                    }
                });
                this.Speaker.SessionSpeakers.Add(new SessionSpeakers
                {
                    Speaker = this.Speaker,
                    Session = new Session
                    {
                        Abstract = "Read all about it.",
                        Title = "Chronicles of Foo",
                        Room = "Salle de Classe A"
                    }
                });

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

        internal void SendSpeakerNavigatingMessage(Speaker speaker)
        {
            throw new System.NotImplementedException();
        }
    }
}