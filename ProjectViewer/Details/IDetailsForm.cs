using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ProjectViewer.Details
{
    public interface IDetailsForm
    {
        void InitDetails(XPathNavigator project, int type, int index);
    }
}
