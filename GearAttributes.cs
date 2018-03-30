using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GearFramework
{    
    public class GearAttributes
    {
        public GearAttributes()
        {
            CacheSeconds = 30;
        }

        /// <summary>
        /// Gear API Url
        /// </summary>
        public string Url { get; set; }
       
        /// <summary>
        /// AccessKey configured in Gear / Media / Project / Access Key
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Enable Cache
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// Cache Seconds
        /// Default: 900
        /// </summary>
        public int CacheSeconds { get; set; }
    }
}