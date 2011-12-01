using GalaSoft.MvvmLight;
using Prdc.Conference.Model;
using System;
using Ninject;

namespace Prdc.Conference.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private static StandardKernel _kernel = null;
        ICacheManager _cache = null;

        public string ApplicationTitle
        {
            get
            {
                return "PRAIRIE DEV CON";
            }
        }

        /// <summary>
        /// The <see cref="LastUpdate" /> property's name.
        /// </summary>
        public const string LastUpdatePropertyName = "LastUpdate";

        private DateTime _lastUpdate;

        /// <summary>
        /// Gets the LastUpdate property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public DateTime LastUpdate
        {
            get
            {
                return _lastUpdate;
            }

            set
            {
                if (_lastUpdate == value)
                {
                    return;
                }

                var oldValue = _lastUpdate;
                _lastUpdate = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LastUpdatePropertyName);

            }
        }


        /// <summary>
        /// The <see cref="HasCache" /> property's name.
        /// </summary>
        public const string HasCachePropertyName = "HasCache";

        private bool _hasCache = false;



        /// <summary>
        /// Gets the HasCache property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool HasCache
        {
            get
            {
                if (!_hasCache)
                {
                    _hasCache = _cache.HasCacheData;
                }
                return _hasCache;
            }

            set
            {
                if (_hasCache == value)
                {
                    return;
                }

                var oldValue = _hasCache;
                _hasCache = value;


                // Update bindings, no broadcast
                RaisePropertyChanged(HasCachePropertyName);

                //// Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                //RaisePropertyChanged(HasCachePropertyName, oldValue, value, true);
            }
        }

        public string PageName
        {
            get
            {
                return "Winnipeg '11";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to MVVM Light";
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {

                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

            _kernel = new StandardKernel(new LegacyCacheModule());
            _cache = _kernel.Get<ICacheManager>();
            

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}