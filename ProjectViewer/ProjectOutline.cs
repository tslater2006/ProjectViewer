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

namespace ProjectViewer
{
    public class LoadedOutline
    {
        public XPathNavigator Project;
        public TreeNode rootNode;
        public TreeNode PCNode;
        public Dictionary<int, TreeNode> CRMNodes = new Dictionary<int, TreeNode>();
        public Dictionary<int, TreeNode> PCNodes = new Dictionary<int, TreeNode>();
        public List<int> seenTypes = new List<int>();
    }
    public partial class ProjectOutline : Form
    {
        List<LoadedOutline> Outlines = new List<LoadedOutline>();

        public ProjectOutline()
        {
            InitializeComponent();

            treeView1.TreeViewNodeSorter = new NodeSorter();
        }

        public void LoadProjectTree(XPathNavigator project)
        {
            if (Outlines.Where(o => o.Project == project).Count() == 1)
            {
                return;
            }

            var outline = new LoadedOutline();
            outline.Project = project;
            Outlines.Add(outline);

            var projectName = project.SelectSingleNode("/instance[@class='PJM']/rowset[@name='PjmDefn']/row/szProjectName").Value;

            HashSet<string> itemTypes = new HashSet<string>();
            var iterator = project.Select("//instance[@class!='PJM']");

            if (iterator.Count > 0)
            {
                //begin to loop through the titles and begin to display them
                while (iterator.MoveNext())
                {
                    var type = iterator.Current.GetAttribute("class", "");
                    itemTypes.Add(type);
                }
            }

            outline.rootNode = treeView1.Nodes.Add(projectName);
            outline.rootNode.Tag = outline;
            var projectItems = project.Select("/instance[@class='PJM']/rowset[@name='PjmDefn']/row/lpPit/rowset[@name='PjmPit']/row/eObjectType");
            if (projectItems.Count > 0)
            {
                while (projectItems.MoveNext())
                {
                    var objectType = projectItems.Current.ValueAsInt;
                    var typeString = objectType.ToString();
                    var isTypePPC = false;
                    if (outline.seenTypes.Contains(objectType) == false)
                    {

                        switch (objectType)
                        {
                            case 0:
                                typeString = "Records";
                                break;
                            case 1:
                                typeString = "Indexes";
                                break;
                            case 2:
                                typeString = "Fields";
                                break;
                            case 3:
                                typeString = "Field Formats";
                                break;
                            case 4:
                                typeString = "Translate Values";
                                break;
                            case 5:
                                typeString = "Pages";
                                break;
                            case 6:
                                typeString = "Menus";
                                break;
                            case 7:
                                typeString = "Components";
                                break;
                            case 8:
                                typeString = "Record PeopleCode";
                                isTypePPC = true;
                                break;
                            case 9:
                                typeString = "Menu PeopleCode";
                                isTypePPC = true;
                                break;
                            case 10:
                                typeString = "Queries";
                                break;
                            case 11:
                                typeString = "Tree Structures";
                                break;
                            case 12:
                                typeString = "Trees";
                                break;
                            case 13:
                                typeString = "Access Groups";
                                break;
                            case 14:
                                typeString = "Colors";
                                break;
                            case 15:
                                typeString = "Styles";
                                break;
                            case 16:
                                typeString = "Business Process Maps";
                                break;
                            case 17:
                                typeString = "Business Processes";
                                break;
                            case 18:
                                typeString = "Activities";
                                break;
                            case 19:
                                typeString = "Roles";
                                break;
                            case 20:
                                typeString = "Process Definitions";
                                break;
                            case 21:
                                typeString = "Process Server Definitions";
                                break;
                            case 22:
                                typeString = "Process Type Definitions";
                                break;
                            case 23:
                                typeString = "Process Jobs";
                                break;
                            case 24:
                                typeString = "Process Recurrences";
                                break;
                            case 25:
                                typeString = "Message Catalogs";
                                break;
                            case 26:
                                typeString = "Cube Dimensions";
                                break;
                            case 27:
                                typeString = "Cube Definitions";
                                break;
                            case 28:
                                typeString = "Cube Instance Definitions";
                                break;
                            case 29:
                                typeString = "Business Interlinks";
                                break;
                            case 30:
                                typeString = "SQL";
                                break;
                            case 31:
                                typeString = "File Layout Definitions";
                                break;
                            case 32:
                                typeString = "Component Interfaces";
                                break;
                            case 33:
                                typeString = "Application Engine Programs";
                                break;
                            case 34:
                                typeString = "Application Engine Sections";
                                break;
                            case 35:
                                typeString = "Message Nodes";
                                break;
                            case 36:
                                typeString = "Message Channels";
                                break;
                            case 37:
                                typeString = "Messages";
                                break;
                            case 38:
                                typeString = "Approval Rule Sets";
                                break;
                            case 39:
                                typeString = "Message PeopleCode";
                                isTypePPC = true;
                                break;
                            case 40:
                                typeString = "Subscription PeopleCode";
                                isTypePPC = true;
                                break;
                            case 42:
                                typeString = "Component Interface PeopleCode";
                                isTypePPC = true;
                                break;
                            case 43:
                                typeString = "Application Engine PeopleCode";
                                isTypePPC = true;
                                break;
                            case 44:
                                typeString = "Page PeopleCode";
                                isTypePPC = true;
                                break;
                            case 45:
                                typeString = "Page Field PeopleCode";
                                isTypePPC = true;
                                break;
                            case 46:
                                typeString = "Component PeopleCode";
                                isTypePPC = true;
                                break;
                            case 47:
                                typeString = "Component Record PeopleCode";
                                isTypePPC = true;
                                break;
                            case 48:
                                typeString = "Component Rec Field PeopleCode";
                                isTypePPC = true;
                                break;
                            case 49:
                                typeString = "Images";
                                break;
                            case 50:
                                typeString = "Style Sheets";
                                break;
                            case 51:
                                typeString = "HTML";
                                break;
                            case 52:
                                typeString = "File Reference Objects";
                                break;
                            case 53:
                                typeString = "Permission Lists";
                                break;
                            case 54:
                                typeString = "Portal Registry Definitions";
                                break;
                            case 55:
                                typeString = "Portal Registry Structures";
                                break;
                            case 56:
                                typeString = "URL Definitions";
                                break;
                            case 57:
                                typeString = "Application Packages";
                                break;
                            case 58:
                                typeString = "Application Package PeopleCode";
                                isTypePPC = true;
                                break;
                            case 59:
                                typeString = "Portal Registry User Homepage";
                                isTypePPC = true;
                                break;
                            case 60:
                                typeString = "Analytic Types";
                                break;
                            case 61:
                                typeString = "Archive Templates";
                                break;
                            case 62:
                                typeString = "XSLT";
                                break;
                            case 63:
                                typeString = "Portal Registry User Favorites";
                                break;
                            case 64:
                                typeString = "Mobile Pages";
                                break;
                            case 65:
                                typeString = "Relationships";
                                break;
                            case 66:
                                typeString = "Comp Intfc Property PeopleCode";
                                isTypePPC = true;
                                break;
                            case 67:
                                typeString = "Optimization Models";
                                break;
                            case 68:
                                typeString = "File References";
                                break;
                            case 69:
                                typeString = "File Type Codes";
                                break;
                            case 70:
                                typeString = "Archive Object Definitions";
                                break;
                            case 71:
                                typeString = "Archive Template (Type 2)";
                                break;
                            case 72:
                                typeString = "Diagnostic Plug Ins";
                                break;
                            case 73:
                                typeString = "Analytic Models";
                                break;
                            case 75:
                                typeString = "Java Portlet User Preferences";
                                break;
                            case 76:
                                typeString = "WSRP Remote Producers";
                                break;
                            case 77:
                                typeString = "WSRP Remote Portlets";
                                break;
                            case 78:
                                typeString = "WSRP Cloned Portlet Handles";
                                break;
                            case 79:
                                typeString = "Services";
                                break;
                            case 80:
                                typeString = "Service Operations";
                                break;
                            case 81:
                                typeString = "Service Operation Handlers";
                                break;
                            case 82:
                                typeString = "Service Operation Versions";
                                break;
                            case 83:
                                typeString = "Service Operation Routings";
                                break;
                            case 84:
                                typeString = "IB Queues";
                                break;
                            case 85:
                                typeString = "XMLP Template Definitions";
                                break;
                            case 86:
                                typeString = "XMLP Report Definitions";
                                break;
                            case 87:
                                typeString = "XMLP File Definitions";
                                break;
                            case 88:
                                typeString = "XMLP Data Source Definitions";
                                break;
                            case 89:
                                typeString = "WSDLs";
                                break;
                            case 90:
                                typeString = "Message Schemas";
                                break;
                            case 91:
                                typeString = "Connected Query Definitions";
                                break;
                            case 92:
                                typeString = "Documents";
                                break;
                            case 93:
                                typeString = "XML Documents";
                                break;
                            case 94:
                                typeString = "Related Documents";
                                break;
                            case 95:
                                typeString = "Dependency Documentss";
                                break;
                            case 96:
                                typeString = "Dcoument Schemas";
                                break;
                            case 97:
                                typeString = "Essbase Cube Dimensions";
                                break;
                            case 98:
                                typeString = "Essbase Cube Outlines";
                                break;
                            case 99:
                                typeString = "Essbase Cube Connections";
                                break;
                            case 100:
                                typeString = "Essbase Cube Templates";
                                break;
                            case 101:
                                typeString = "Delimited Documents";
                                break;
                            case 102:
                                typeString = "Positional Documents";
                                break;
                            case 103:
                                typeString = "Application Data Set Defns";
                                break;
                            case 104:
                                typeString = "Tests";
                                break;
                            case 105:
                                typeString = "Test Cases";
                                break;
                            case 106:
                                typeString = "DMW Data Sets";
                                break;
                            case 107:
                                typeString = "Feed Definitions";
                                break;
                            case 108:
                                typeString = "Feed Categories";
                                break;
                            case 109:
                                typeString = "Feed Data Types";
                                break;
                            case 110:
                                typeString = "JSON Documents";
                                break;
                            case 111:
                                typeString = "Related Content Definitions";
                                break;
                            case 112:
                                typeString = "Related Content Services";
                                break;
                            case 113:
                                typeString = "Related Content Configurations";
                                break;
                            case 114:
                                typeString = "Related Content Layouts";
                                break;
                            case 115:
                                typeString = "Search Attributes";
                                break;
                            case 116:
                                typeString = "Search Definitions";
                                break;
                            case 117:
                                typeString = "Search Categorys";
                                break;
                            case 118:
                                typeString = "Search Contexts";
                                break;
                            case 119:
                                typeString = "Integration Groups";
                                break;
                            case 120:
                                typeString = "XML Documents";
                                break;
                        }
                        typeString += " (" + objectType + ")";
                        outline.seenTypes.Add(objectType);
                        if (isTypePPC)
                        {
                            if (outline.PCNode == null)
                            {
                                outline.PCNode = outline.rootNode.Nodes.Add("PeopleCode");
                            }

                            outline.PCNode.Nodes.Add(objectType.ToString(), typeString);
                        } else
                        {
                            outline.rootNode.Nodes.Add(objectType.ToString(), typeString);
                        }
                    }
                }
            }


            outline.rootNode.Expand();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var treeNode = e.Node;
            var parentNode = e.Node.Parent;
            if (parentNode == null)
            {
                return;
            }
            while (parentNode.Parent != null)
            {
                parentNode = parentNode.Parent;
            }

            if (e.Node.Name != null && e.Node.Name.Length > 0)
            {
                ViewManager.ShowOverview(((LoadedOutline)parentNode.Tag).Project, int.Parse(e.Node.Name));
            }
            
        }
    }

    public class NodeSorter : System.Collections.IComparer
    {
        // Compare the length of the strings, or the strings
        // themselves, if they are the same length.
        public int Compare(object x, object y)
        {
            var tx = x as TreeNode;
            var ty = y as TreeNode;
            // If they are the same length, call Compare.
            return string.Compare(tx.Text, ty.Text);
        }
    }
}
