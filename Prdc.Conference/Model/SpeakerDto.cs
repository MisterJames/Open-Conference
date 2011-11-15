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
    public class SpeakerDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string bio { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string website { get; set; }
        public string twitter { get; set; }
        public string location { get; set; }
        public string blog { get; set; }
        public string picture { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
