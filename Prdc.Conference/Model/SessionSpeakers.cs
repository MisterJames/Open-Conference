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
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Prdc.Conference.Model
{
    [Table]
    public class SessionSpeakers
    {
        [Column(IsPrimaryKey=true)]
        public int SessionId { get; set; }
        [Column(IsPrimaryKey = true)]
        public int SpeakerId { get; set; }

        private EntityRef<Session> _session;
        private EntityRef<Speaker> _speaker;

        [Association(OtherKey = "SpeakerId", ThisKey = "SpeakerId", Storage = "_speaker")]
        public Speaker Speaker
        {
            get{ return _speaker.Entity;}
            set{ _speaker.Entity = value;  SpeakerId = value.SpeakerId;}
        }

        [Association(OtherKey="SessionId" , ThisKey="SessionId" , Storage = "_session")]
        public Session Session
        {
            get { return _session.Entity;}
            set { _session.Entity = value; SessionId = value.SessionId; }
        }

    }
}
