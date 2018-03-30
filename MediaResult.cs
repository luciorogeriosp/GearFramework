using System;
using System.Data;

namespace GearFramework
{
    [Serializable]
    public class MediaResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public int MediaTotalRows { get; set; }

        public DataTable MediaList { get; set; }
    }
}