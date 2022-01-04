using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using ProjectViewer.Details;

namespace ProjectViewer.Overview
{
    public class HTMLOverview : IOverviewHandler
    {
        public string GetTitle(int type)
        {
            return "HTML Objects";
        }
        public List<string> GetHeaders(int type)
        {
            List<string> headers = new List<string>();
            headers.Add("HTML");

            return headers;
        }

        public List<string> GetValues(int type, XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            return values;

        }

        public IDetailsForm GetDetailsForm(int type)
        {
            return new HTMLDetails();
        }
    }
}
