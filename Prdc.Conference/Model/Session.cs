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
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Linq;

namespace Prdc.Conference.Model
{
    [Table]
    public class Session
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, AutoSync = AutoSync.OnInsert)]
        public int SessionId { get; set; }
        [Column]
        public string Title { get; set; }
        [Column]
        public string Abstract { get; set; }
        [Column]
        public string TagList { get; set; }
        [Column]
        public DateTime? Start { get; set; }
        [Column]
        public DateTime? Finish { get; set; }
        [Column]
        public string Room { get; set; }

        [Association(OtherKey = "SessionId", ThisKey = "SessionId")]
        public EntitySet<SessionSpeakers> SessionSpeakers { get; set; }

        [Association(OtherKey = "SessionId", ThisKey = "SessionId")]
        public EntitySet<SessionTags> SessionTags { get; set; }


        public string GetSpeakers
        {
            get
            {
                var speakers = this.SessionSpeakers.Select(s => string.Format("{0} {1}", s.Speaker.FirstName, s.Speaker.LastName));
                return string.Join(", ", speakers.ToList());
            }
        }


    }
}
