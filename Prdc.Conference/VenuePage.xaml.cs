using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls.Maps;

namespace Prdc.Conference
{
    public partial class VenuePage : PhoneApplicationPage
    {
        MapLayer imageLayer;

        public VenuePage()
        {
            InitializeComponent();
            imageLayer = new MapLayer();
            mapArea.Children.Add(imageLayer);

            mapArea.Center = new GeoCoordinate(49.879738, -97.203598);
            Image pinImage = new Image();
            pinImage.Source = new BitmapImage(new Uri("images/prairiedevconpin.png", UriKind.Relative));
            pinImage.Stretch = Stretch.None;
            PositionOrigin origin = PositionOrigin.BottomCenter;

            imageLayer.AddChild(pinImage, mapArea.Center, origin);

        }
    }
}