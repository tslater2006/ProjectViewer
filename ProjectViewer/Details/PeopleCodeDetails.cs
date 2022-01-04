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
    public partial class PeopleCodeDetails : Form, IDetailsForm
    {
        public PeopleCodeDetails()
        {
            InitializeComponent();
            ConfigureCodeFolding();
        }

        public void InitDetails(XPathNavigator project, int type, int index)
        {
            var projItem = GetProjectItem(project, type, index);

            List<string> projectItemKeys = new List<string>();

            for (var x = 0; x < 7; x++)
            {
                var currentValue = projItem.SelectSingleNode("szObjectValue_" + x);
                if (currentValue != null && currentValue.Value.Length > 0)
                {
                    projectItemKeys.Add(currentValue.Value);
                }
            }
            //TODO: Handle App Engine PPC keys since 3 of them are stored in _1 in the project :/

            StringBuilder pcmXpath = new StringBuilder("/instance[@class='PCM']/rowset[@name='PcmProg']/row[");

            for (var x = 0; x < projectItemKeys.Count; x++)
            {
                pcmXpath.Append("(szObjectValue_" + x + "='" + projectItemKeys[x] + "')");
                if (x + 1 < projectItemKeys.Count)
                {
                    pcmXpath.Append(" and ");
                }
            }
            pcmXpath.Append("]/../..");
            var pcmItem = project.SelectSingleNode(pcmXpath.ToString());

            var ppcText = pcmItem.SelectSingleNode("peoplecode_text").Value;

            this.Text = String.Join(":", projectItemKeys);
            simpleEditor1.ReadOnly = false;
            simpleEditor1.Text = ppcText;
            simpleEditor1.ReadOnly = true;
            simpleEditor1.Margins[0].Width = 20;
            // ProcessFolds();

        }

        private void ConfigureCodeFolding()
        {
            simpleEditor1.Margins[2].Type = MarginType.Symbol;
            simpleEditor1.Margins[2].Mask = Marker.MaskFolders;
            simpleEditor1.Margins[2].Sensitive = true;
            simpleEditor1.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                simpleEditor1.Markers[i].SetForeColor(SystemColors.Control);
                simpleEditor1.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            simpleEditor1.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            simpleEditor1.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            simpleEditor1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            simpleEditor1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            simpleEditor1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            simpleEditor1.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            simpleEditor1.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;

            simpleEditor1.SetFoldFlags(FoldFlags.LineAfterContracted);

            simpleEditor1.MarginClick += (s, mcea) =>
            {
                // Toggle the fold when clicking
                var line = simpleEditor1.LineFromPosition(mcea.Position);
                simpleEditor1.Lines[line].ToggleFold();
            };
        }

        private void ProcessFolds()
        {
            var foldLevel = simpleEditor1.Lines[0].FoldLevel;

            List<string> beginFold = new List<string>() { "class","If","While","Evaluate","For" };
            List<string> endFold = new List<string>() { "end-class", "End-If", "End-While", "End-Evaluate", "End-For" };

            for (var x = 0; x < simpleEditor1.Lines.Count; x++)
            {
                var trimmedLine = simpleEditor1.Lines[x].Text.Trim();
                var firstWord = trimmedLine.Replace(";","").Split(' ')[0];

                if (beginFold.Contains(firstWord))
                {
                    simpleEditor1.Lines[x].FoldLevelFlags = FoldLevelFlags.Header;
                    simpleEditor1.Lines[x].FoldLevel = foldLevel++;
                } else if (endFold.Contains(firstWord))
                {
                    simpleEditor1.Lines[x].FoldLevel = foldLevel--;
                } else
                {
                    simpleEditor1.Lines[x].FoldLevel = foldLevel;
                }
            }

        }

        private XPathNavigator GetProjectItem(XPathNavigator project, int type, int index)
        {
            return project.SelectSingleNode($"/instance[@class='PJM']/rowset[@name='PjmDefn']/row/lpPit/rowset[@name='PjmPit']/row[eObjectType={type}][{index + 1}]");
        }
    }
}
