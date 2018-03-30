using System.Collections.Generic;

namespace GearFramework
{
    public class SaveMediaAttributes
    {
        public SaveMediaAttributes()
        {
            Values = new List<MediaValuesModel>();
        }

        /// <summary>
        /// This property determinate Media Area ID Edit is save.
        /// </summary>
        public string MediaAreaId { get; set; }

        /// <summary>
        /// This property determinate Media Content ID Edit is save.
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// This property determinate Values to save.
        /// </summary>
        public List<MediaValuesModel> Values { get; set; }
    }
}