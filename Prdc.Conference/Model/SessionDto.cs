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
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Prdc.Conference.Model
{
    public class SessionDto
    {        
        public int id { get; set; }
        public string title { get; set; }
        public string @abstract { get; set; }
        public List<int> speakers { get; set; }
        public List<string> tags { get; set; }
        public DateTime? start { get; set; }
        public DateTime? finish { get; set; }
        public string room { get; set; }
    }
}
