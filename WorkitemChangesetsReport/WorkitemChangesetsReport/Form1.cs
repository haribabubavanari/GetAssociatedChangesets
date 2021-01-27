using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkitemChangesetsReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();           
        }

        private void GetChangesetsList(string wiID)
        {
           // string wiID = "5";
            List<string> asscChanges = new List<string>();
            JSONHelper _jsonHelper = new JSONHelper();

           

            string result = _jsonHelper.GetTFSJsonData(WorkitemChangesetsReport.Properties.Settings.Default.TfsUrl +
                                                        "/_apis/wit/workitems/" + wiID
                                                        + "?$expand=relations&api-version=1.0");

            if (!string.IsNullOrEmpty(result))
            {
                foreach(Relations relation in JsonConvert.DeserializeObject<Object>(result).relations)
                {
                    if(relation.rel== "ArtifactLink")
                    {
                        asscChanges.Add(relation.url.Split('/').Last());
                    }
                }
            }
            else
            {
               // return null;
            }

            DataTable _tableList = new DataTable();
            _tableList= GetChangesetInfo(asscChanges);

           dataGridView1.DataSource = _tableList;
            //dataGridView1.databind

            //dataGridView1.DataBindingComplete();

            // dataGridView1

        }

        private DataTable GetChangesetInfo(List<string> asscChanges)
        {
            DataTable gridReportData = new DataTable();

            gridReportData.Columns.Add("DateTime");
            gridReportData.Columns.Add("Changeset ID");
            gridReportData.Columns.Add("User");
            gridReportData.Columns.Add("TFS Comment");
            gridReportData.Columns.Add("Url");
           

            foreach (string change in asscChanges)
            {
                JSONHelper _jsonHelper = new JSONHelper();
                string result = _jsonHelper.GetTFSJsonData(WorkitemChangesetsReport.Properties.Settings.Default.TfsUrl +
                                                            "/_apis/tfvc/changesets/" + change+ "?api-version=1.0");

                if (!string.IsNullOrEmpty(result))
                {
                    var _changesetInfo = JsonConvert.DeserializeObject<Changeset>(result);

                    DataRow dr = gridReportData.NewRow();
                    dr["DateTime"] = _changesetInfo.createdDate;
                    dr["Changeset ID"] = _changesetInfo.changesetId;
                    dr["user"] = _changesetInfo.author["displayName"];
                    dr["TFS Comment"] = _changesetInfo.comment.Replace("\n", "").Replace("\r", ""); ;
                    dr["Url"] = _changesetInfo.url;
                    //dr["url"] = "<a href='"+ _changesetInfo.url + "'>Changes Info</a>";
                    //"<a href='www.stackoverflow.com'>Stack Overflow</a>"
                    gridReportData.Rows.Add(dr);
                }
                else
                {
                    return null;
                }
            }

          
            return gridReportData;
        }


        public class Changeset
        {
            public int changesetId { get; set; }
            public Uri url { get; set; }
            public DateTime createdDate { get; set; }
            public string comment { get; set; }
            // public object author { get; set; }
            public Dictionary<string, string> author { get; set;}
        }

      
        public class Object
        {
            public int id { get; set; }

            public int rev { get; set; }
            public IList<Relations> relations { get; set; }
        }

         public class Relations
        {
           // public IList<Value> value { get; set; }
             public string rel { get; set; }
            public string url { get; set; }
        }

        public class Value
        {
            public string rel { get; set; }
            public string url { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // dataGridView1.DataSource = gridReportData;
            GetChangesetsList(textBox1.Text);
        }

       }
}
