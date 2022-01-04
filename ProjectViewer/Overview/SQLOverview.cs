using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using ProjectViewer.Details;

namespace ProjectViewer.Overview
{
    class SQLOverview : IOverviewHandler
    {
        public IDetailsForm GetDetailsForm(int type)
        {
            return new SQLDetails();
        }

        public List<string> GetHeaders(int type)
        {
            List<string> headers = new List<string>();
            headers.Add("SQL ID");
            headers.Add("Type");
            return headers;
        }

        public string GetTitle(int type)
        {
            return "SQL";
        }

        public List<string> GetValues(int type, XPathNavigator item)
        {

            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value == "2" ? "Record View" : "Normal");

            return values;
        }
    }
}
