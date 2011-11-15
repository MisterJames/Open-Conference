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
    public class SessionTags
    {
        [Column(IsPrimaryKey = true)]
        public int SessionId { get; set; }
        [Column(IsPrimaryKey=true)]
        public int TagId { get; set; }

        private EntityRef<Session> _session;
        [Association(OtherKey = "SessionId", ThisKey = "SessionId", Storage = "_session")]
        public Session Session
        {
            get { return _session.Entity; }
            set { _session.Entity = value; SessionId = value.SessionId; }
        }

        private EntityRef<Tag> _tag;
        [Association(OtherKey="TagId", ThisKey="TagId", Storage="_tag")]
        public Tag Tag
        {
            get { return _tag.Entity; }
            set { _tag.Entity = value; TagId = value.TagId; }
        }

    }
}
