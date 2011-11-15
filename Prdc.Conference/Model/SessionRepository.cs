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
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Prdc.Conference.Model
{
    public class SessionRepository
    {
        // keep track of when we last looked up session data from the cache
        // private DateTime _updateTimestamp = DateTime.Now;


        public IEnumerable<Session> GetSessions()
        {
            // we're going to return an empty list if we don't have anything locally
            List<Session> result = new List<Session>();

            // pull items from local storage if available
            if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionCache"))
                result = (List<Session>)IsolatedStorageSettings.ApplicationSettings["SessionCache"];

            return result;
        }

        public List<Session> GetFavoriteSessions()
        {
            List<Session> result = new List<Session>();

            // get the user's saved sessions
            List<int> savedSessions = GetSavedSessions(); 

            foreach (int sessionID in savedSessions)
            {
                // todo: get from db
                result.Add(new Session { SessionId = sessionID });
            }

            return result;
        }

        private static List<int> GetSavedSessions()
        {
            List<int> result = new List<int>();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("SavedSessions"))
                result = (List<int>)IsolatedStorageSettings.ApplicationSettings["SavedSessions"];

            return result;
        }

        public bool SaveFavoriteSession(int session)
        {
            bool result = false;

            // get the list of saved sessions
            List<int> savedSessions = GetSavedSessions();

            // add the new session and save
            savedSessions.Add(session);
            try
            {
                IsolatedStorageSettings.ApplicationSettings["SavedSessions"] = savedSessions;
            }
            catch (IsolatedStorageException ex)
            {
                result = false;             
            }

            return result;
        }



    }
}
