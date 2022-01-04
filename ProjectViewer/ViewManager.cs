using ProjectViewer.Details;
using ProjectViewer.Overview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace ProjectViewer
{
    public class ViewManager
    {
        public static Dictionary<int, OverviewForm> OverviewMaps = new Dictionary<int, OverviewForm>();
        public static Dictionary<int, IDetailsForm> DetailsMaps = new Dictionary<int, IDetailsForm>();
        public static Panel mdiPanel;
        public static MainWindow mainWindow;
        public static void ShowOverview(XPathNavigator project, int type)
        {

            if (OverviewMaps.ContainsKey(type))
            {
                if (OverviewMaps[type].IsDisposed == false)
                {
                    OverviewMaps[type].BringToFront();
                    OverviewMaps[type].Focus();
                }
                else
                {
                    OverviewMaps.Remove(type);
                    CreateNewForm(project, type);
                }
            }
            else
            {
                CreateNewForm(project, type);
            }
        }
        private static void CreateDetailsForm(IOverviewHandler handler, XPathNavigator project, int type, int index)
        {
            var detailForm = handler.GetDetailsForm(type);
            var formCast = detailForm as Form;

            if (detailForm == null)
            {
                MessageBox.Show("Viewing of " + handler.GetTitle(type) + " definitions is not currently supported.");
            }
            else
            {
                formCast.TopLevel = false;
                formCast.MdiParent = mainWindow;
                DetailsMaps.Add(type, detailForm);
                mdiPanel.Controls.Add(formCast);

                detailForm.InitDetails(project, type, index);

                formCast.Show();
                formCast.BringToFront();
            }
        }
        public static void ShowDetails(IOverviewHandler handler, XPathNavigator project, int type, int index)
        {
            if (DetailsMaps.ContainsKey(type))
            {
                var detailForm = DetailsMaps[type] as Form;

                if (detailForm.IsDisposed == false)
                {
                    detailForm.BringToFront();
                    detailForm.Focus();
                    DetailsMaps[type].InitDetails(project, type, index);
                }
                else
                {
                    DetailsMaps.Remove(type);
                    CreateDetailsForm(handler, project, type, index);
                }
            }
            else
            {
                CreateDetailsForm(handler, project, type, index);
            }

        }

        private static void CreateNewForm(XPathNavigator project, int type)
        {
            var f = new OverviewForm(project, type);
            f.TopLevel = false;
            f.MdiParent = mainWindow;
            OverviewMaps.Add(type, f);
            mdiPanel.Controls.Add(f);

            f.Show();
            f.BringToFront();
        }
    }
}
