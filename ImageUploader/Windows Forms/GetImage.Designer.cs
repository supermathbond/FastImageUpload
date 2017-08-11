namespace ImageUploader
{
    partial class GetImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetImage));
            this.button_CopyDirectLink = new System.Windows.Forms.Button();
            this.textBox_ImageDirectUrl = new System.Windows.Forms.TextBox();
            this.label_DirectLink = new System.Windows.Forms.Label();
            this.button_CopyBBcodeLink = new System.Windows.Forms.Button();
            this.textBox_ImageBBcodeUrl = new System.Windows.Forms.TextBox();
            this.label_BBcode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_CopyDirectLink
            // 
            this.button_CopyDirectLink.Location = new System.Drawing.Point(498, 40);
            this.button_CopyDirectLink.Name = "button_CopyDirectLink";
            this.button_CopyDirectLink.Size = new System.Drawing.Size(74, 23);
            this.button_CopyDirectLink.TabIndex = 3;
            this.button_CopyDirectLink.Text = "Copy Text";
            this.button_CopyDirectLink.UseVisualStyleBackColor = true;
            this.button_CopyDirectLink.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_ImageDirectUrl
            // 
            this.textBox_ImageDirectUrl.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_ImageDirectUrl.Location = new System.Drawing.Point(91, 43);
            this.textBox_ImageDirectUrl.Name = "textBox_ImageDirectUrl";
            this.textBox_ImageDirectUrl.ReadOnly = true;
            this.textBox_ImageDirectUrl.Size = new System.Drawing.Size(401, 20);
            this.textBox_ImageDirectUrl.TabIndex = 1;
            this.textBox_ImageDirectUrl.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox_ImageDirectUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
            // 
            // label_DirectLink
            // 
            this.label_DirectLink.AutoSize = true;
            this.label_DirectLink.Location = new System.Drawing.Point(12, 46);
            this.label_DirectLink.Name = "label_DirectLink";
            this.label_DirectLink.Size = new System.Drawing.Size(61, 13);
            this.label_DirectLink.TabIndex = 3;
            this.label_DirectLink.Text = "Direct Link:";
            // 
            // button_CopyBBcodeLink
            // 
            this.button_CopyBBcodeLink.Location = new System.Drawing.Point(498, 87);
            this.button_CopyBBcodeLink.Name = "button_CopyBBcodeLink";
            this.button_CopyBBcodeLink.Size = new System.Drawing.Size(74, 23);
            this.button_CopyBBcodeLink.TabIndex = 4;
            this.button_CopyBBcodeLink.Text = "Copy Text";
            this.button_CopyBBcodeLink.UseVisualStyleBackColor = true;
            this.button_CopyBBcodeLink.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_ImageBBcodeUrl
            // 
            this.textBox_ImageBBcodeUrl.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_ImageBBcodeUrl.Location = new System.Drawing.Point(91, 87);
            this.textBox_ImageBBcodeUrl.Name = "textBox_ImageBBcodeUrl";
            this.textBox_ImageBBcodeUrl.ReadOnly = true;
            this.textBox_ImageBBcodeUrl.Size = new System.Drawing.Size(401, 20);
            this.textBox_ImageBBcodeUrl.TabIndex = 2;
            this.textBox_ImageBBcodeUrl.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox_ImageBBcodeUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
            // 
            // label_BBcode
            // 
            this.label_BBcode.AutoSize = true;
            this.label_BBcode.Location = new System.Drawing.Point(12, 90);
            this.label_BBcode.Name = "label_BBcode";
            this.label_BBcode.Size = new System.Drawing.Size(77, 13);
            this.label_BBcode.TabIndex = 7;
            this.label_BBcode.Text = "BBCODE Link:";
            // 
            // GetImage
            // 
            this.ClientSize = new System.Drawing.Size(595, 162);
            this.Controls.Add(this.label_BBcode);
            this.Controls.Add(this.textBox_ImageBBcodeUrl);
            this.Controls.Add(this.button_CopyBBcodeLink);
            this.Controls.Add(this.label_DirectLink);
            this.Controls.Add(this.textBox_ImageDirectUrl);
            this.Controls.Add(this.button_CopyDirectLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GetImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Link";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Button button_CopyDirectLink;
        private System.Windows.Forms.TextBox textBox_ImageDirectUrl;
        private System.Windows.Forms.Label label_DirectLink;
        private System.Windows.Forms.Button button_CopyBBcodeLink;
        private System.Windows.Forms.TextBox textBox_ImageBBcodeUrl;
        private System.Windows.Forms.Label label_BBcode;
    }
}