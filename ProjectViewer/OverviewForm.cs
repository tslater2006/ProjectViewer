using ProjectViewer.Details;
using ProjectViewer.Overview;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace ProjectViewer
{
    public partial class OverviewForm : Form
    {
        XPathNavigator project;
        int type;

        IOverviewHandler handler;

        public OverviewForm(XPathNavigator project, int type)
        {
            InitializeComponent();
            this.project = project;
            this.type = type;

            if (DefaultOverviewHandler.OverviewMap.ContainsKey(type))
            {
                handler = DefaultOverviewHandler.OverviewMap[type];
            }
            else
            {
                handler = new DefaultOverviewHandler();
            }

            this.Text = handler.GetTitle(type);

            InitColumns(type);

            LoadRows(type);
        }

        private void InitColumns(int type)
        {


            List<string> headers = handler.GetHeaders(type);
            headers.Reverse();
            foreach(var header in headers)
            {
                DataGridViewTextBoxColumn textCol = new DataGridViewTextBoxColumn();
                textCol.HeaderText = header;
                dataGridView1.Columns.Insert(0, textCol);
            }

        }
        private void GetValuesForType(List<object>valueHolder, int type, XPathNavigator currentItem)
        {
            List<string> values = handler.GetValues(type, currentItem);
            foreach (var value in values)
            {
                valueHolder.Add(value);
            }
        }
        private void LoadRows(int type)
        {
            var items = project.Select($"/instance[@class='PJM']/rowset[@name='PjmDefn']/row/lpPit/rowset[@name='PjmPit']/row[eObjectType={type}]");
            if (items.Count > 0)
            {
                while (items.MoveNext())
                {
                    List<object> rowValues = new List<object>();

                    GetValuesForType(rowValues,type, items.Current);

                    var sourceStatus = items.Current.SelectSingleNode("eSourceStatus").ValueAsInt;
                    var targetStatus = items.Current.SelectSingleNode("eTargetStatus").ValueAsInt;
                    var upgradeAction = items.Current.SelectSingleNode("eUpgradeAction").ValueAsInt;
                    var takeAction = items.Current.SelectSingleNode("bTakeAction").ValueAsInt;
                    var copyDone = items.Current.SelectSingleNode("bCopyDone").ValueAsInt;

                    switch(sourceStatus)
                    {
                        case 0:
                            rowValues.Add("Unknown");
                            break;
                        default:
                            rowValues.Add(sourceStatus.ToString());
                            break;
                    }

                    switch(targetStatus)
                    {
                        case 0:
                            rowValues.Add("Unknown");
                            break;
                        default:
                            rowValues.Add(targetStatus.ToString());
                            break;
                    }

                    switch(upgradeAction)
                    {
                        case 0:
                            rowValues.Add("Copy");
                            break;
                        case 1:
                            rowValues.Add("Delete");
                            break;
                        default:
                            rowValues.Add(upgradeAction.ToString());
                            break;
                    }

                    rowValues.Add((takeAction == 1));
                    rowValues.Add((copyDone == 1));

                    dataGridView1.Rows.Add(rowValues.ToArray());
                }

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }

        private void OverviewForm_MouseDown(object sender, MouseEventArgs e)
        {
            BringToFront();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ViewManager.ShowDetails(handler, project, type, e.RowIndex);
            }
            
        }
    }
}
