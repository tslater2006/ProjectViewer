using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace ProjectViewer
{
    public partial class MainWindow : Form
    {
        public ProjectOutline outlineForm;
        public static MainWindow Instance;
        public static void SetStatusBar(string text)
        {
            if (Instance != null)
            {
                Instance.toolStripStatusLabel.Text = text;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            /* Create Project Outline Form */
            outlineForm = new ProjectOutline
            {
                TopLevel = false,
                // outlineForm.MdiParent = this;
                Parent = this.splitContainer1.Panel1,
                Dock = DockStyle.Fill
            };

            outlineForm.Show();
            ViewManager.mainWindow = this;
            ViewManager.mdiPanel = splitContainer1.Panel2;
            Instance = this;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Project XML Files|*.xml";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                XPathDocument doc;
                using (XmlReader xr = XmlReader.Create(fileName, new XmlReaderSettings(){ConformanceLevel = ConformanceLevel.Fragment}))
                {
                    doc = new XPathDocument(xr);
                }

                // new create XPathNavigator for read out data e.g.
                XPathNavigator nav = doc.CreateNavigator();

                outlineForm.LoadProjectTree(nav);
                sw.Stop();
                SetStatusBar($"Project loaded in: {sw.Elapsed.TotalMilliseconds}ms.");
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

    }
}
