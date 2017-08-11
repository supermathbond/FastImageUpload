namespace ImageUploader
{
    partial class MyMessageBox
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
            this.label_MessageString = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_link = new System.Windows.Forms.TextBox();
            this.linkLabel_OpenTextBox = new System.Windows.Forms.LinkLabel();
            this.textBox_VersionsImprovements = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_MessageString
            // 
            this.label_MessageString.AutoSize = true;
            this.label_MessageString.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_MessageString.Location = new System.Drawing.Point(12, 33);
            this.label_MessageString.Name = "label_MessageString";
            this.label_MessageString.Size = new System.Drawing.Size(0, 29);
            this.label_MessageString.TabIndex = 0;
            this.label_MessageString.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(192, 124);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // textBox_link
            // 
            this.textBox_link.Location = new System.Drawing.Point(12, 87);
            this.textBox_link.Name = "textBox_link";
            this.textBox_link.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_link.Size = new System.Drawing.Size(430, 20);
            this.textBox_link.TabIndex = 3;
            this.textBox_link.Enter += new System.EventHandler(this.textBox_link_Enter);
            this.textBox_link.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_link_MouseDown);
            // 
            // linkLabel_OpenTextBox
            // 
            this.linkLabel_OpenTextBox.AutoSize = true;
            this.linkLabel_OpenTextBox.Location = new System.Drawing.Point(14, 110);
            this.linkLabel_OpenTextBox.Name = "linkLabel_OpenTextBox";
            this.linkLabel_OpenTextBox.Size = new System.Drawing.Size(103, 13);
            this.linkLabel_OpenTextBox.TabIndex = 4;
            this.linkLabel_OpenTextBox.TabStop = true;
            this.linkLabel_OpenTextBox.Text = "Show Improvements";
            this.linkLabel_OpenTextBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_OpenTextBox_LinkClicked);
            // 
            // textBox_VersionsImprovements
            // 
            this.textBox_VersionsImprovements.Enabled = false;
            this.textBox_VersionsImprovements.Location = new System.Drawing.Point(12, 149);
            this.textBox_VersionsImprovements.Multiline = true;
            this.textBox_VersionsImprovements.Name = "textBox_VersionsImprovements";
            this.textBox_VersionsImprovements.ReadOnly = true;
            this.textBox_VersionsImprovements.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_VersionsImprovements.Size = new System.Drawing.Size(430, 137);
            this.textBox_VersionsImprovements.TabIndex = 5;
            this.textBox_VersionsImprovements.Visible = false;
            // 
            // MyMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 161);
            this.Controls.Add(this.textBox_VersionsImprovements);
            this.Controls.Add(this.linkLabel_OpenTextBox);
            this.Controls.Add(this.textBox_link);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_MessageString);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyMessageBox_FormClosed);
            this.Load += new System.EventHandler(this.MyMessageBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_MessageString;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_link;
        private System.Windows.Forms.LinkLabel linkLabel_OpenTextBox;
        private System.Windows.Forms.TextBox textBox_VersionsImprovements;
    }
}