using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace GearFramework
{
    public class Media
    {
        private GearAttributes _gearAttributes;
        private const string _LoadMediaLink = "load/default.asp?source=LoadMedia";
        private const string _SaveMediaLink = "save/default.asp?source=SaveMedia";

        public Media(GearAttributes gearAttributes)
        {
            _gearAttributes = gearAttributes;
        }

        /// <summary>
        /// Receive Media registered into Gear Media Area.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns>MediaResult</returns>
        public MediaResult LoadMedia(LoadMediaAttributes attributes)
        {
            MediaResult result = null;

            try
            {
                ulong hash = 0;

                if (_gearAttributes.Cache)
                {
                    hash = attributes.CreateHashCode();

                    if (GearAPI.Exists("GearFramework_LoadMedia_" + hash))
                    {
                        result = GearAPI.Get<MediaResult>("GearFramework_LoadMedia_" + hash);
                    }
                }

                if (result == null)
                {

                    WebClient client = new WebClient();
                    client.Encoding = Encoding.UTF8;

                    #region Create String Parameters
                    StringBuilder sbParameters = new StringBuilder();
                    bool blnPagination = false;

                    if (attributes.ActualPage > 0 && attributes.Rows > 0)
                    {
                        blnPagination = true;
                    }

                    sbParameters.Append("&pagination=" + (blnPagination ? 1 : 0));
                    sbParameters.Append("&rows=" + (attributes.Rows > 0 ? attributes.Rows.ToString() : ""));
                    sbParameters.Append("&actualPage=" + (attributes.ActualPage > 0 ? attributes.ActualPage.ToString() : ""));
                    sbParameters.Append("&query=" + (String.IsNullOrEmpty(attributes.Query) ? "" : attributes.Query));
                    sbParameters.Append("&search=" + (String.IsNullOrEmpty(attributes.Search) ? "" : attributes.Search));
                    sbParameters.Append("&fields=" + (String.IsNullOrEmpty(attributes.Fields) ? "" : attributes.Fields));
                    sbParameters.Append("&fieldOrder=" + (String.IsNullOrEmpty(attributes.Order) ? "" : attributes.Order));
                    sbParameters.Append("&orderSort=" + (attributes.Sort.HasValue == false ? "" : attributes.Sort.Value == LoadMediaAttributes.enuSort.ASC ? "ASC" : "DESC"));
                    sbParameters.Append("&files=" + (attributes.Files ? 1 : 0));

                    if (!String.IsNullOrEmpty(attributes.AreaId))
                    {
                        sbParameters.Append("&mediaAreaID=" + attributes.AreaId);
                    }

                    if (!String.IsNullOrEmpty(attributes.MediaId))
                    {
                        sbParameters.Append("&mediaID=" + attributes.MediaId);
                    }

                    if (!String.IsNullOrEmpty(attributes.AreaParentId))
                    {
                        sbParameters.Append("&mediaAreaParentID=" + attributes.AreaParentId);
                    }

                    if (!String.IsNullOrEmpty(_gearAttributes.AccessKey))
                    {
                        sbParameters.Append("&accesskey=" + _gearAttributes.AccessKey);
                    }
                    #endregion

                    string downloadString = client.DownloadString(_gearAttributes.Url + _LoadMediaLink + sbParameters.ToString());

                    dynamic objMedia = JsonConvert.DeserializeObject<dynamic>(downloadString);

                    result = new MediaResult();
                    if (objMedia != null && objMedia.StatusCode != null)
                    {
                        result.StatusCode = Convert.ToInt32(objMedia.StatusCode);
                        result.Message = objMedia.Message;
                        result.MediaTotalRows = objMedia.MediaTotalRows;
                        result.MediaList = new DataTable("MediaList");

                        bool ColumnsCreated = false;
                        DataRow objDataRow = null;

                        foreach (dynamic MediaItem in objMedia.MediaList)
                        {
                            dynamic MediaItemJObject = MediaItem as Newtonsoft.Json.Linq.JObject;
                            IEnumerable<Newtonsoft.Json.Linq.JProperty> JPropertyList = ((Newtonsoft.Json.Linq.JObject)MediaItemJObject).Properties();

                            if (!ColumnsCreated)
                            {
                                foreach (var JPropertyItem in JPropertyList)
                                {
                                    string name = JPropertyItem.Name;

                                    result.MediaList.Columns.Add(new DataColumn(name, typeof(string)));
                                }

                                ColumnsCreated = true;
                            }

                            objDataRow = result.MediaList.NewRow();

                            foreach (var JPropertyItem in JPropertyList)
                            {
                                try
                                {
                                    string name = JPropertyItem.Name;
                                    object value = System.Net.WebUtility.HtmlDecode(((Newtonsoft.Json.Linq.JValue)JPropertyItem.Value).Value.ToString()); //HttpContext.Current.Server.HtmlDecode

                                    if (value != null && value.ToString().IndexOf("http://gear.bdg.media/") > -1)
                                    {
                                        value = value.ToString().Replace("http://gear.bdg.media/", "https://bdg.media/gear/");
                                    }
                                    if (value != null && value.ToString().IndexOf("https://gear.bdg.media/") > -1)
                                    {
                                        value = value.ToString().Replace("https://gear.bdg.media/", "https://bdg.media/gear/");
                                    }

                                    objDataRow[name] = value;
                                }
                                catch (Exception exInt)
                                {

                                }
                            }

                            result.MediaList.Rows.Add(objDataRow);




                        }
                    }

                    if (_gearAttributes.Cache)
                    {
                        GearAPI.Add<MediaResult>(result, "GearFramework_LoadMedia_" + hash, _gearAttributes.CacheSeconds);
                    }
                }
            }
            catch (Exception ex)
            {
                result = new MediaResult();
                result.StatusCode = 0;
                result.Message = ex.Message;
            }

            return result;
        }

        public SaveMediaResult SaveMedia(SaveMediaAttributes attributes)
        {
            SaveMediaResult result = new SaveMediaResult();

            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;

                #region Create String Parameters
                StringBuilder sbParameters = new StringBuilder();

                foreach (var item in attributes.Values)
                {
                    sbParameters.Append("&" + item.Name + "=" + item.Value);
                }

                if (!String.IsNullOrEmpty(attributes.MediaAreaId))
                {
                    sbParameters.Append("&mediaAreaID=" + attributes.MediaAreaId);
                }
                else
                {
                    throw new Exception("MediaAreaId is required.");
                }

                if (!String.IsNullOrEmpty(attributes.MediaId))
                {
                    sbParameters.Append("&mediaID=" + attributes.MediaId);
                }
                else
                {
                    throw new Exception("MediaID is required.");
                }

                if (!String.IsNullOrEmpty(_gearAttributes.AccessKey))
                {
                    sbParameters.Append("&accesskey=" + _gearAttributes.AccessKey);
                }
                else
                {
                    throw new Exception("AccessKey is required.");
                }
                #endregion

                string downloadString = client.DownloadString(_gearAttributes.Url + _SaveMediaLink + sbParameters.ToString());

                dynamic objMedia = JsonConvert.DeserializeObject<dynamic>(downloadString);

                result.StatusCode = Convert.ToInt32(objMedia.StatusCode);
                result.Message = objMedia.Message;
            }
            catch (Exception ex)
            {
                result.StatusCode = 0;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}