using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using Prdc.Conference.Model;
using System.Linq;
using System.Data.Linq;
using System.Diagnostics;
using System.IO.IsolatedStorage;

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
    public class SessionsViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SessionsViewModel class.
        /// </summary>
        public SessionsViewModel()
        {
            _sessions = new List<Session>();
            _tags = new List<Tag>();                        

            Messenger.Default.Register<CacheUpdatedMessage>(this, delegate(CacheUpdatedMessage update)
            {
                RefreshData();
            });
            
            if (IsInDesignMode)
            {
                //SetupData();
            }
            else
            {
                RefreshData();
            }
        }


        public void RefreshData()
        {
            Sessions = null;
            ConferenceDataContext dc = new ConferenceDataContext("isostore:/conference.sdf");
            
            if (dc.DatabaseExists())
            {
                Sessions = dc.Sessions.OrderBy(r => r.Title).ToList();
                Tags =  dc.Tags.ToList().OrderBy(t => t.Title).ToList();
            }

            RefreshSavedSessions(dc);
        }

        public void RefreshSavedSessions(ConferenceDataContext dc)
        {
            if (dc == null)
                dc = new ConferenceDataContext("isostore:/conference.sdf");            

            // get the saved sessions for the phone
            List<int> savedSessions = new List<int>();
            if (IsolatedStorageSettings.ApplicationSettings.Contains("FavouriteSessions"))
                savedSessions = (List<int>)IsolatedStorageSettings.ApplicationSettings["FavouriteSessions"];

            if (dc.DatabaseExists())
                SavedSessions = dc.Sessions.Where(s => savedSessions.Contains(s.SessionId)).ToList();
        }


        //private void SetupData()
        //{
        //    List<Speaker> speakers = new List<Speaker>();
        //    speakers.Add(new Speaker { FirstName = "James", LastName = "Chambers" });
        //    speakers.Add(new Speaker { FirstName = "Adrian", LastName = "Miles" });
        //    speakers.Add(new Speaker { FirstName = "Cory", LastName = "Fowler" });

        //    Session temp;

        //    temp = new Session { Title = "Asp.Net MVC", Tags = "Microsoft, ASP.NET", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[0]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Windows Azure", Tags = "Microsoft, Azure", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Makin' it Agile", Tags = "Agile, Ruby", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    temp.Speakers.Add(speakers[2]);
        //    _sessions.Add(temp);
        //    temp = new Session { Title = "Fun with Ruby", Tags = "Microsoft, ASP.NET", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[2]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "That's how we roll in Azure", Tags = "Microsoft, Azure", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Deep Dive Dojo", Tags = "Agile, Ruby", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    temp.Speakers.Add(speakers[0]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Sharepoint for Management Types", Tags = "Microsoft, ASP.NET", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[2]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Rock out your WinForms by Moving to Silverlight", Tags = "Microsoft, Azure", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    _sessions.Add(temp);

        //    temp = new Session { Title = "Just say no to Java", Tags = "Agile, Ruby", Speakers = new System.Data.Linq.EntitySet<Speaker>() };
        //    temp.Speakers.Add(speakers[1]);
        //    temp.Speakers.Add(speakers[0]);
        //    _sessions.Add(temp);
        //}

        /// <summary>
        /// The <see cref="SavedSessions" /> property's name.
        /// </summary>
        public const string SavedSessionsPropertyName = "SavedSessions";

        private List<Session> _savedSessions ;

        /// <summary>
        /// Gets the SavedSessions property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<Session> SavedSessions
        {
            get
            {
                return _savedSessions;
            }

            set
            {
                if (_savedSessions == value)
                {
                    return;
                }

                var oldValue = _savedSessions;
                _savedSessions = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SavedSessionsPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Sessions" /> property's name.
        /// </summary>
        public const string SpeakersPropertyName = "Sessions";

        private List<Session> _sessions;

        public List<Session> Sessions
        {
            get
            {
                return _sessions;
            }

            set
            {
                if (_sessions == value)
                {
                    return;
                }

                var oldValue = _sessions;
                _sessions = value;
                Debug.WriteLine("Changed sessions.");
                // Update bindings, no broadcast
                RaisePropertyChanged(SpeakersPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Tags" /> property's name.
        /// </summary>
        public const string TagsPropertyName = "Tags";

        private List<Tag> _tags;

        /// <summary>
        /// Gets the Tags property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<Tag> Tags
        {
            get
            {
                return _tags;
            }

            set
            {
                if (_tags == value)
                {
                    return;
                }

                var oldValue = _tags;
                _tags = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TagsPropertyName);
            }
        }

        public string ApplicationTitle
        {
            get { return "PRAIRIE DEV CON"; }
        }

        public string PageName
        {
            get { return "sessions"; }
        }


        internal void SendSessionNavigatingMessage(Session session)
        {
            Messenger.Default.Send<SessionSelectionChangedMessage>(new SessionSelectionChangedMessage { Session = session });
        }

        internal void ClearSavedSessions()
        {
            List<int> savedSessions = new List<int>();
            IsolatedStorageSettings.ApplicationSettings.Remove("FavouriteSessions");
            RefreshSavedSessions(null);
        }
    }
}