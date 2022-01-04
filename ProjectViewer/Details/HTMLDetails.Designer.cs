namespace ProjectViewer.Details
{
    partial class HTMLDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleEditor1 = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // simpleEditor1
            // 
            this.simpleEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleEditor1.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.simpleEditor1.Lexer = ScintillaNET.Lexer.Html;
            this.simpleEditor1.Location = new System.Drawing.Point(0, 0);
            this.simpleEditor1.Name = "simpleEditor1";
            this.simpleEditor1.ReadOnly = true;
            this.simpleEditor1.Size = new System.Drawing.Size(632, 527);
            //this.simpleEditor1.Styler = null;
            this.simpleEditor1.TabIndex = 1;
            // 
            // HTMLDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 527);
            this.Controls.Add(this.simpleEditor1);
            this.Name = "HTMLDetails";
            this.Text = "HTMLDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla simpleEditor1;
    }
}