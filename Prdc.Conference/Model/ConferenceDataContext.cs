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

namespace Prdc.Conference.Model
{
    public class ConferenceDataContext : DataContext
    {
        public Table<Session> Sessions;
        public Table<Speaker> Speakers;
        public Table<SessionSpeakers> SessionSpeakers;
        public Table<Tag> Tags;
        public Table<SessionTags> SessionTags;

        public ConferenceDataContext(string connection) :
            base(connection) { }

    }
}
