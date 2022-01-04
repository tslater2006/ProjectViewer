using ScintillaNET;
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
    public partial class SQLDetails : Form, IDetailsForm
    {
        public SQLDetails()
        {
            InitializeComponent();
            SetStyles();
        }

        private void SetStyles()
        {
 

            //simpleEditor1.SetKeywords(0, keywords);
        }

        public void InitDetails(XPathNavigator project, int type, int index)
        {
            var projItem = GetProjectItem(project, type, index);

            var sqlNodes = projItem.Select("lpStmtT/rowset/row/lpszSqlText/rowset/row/lpszSqlText");
            StringBuilder sb = new StringBuilder();
            while (sqlNodes.MoveNext())
            {
                sb.Append(sqlNodes.Current.Value);
            }

            var sqlText = sb.ToString();

            this.Text = String.Join(": ", "SQL Object", projItem.SelectSingleNode("szSqlId").Value);
            simpleEditor1.ReadOnly = false;
            simpleEditor1.Text = sqlText;
            simpleEditor1.ReadOnly = true;
            simpleEditor1.WrapMode = ScintillaNET.WrapMode.Word;
            simpleEditor1.Margins[0].Width = 20;

        }

        private XPathNavigator GetProjectItem(XPathNavigator project, int type, int index)
        {
            var nodeIterator = project.Select($"/instance[@class='SRM']/rowset[@name='SrmDefn']/row");
            for (var x = 0; x <= index; x++)
            {
                nodeIterator.MoveNext();
            }

            return nodeIterator.Current;
        }
    }
}
