using ProjectViewer.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ProjectViewer.Overview
{
    
    public interface IOverviewHandler
    {
        string GetTitle(int type);
        List<string> GetHeaders(int type);
        List<string> GetValues(int type, XPathNavigator item);

        IDetailsForm GetDetailsForm(int type);
    }

    public class DefaultOverviewHandler : IOverviewHandler
    {
        public static Dictionary<int, IOverviewHandler> OverviewMap = new Dictionary<int, IOverviewHandler>();

        static DefaultOverviewHandler()
        {
            PPCOverview ppc = new PPCOverview();
            OverviewMap.Add(8, ppc);
            OverviewMap.Add(43, ppc);
            OverviewMap.Add(44, ppc);
            OverviewMap.Add(46, ppc);
            OverviewMap.Add(47, ppc);
            OverviewMap.Add(48, ppc);
            OverviewMap.Add(58, ppc);

            OverviewMap.Add(30, new SQLOverview());
            OverviewMap.Add(51, new HTMLOverview());
            OverviewMap.Add(0, new RecordOverview());
        }

        public string GetTitle(int type)
        {
            return "Generic Overview";
        }
        public List<string> GetHeaders(int type)
        {
            List<string> headers = new List<string>();
            headers.Add("Value 1");
            headers.Add("Value 2");
            headers.Add("Value 3");
            headers.Add("Value 4");
            headers.Add("Value 5");
            headers.Add("Value 6");
            headers.Add("Value 7");

            return headers;
        }

        public List<string> GetValues(int type, XPathNavigator item)
        {
            List<string> values = new List<string>();

            for(var x = 0; x < 7; x++)
            {
                var currentValue = item.SelectSingleNode("szObjectValue_" + x);
                if (currentValue != null)
                {
                    values.Add(currentValue.Value);
                } else
                {
                    values.Add("");
                }
            }

            return values;
        }

        public IDetailsForm GetDetailsForm(int type)
        {
            return null;
        }
    }
}
