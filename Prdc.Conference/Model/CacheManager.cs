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
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data.Linq;
using System.Linq;
using System.IO;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;

namespace Prdc.Conference.Model
{
    public class CacheManager : ICacheManager
    {
        WebClient _client = null;

        private DateTime? _lastupdated;

        public DateTime? LastUpdate
        {
            get
            {
                DateTime? result = null;

                if (_lastupdated.HasValue)
                    result = _lastupdated.Value;
                else
                {
                    // look up the value in iso storage
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("LastUpdate"))
                    {
                        _lastupdated = result = (DateTime)IsolatedStorageSettings.ApplicationSettings["LastUpdate"];
                    }
                }

                return result;
            }
        }

        public bool HasCacheData
        {
            get
            {
                // load from iso storage
                return IsolatedStorageSettings.ApplicationSettings.Contains("LastUpdate");
            }
        }

        public void UpdateCache()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                // attempt to get speaker and session infos from the interwebs
                if (_client == null) _client = new WebClient();
                if (_client == null) _client = new WebClient();

                UpdateCachedSpeakers();

            }
            else
            {
                NetworkWentBoom();
            }

        }

        public event EventHandler OnNetworkIckiness;
        private void NetworkWentBoom()
        {
            if (OnNetworkIckiness != null)
                OnNetworkIckiness(this, new EventArgs());
        }

        public event EventHandler<CacheUpdateEventArgs> OnRefreshCompleted;
        private void ReportCompletedRefresh(int sessions, int speakers)
        {
            // note update time/date
            IsolatedStorageSettings.ApplicationSettings["LastUpdate"] = DateTime.Now;
            _lastupdated = (DateTime)IsolatedStorageSettings.ApplicationSettings["LastUpdate"];

            if (OnRefreshCompleted != null)
                OnRefreshCompleted(this, new CacheUpdateEventArgs { SessionCount = sessions, SpeakerCount = speakers });

            Messenger.Default.Send<CacheUpdatedMessage>(new CacheUpdatedMessage());

        }

        public event EventHandler<ProgressEventArgs> SpeakersProgressReported;
        private void ReportSpeakerProgress(string message, int percent)
        {
            if (SpeakersProgressReported != null)
            {
                SpeakersProgressReported(this, new ProgressEventArgs { Message = message, Percent = percent });
            }
        }

        public event EventHandler<ProgressEventArgs> SessionProgressReported;
        private void ReportSessionProgress(string message, int percent)
        {
            if (SessionProgressReported != null)
            {
                SessionProgressReported(this, new ProgressEventArgs { Message = message, Percent = percent });
            }
        }

        private bool UpdateCachedSpeakers()
        {
            bool result = false;

            if (true)
            {
                // get json from server
                _client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(_speakerClient_DownloadStringCompleted);
                _client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_speakerClient_DownloadProgressChanged);

                ReportSpeakerProgress("Starting speaker download...", 0);
                _client.DownloadStringAsync(new Uri("http://prairiedevcon.com/speakers.json"));

                result = true;
            }

            return result;
        }

        void _speakerClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string stringArray = e.Result.Substring(12, e.Result.Length - 13);

                ReportSpeakerProgress("Reading data...", 70);
                List<SpeakerDto> speakers;
                try
                {
                    speakers = JsonConvert.DeserializeObject<List<SpeakerDto>>(stringArray);
                }
                catch (JsonReaderException ex)
                {
                    NetworkWentBoom();
                    return;
                }
                
                ReportSpeakerProgress("Preparing to save...", 75);

                ConferenceDataContext localDc = new ConferenceDataContext("isostore:/conference.sdf");

                // make a db if it ain't there
                if (!localDc.DatabaseExists()) 
                    localDc.CreateDatabase();

                ReportSpeakerProgress("Clearing existing data...", 80);
                int c = localDc.Speakers.Count();

                localDc.SessionTags.DeleteAllOnSubmit(localDc.SessionTags);
                localDc.Tags.DeleteAllOnSubmit(localDc.Tags);
                localDc.SessionSpeakers.DeleteAllOnSubmit(localDc.SessionSpeakers);
                localDc.Speakers.DeleteAllOnSubmit(localDc.Speakers);
                localDc.SubmitChanges();

                ReportSpeakerProgress("Saving to device...", 85);
                var speakerlist = speakers.Select(s => new Speaker
                {
                    Bio = s.bio,
                    Blog = s.blog,
                    CreatedAt = s.created_at,
                    Email = s.email,
                    FirstName = s.first_name,
                    LastName = s.last_name,
                    Location = s.location,
                    Picture = s.picture,
                    SpeakerId = s.id,
                    Twitter = s.twitter,
                    Website = s.website,
                    UpdatedAt = s.updated_at,
                    SessionSpeakers = new EntitySet<SessionSpeakers>()
                }).ToList();

                speakerlist.Add(new Speaker { FirstName = "TBD", SessionSpeakers = new EntitySet<SessionSpeakers>(), CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now });

                foreach (var spkr in speakerlist)
                {
                    localDc.Speakers.InsertOnSubmit(spkr);
                    localDc.SubmitChanges();
                }

                ReportSpeakerProgress("Speakers downloaded and saved.", 100);

                _client = new WebClient();
                UpdateCachedSessions();
            }
            else
            {
                NetworkWentBoom();
            }

        }

        void _speakerClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ReportSpeakerProgress(string.Format("Speakers downloaded {0}%", e.ProgressPercentage), e.ProgressPercentage);
        }

        private void UpdateCachedSessions()
        {
            // get json from server
            _client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(SessionDownloadStringCompleted);
            _client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(SessionDownloadProgressChanged);

            ReportSessionProgress("Starting download...", 0);

            _client.DownloadStringAsync(new Uri("http://prairiedevcon.com/sessions.json"));

        }

        void SessionDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int sessionPrct = (e.ProgressPercentage * 100) / 125;
            ReportSessionProgress(string.Format("Sessions downloaded {0}%", sessionPrct), sessionPrct);
        }

        void SessionDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string stringArray = e.Result.Substring(12, e.Result.Length - 13);

                ReportSessionProgress("Reading data...", 80);
                List<SessionDto> sessions = JsonConvert.DeserializeObject<List<SessionDto>>(stringArray);

                ReportSessionProgress("Preparing to save data...", 85);
                ConferenceDataContext dc = new ConferenceDataContext("isostore:/conference.sdf");

                dc.Sessions.DeleteAllOnSubmit(dc.Sessions);
                dc.SubmitChanges();

                ReportSessionProgress("Saving to device...", 90);
                List<Session> sessionList = new List<Session>();
                foreach (var ssn in sessions.OrderBy(r=>r.title))
                {
                    // check to see if the tags exist already, if not add them
                    foreach (var tag in ssn.tags)
                    {
                        if (dc.Tags.Where(t=>t.Title == tag).Count() == 0)
                        {
                            dc.Tags.InsertOnSubmit(new Tag { Title = tag, SessionTags = new EntitySet<SessionTags>() });
                            dc.SubmitChanges();
                        }
                    }

                    // create a db version of the record
                    Session s = new Session
                    {
                        Abstract = ssn.@abstract,
                        Finish = ssn.finish,
                        Room = ssn.room,
                        SessionId = ssn.id,
                        Start = ssn.start,
                        TagList = string.Join(", ", ssn.tags).ToUpper(),
                        Title = ssn.title,
                        SessionSpeakers = new EntitySet<SessionSpeakers>(),
                        SessionTags = new EntitySet<SessionTags>()
                    };
                    
                    // update the db
                    dc.Sessions.InsertOnSubmit(s);
                    dc.SubmitChanges();

                    // build up the speaker list
                    foreach (int speakerId in ssn.speakers)
                    {
                        Speaker speaker = dc.Speakers.Where(sp => sp.SpeakerId == speakerId).First();
                        s.SessionSpeakers.Add(new SessionSpeakers { Speaker = speaker, Session = s });
                        dc.SubmitChanges();
                    }

                    // default is a speaker if none are present
                    if (s.SessionSpeakers.Count == 0)
                        s.SessionSpeakers.Add(new SessionSpeakers { Session = s, Speaker = dc.Speakers.Where(k => k.FirstName == "TBD").First() });

                    // build up the tag list
                    foreach (var item in ssn.tags)
                    {
                        Tag tag = dc.Tags.Where(t => t.Title == item).First();
                        s.SessionTags.Add(new SessionTags { Session = s, Tag = tag });
                        dc.SubmitChanges();
                    }
                }


                ReportSessionProgress("Sessions downloaded and saved.", 95);

                int sessionCount = dc.Sessions.Count();
                int speakersCount = dc.Speakers.Count();
                ReportCompletedRefresh(sessionCount, speakersCount);
                dc.Dispose();
                dc = null;
            }
            else
            {
                NetworkWentBoom();
            }

        }

        




    }
}
