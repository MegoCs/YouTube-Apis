using System;
using System.Collections.Generic;

namespace YouTubeKeysHouse.Models
{
    public partial class Usertracker
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int KeysFrom { get; set; }
        public int KeysToFetch { get; set; }
        public int? ActualKeysProcessed { get; set; }
    }
}
