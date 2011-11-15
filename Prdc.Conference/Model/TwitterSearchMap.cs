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

namespace Prdc.Conference.Model
{
    public class TwitterSearchMap
    {
        public double completed_in { get; set; }
        public Int64 max_id { get; set; }
        public string max_id_str { get; set; }
        public int page { get; set; }
        public string query { get; set; }
        public string refresh_url { get; set; }
        public List<TwitterSearchResult> results { get; set; }
        public int results_per_page { get; set; }
        public Int64 since_id { get; set; }
        public string since_id_str { get; set; }
    }

    public class TwitterSearchResult
    {
        public DateTime created_at { get; set; }
        public string from_user { get; set; }
        public int from_user_id { get; set; }
        public string from_user_id_str { get; set; }
        public string geo { get; set; }
        public Int64 id { get; set; }
        public string id_str { get; set; }
        public string iso_language_code { get; set; }
    //    public object metadata { get; set; }
        public string profile_image_url { get; set; }
        public string source { get; set; }
        public string text { get; set; }
        public string to_user { get; set; }
        public Int64 to_user_id { get; set; }
        public string to_user_id_str { get; set; }

    }



}
