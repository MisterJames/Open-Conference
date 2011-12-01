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
using Ninject;
using Ninject.Modules;

namespace Prdc.Conference.Model
{
    public class LegacyCacheModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICacheManager>().To<CacheManager>();
        }
    }

    public class ConferenceCacheModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICacheManager>().To<ConferenceCacheManager>();
        }
    }

}
