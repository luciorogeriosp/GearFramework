namespace GearFramework
{
    public class LoadMediaAttributes
    {
        public enum enuSort
        {
            ASC,
            DESC
        }

        /// <summary>
        /// This property execute the filter by Area Token. This token is displayed in the Areas Tab at Media Project Edit page in Gear Panel. 
        /// Set one of this properties to filter Medias: AreaId or AreaParentId or MediaId.
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// This property execute the filter by Media Token. This token is displayed in the Media Content Edit page in Gear Panel.
        /// Set one of this properties to filter Medias: AreaId or AreaParentId or MediaId.
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// This property execute the filter by Area Parent Token. This token is displayed in the Areas Tab at Media Project Edit page in Gear Panel. 
        /// Set one of this properties to filter Medias: AreaId or AreaParentId or MediaId.
        /// </summary>
        public string AreaParentId { get; set; }

        /// <summary>
        /// This property define the content you need to find in one or more fields.
        /// Ex.: language:'PT' AND city:'São Paulo'
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// This property define the content you need to find in all fields.
        /// Ex.: São Paulo Events
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// This property define the number of Actual Page, when you need to use pagination.
        /// Combine this property with the Rows.        
        /// </summary>
        public int ActualPage { get; set; }

        /// <summary>
        /// This property define the number of Rows, when you need to use pagination.
        /// Combine this property with the ActualPage.        
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// This property define the list of dynamic fields you need to get. When this property do not set, Gear returns all of dynamic fields.
        /// Separated the fields by ",".
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// This property define the name of fields you need to order.
        /// Combine this property with the Sort.        
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// This property define the orientation order. asc or desc.
        /// Combine this property with the Order.        
        /// </summary>
        public enuSort? Sort { get; set; }

        /// <summary>
        /// When this property will set with true value, Gear load the Media Files.
        /// Default: false.        
        /// </summary>
        public bool Files { get; set; }
    }
}