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
    public class ProgressEventArgs :EventArgs
    {
        public string Message { get; set; }
        public int Percent { get; set; }

    }
}
