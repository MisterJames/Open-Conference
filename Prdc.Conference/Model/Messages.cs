using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prdc.Conference.Model
{
    public class CacheUpdatedMessage
    {
    }

    public class SessionSelectionChangedMessage
    {
        public Session Session { get; set; }

    }

    public class SpeakerSelectionChangedMessage
    {
        public Speaker Speaker { get; set; }
    }

}
