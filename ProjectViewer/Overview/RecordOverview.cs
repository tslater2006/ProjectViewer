using System;
using System.Collections.Generic;
using System.Xml.XPath;
using ProjectViewer.Details;

namespace ProjectViewer.Overview
{
    public class RecordOverview : IOverviewHandler
    {
        
        public IDetailsForm GetDetailsForm(int type)
        {
            throw new NotImplementedException();
        }

        public List<string> GetHeaders(int type)
        {
            List<string> headers = new List<string>();
            headers.Add("Record Name");

            return headers;
        }

        public string GetTitle(int type)
        {
            return "Record Objects";
        }

        public List<string> GetValues(int type, XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            return values;
        }
    }
}
