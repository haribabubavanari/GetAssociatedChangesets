using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkitemChangesetsReport
{
    class JSONHelper
    {
        public string GetTFSJsonData(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Credentials = new NetworkCredential(WorkitemChangesetsReport.Properties.Settings.Default.UserName,
                                                            WorkitemChangesetsReport.Properties.Settings.Default.Password,
                                                            WorkitemChangesetsReport.Properties.Settings.Default.Domain
                                                            );
                request.Method = "GET";
                request.ContentType = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Console.Write(response.StatusCode);
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
