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

namespace Prdc.Conference.Model
{
    public class CacheUpdateEventArgs : EventArgs
    {
        public int SessionCount { get; set; }
        public int SpeakerCount { get; set; }

    }
}
