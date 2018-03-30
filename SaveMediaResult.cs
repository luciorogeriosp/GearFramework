using System;

namespace GearFramework
{
    [Serializable]
    public class SaveMediaResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}