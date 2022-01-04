using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace ProjectViewer.Details
{
    public partial class HTMLDetails : Form, IDetailsForm
    {
        public HTMLDetails()
        {
            InitializeComponent();
        }


        public void InitDetails(XPathNavigator project, int type, int index)
        {
            var projItem = GetProjectItem(project, type, index);            

            var htmlNodes = projItem.Select("hContStrData/rowset/row/hContStrData");
            StringBuilder sb = new StringBuilder();
            while (htmlNodes.MoveNext())
            {
                sb.Append(htmlNodes.Current.Value);
            }

            var htmlText = sb.ToString();

            this.Text = String.Join(":", "HTML Object", projItem.SelectSingleNode("szContName").Value);
            simpleEditor1.ReadOnly = false;
            simpleEditor1.Text = htmlText;
            simpleEditor1.ReadOnly = true;
            simpleEditor1.Margins[0].Width = 20;

        }

        private XPathNavigator GetProjectItem(XPathNavigator project, int type, int index)
        {
            var nodeIterator = project.Select("/instance[@class='CRM']/rowset[@name='CrmDefn']/row[eContType=4]");
            for (var x = 0; x <= index; x++)
            {
                nodeIterator.MoveNext();
            }

            return nodeIterator.Current;
        }
    }

}
