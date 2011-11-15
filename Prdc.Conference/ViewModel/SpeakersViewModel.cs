using GalaSoft.MvvmLight;
using Prdc.Conference.Model;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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
    public class SpeakersViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SpeakersViewModel class.
        /// </summary>
        public SpeakersViewModel()
        {
            Messenger.Default.Register<CacheUpdatedMessage>(this, delegate(CacheUpdatedMessage update)
            {
                RefreshData();
            });

            
            if (IsInDesignMode)
            {
                List<Speaker> foo = new List<Speaker>();
                //foo.Add(new Speaker { FirstName = "James", LastName = "Chambers", SpeakerId = 49, Sessions = new System.Data.Linq.EntitySet<Session>()});
                //foo.Add(new Speaker { FirstName = "David", LastName = "Wesst", SpeakerId = 37, Sessions = new System.Data.Linq.EntitySet<Session>() });
                //foo.Add(new Speaker { FirstName = "James", LastName = "Chambers", SpeakerId = 21, Sessions = new System.Data.Linq.EntitySet<Session>() });
                Speakers = foo;
            }
            else
            {
                RefreshData();
                // Code runs "for real": Connect to service, etc...
            }
        }

        public void RefreshData()
        {
            Speakers = null;
            ConferenceDataContext dc = new ConferenceDataContext("isostore:/conference.sdf");
            if (dc.DatabaseExists())
                Speakers = dc.Speakers.OrderBy(s => s.FirstName).ToList();
        }

        public string ApplicationTitle { get { return "PRAIRIE DEV CON"; } }
        public string PageName { get { return "speakers"; } }

        /// <summary>
        /// The <see cref="IsLoadingData" /> property's name.
        /// </summary>
        public const string IsLoadingDataPropertyName = "IsLoadingData";

        private bool _loading = false;

        /// <summary>
        /// Gets the IsLoadingData property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsLoadingData
        {
            get
            {
                return _loading;
            }

            set
            {
                if (_loading == value)
                {
                    return;
                }

                var oldValue = _loading;
                _loading = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsLoadingDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Speakers" /> property's name.
        /// </summary>
        public const string SpeakersPropertyName = "Speakers";

        private List<Speaker> _speakers = new List<Speaker>();

        /// <summary>
        /// Gets the Speakers property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<Speaker> Speakers
        {
            get
            {
                return _speakers;
            }

            set
            {
                if (_speakers == value)
                {
                    return;
                }

                var oldValue = _speakers;
                _speakers = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SpeakersPropertyName);
            }
        }

        internal void SendSessionNavigatingMessage(Session session)
        {
            Messenger.Default.Send<SessionSelectionChangedMessage>(new SessionSelectionChangedMessage { Session = session });
        }


        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}

        internal void SendSpeakerNavigatingMessage(Speaker speaker)
        {
            Messenger.Default.Send<SpeakerSelectionChangedMessage>(new SpeakerSelectionChangedMessage { Speaker = speaker });

        }
    }
}