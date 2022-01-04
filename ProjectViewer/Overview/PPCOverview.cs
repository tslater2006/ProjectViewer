using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using ProjectViewer.Details;

namespace ProjectViewer.Overview
{
    public class PPCOverview : IOverviewHandler
    {
        public string GetTitle(int type)
        {
            switch (type)
            {
                case 8:
                    return "Record PeopleCode";
                case 43:
                    return "Application Engine PeopleCode";
                case 44:
                    return "Page PeopleCode";
                case 46:
                    return "Component PeopleCode";
                case 47:
                    return "Component Record PeopleCode";
                case 58:
                    return "Application Package PeopleCode";
                
            }
            return "Unknown";
        }

        public List<string> GetHeaders(int type)
        {
            List<string> headers = new List<string>();
            switch(type)
            {
                case 8:
                    headers.Add("Record");
                    headers.Add("Field");
                    headers.Add("Event");
                    break;
                case 43:
                    headers.Add("App Engine");
                    headers.Add("Section");
                    headers.Add("Step");
                    headers.Add("Event");
                    break;
                case 44:
                    headers.Add("Page");
                    headers.Add("Event");
                    break;
                case 46:
                    headers.Add("Component");
                    headers.Add("Market");
                    headers.Add("Event");
                    break;
                case 47:
                    headers.Add("Component");
                    headers.Add("Market");
                    headers.Add("Record");
                    headers.Add("Event");
                    break;
                case 48:
                    headers.Add("Component");
                    headers.Add("Market");
                    headers.Add("Record");
                    headers.Add("Field");
                    headers.Add("Event");
                    break;
                case 58:
                    headers.Add("Base Package");
                    headers.Add("Qualify Path");
                    headers.Add("Class");
                    break;
            }
            return headers;
        }

        public List<string> GetValues(int type,XPathNavigator item)
        {
            switch (type)
            {
                case 8:
                    return GetRecordPPCValues(item);
                case 43:
                    return GetAppEngineValues(item);
                case 44:
                    return GetPageValues(item);
                case 46:
                    return GetComponentValues(item);
                case 47:
                    return GetCompRecordValues(item);
                case 48:
                    return GetCompRecFldValues(item);
                case 58:
                    return GetAppPkgValues(item);
                
                
            }

            return null;
        }

        private List<string> GetCompRecFldValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value);
            values.Add(item.SelectSingleNode("szObjectValue_2").Value);
            values.Add(item.SelectSingleNode("szObjectValue_3").Value);
            values.Add("TBD");
            
            return values;
        }

        private List<string> GetCompRecordValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value);
            values.Add(item.SelectSingleNode("szObjectValue_2").Value);
            values.Add(item.SelectSingleNode("szObjectValue_3").Value);
            return values;
        }

        private List<string> GetComponentValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value);
            values.Add(item.SelectSingleNode("szObjectValue_2").Value);
            return values;
        }

        private List<string> GetAppEngineValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value.Split(' ')[0]);
            values.Add(item.SelectSingleNode("szObjectValue_2").Value);
            values.Add(item.SelectSingleNode("szObjectValue_3").Value);
            return values;
        }

        private List<string> GetPageValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value);
            return values;
        }

        public IDetailsForm GetDetailsForm(int type)
        {
            return new PeopleCodeDetails();
        }

        private List<string> GetRecordPPCValues(XPathNavigator item)
        {
            List<string> values = new List<string>();
            values.Add(item.SelectSingleNode("szObjectValue_0").Value);
            values.Add(item.SelectSingleNode("szObjectValue_1").Value);
            values.Add(item.SelectSingleNode("szObjectValue_2").Value);

            return values;
        }
        private List<string> GetAppPkgValues(XPathNavigator item)
        {
            List<string> packageParts = new List<string>();
            for (var x = 0; x < 7; x++)
            {
                var currentValue = item.SelectSingleNode("szObjectValue_" + x);
                if (currentValue != null)
                {
                    if (currentValue.Value.Length > 0)
                    {
                        packageParts.Add(currentValue.Value);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            List<string> values = new List<string>();
            values.Add(packageParts.First());
            packageParts.RemoveAt(0);

            string qualPath = "";
            while (packageParts.Count > 1)
            {
                qualPath += packageParts[0] + ":";
                packageParts.RemoveAt(0);
            }

            if (qualPath.Length > 0)
            {
                qualPath = qualPath.Substring(0, qualPath.Length - 1);
            }

            values.Add(qualPath);
            values.Add(packageParts[0]);
            packageParts.Clear();
            return values;
        }
    }
}
