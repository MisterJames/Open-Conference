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

namespace Prdc.Conference.Model
{
    [Table]
    public class Speaker
    {
        [Column(IsPrimaryKey = true, IsDbGenerated=false, AutoSync= AutoSync.OnInsert)]
        public int SpeakerId { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Bio { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; }
        [Column]
        public DateTime UpdatedAt { get; set; }
        [Column]
        public string Website { get; set; }
        [Column]
        public string Twitter { get; set; }
        [Column]
        public string Location { get; set; }
        [Column]
        public string Blog { get; set; }
        [Column]
        public string Picture { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }

        [Association(OtherKey="SpeakerId", ThisKey="SpeakerId")]
        public EntitySet<SessionSpeakers> SessionSpeakers { get; set; }

        public string ImageUrl { get { return "http://prairiedevcon.com/assets/speakers/"+this.Picture; } }

    }
}
