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
    public class Tag
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int TagId { get; set; }
        [Column]
        public string Title { get; set; }

        [Association(OtherKey = "TagId", ThisKey = "TagId")]
        public EntitySet<SessionTags> SessionTags { get; set; }

    }
}
