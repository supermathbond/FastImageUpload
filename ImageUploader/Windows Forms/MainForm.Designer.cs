namespace ImageUploader
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label_ChooseServer = new System.Windows.Forms.Label();
            this.label_SuperMathCredit = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox_UpdateButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UpdateButton)).BeginInit();
            this.SuspendLayout();
            // 
            // label_ChooseServer
            // 
            this.label_ChooseServer.AutoSize = true;
            this.label_ChooseServer.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_ChooseServer.Location = new System.Drawing.Point(227, 19);
            this.label_ChooseServer.Name = "label_ChooseServer";
            this.label_ChooseServer.Size = new System.Drawing.Size(149, 29);
            this.label_ChooseServer.TabIndex = 1;
            this.label_ChooseServer.Text = "Pick Server:";
            // 
            // label_SuperMathCredit
            // 
            this.label_SuperMathCredit.AutoSize = true;
            this.label_SuperMathCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_SuperMathCredit.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label_SuperMathCredit.Location = new System.Drawing.Point(12, 9);
            this.label_SuperMathCredit.Name = "label_SuperMathCredit";
            this.label_SuperMathCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_SuperMathCredit.Size = new System.Drawing.Size(118, 15);
            this.label_SuperMathCredit.TabIndex = 13;
            this.label_SuperMathCredit.Text = "Made by SuperMath";
            this.label_SuperMathCredit.Visible = false;
            // 
            // pictureBox_UpdateButton
            // 
            this.pictureBox_UpdateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_UpdateButton.Image = global::ImageUploader.Properties.Resources.update_Image;
            this.pictureBox_UpdateButton.Location = new System.Drawing.Point(15, 6);
            this.pictureBox_UpdateButton.Name = "pictureBox_UpdateButton";
            this.pictureBox_UpdateButton.Size = new System.Drawing.Size(52, 52);
            this.pictureBox_UpdateButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_UpdateButton.TabIndex = 14;
            this.pictureBox_UpdateButton.TabStop = false;
            this.pictureBox_UpdateButton.Tag = "0";
            this.pictureBox_UpdateButton.Click += new System.EventHandler(this.pictureBox_UpdateButton_Click);
            this.pictureBox_UpdateButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_UpdateButton_MouseDown);
            this.pictureBox_UpdateButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_UpdateButton_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 72);
            this.Controls.Add(this.label_ChooseServer);
            this.Controls.Add(this.label_SuperMathCredit);
            this.Controls.Add(this.pictureBox_UpdateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FastImageUpload!";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UpdateButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ChooseServer;
        private System.Windows.Forms.Label label_SuperMathCredit;
        private System.Windows.Forms.PictureBox pictureBox_UpdateButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}