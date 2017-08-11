namespace ImageUploader
{
    partial class UploadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.status = new System.Windows.Forms.Label();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_FilePathToUpload = new System.Windows.Forms.TextBox();
            this.button_Upload = new System.Windows.Forms.Button();
            this.button_TakeSnapshot = new System.Windows.Forms.Button();
            this.button_EditPicture = new System.Windows.Forms.Button();
            this.label_FileToSend = new System.Windows.Forms.Label();
            this.label_creditToSuperMath = new System.Windows.Forms.Label();
            this.checkBox_DeleteUploadedFile = new System.Windows.Forms.CheckBox();
            this.panel = new System.Windows.Forms.Panel();
            this.textBox_linkToUpload = new System.Windows.Forms.TextBox();
            this.label_UrlInstruction = new System.Windows.Forms.Label();
            this.radioButton_Image = new System.Windows.Forms.RadioButton();
            this.radioButton_Url = new System.Windows.Forms.RadioButton();
            this.button_SwitchServer = new System.Windows.Forms.Button();
            this.pictureBox_ServerImageLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ServerImageLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.status.Location = new System.Drawing.Point(78, 12);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(158, 18);
            this.status.TabIndex = 9;
            this.status.Text = "Uploading, please wait...";
            this.status.Visible = false;
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(313, 117);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(75, 23);
            this.button_Browse.TabIndex = 4;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // textBox_FilePathToUpload
            // 
            this.textBox_FilePathToUpload.AllowDrop = true;
            this.textBox_FilePathToUpload.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_FilePathToUpload.Location = new System.Drawing.Point(87, 117);
            this.textBox_FilePathToUpload.Name = "textBox_FilePathToUpload";
            this.textBox_FilePathToUpload.ReadOnly = true;
            this.textBox_FilePathToUpload.Size = new System.Drawing.Size(218, 20);
            this.textBox_FilePathToUpload.TabIndex = 3;
            this.textBox_FilePathToUpload.TabStop = false;
            this.textBox_FilePathToUpload.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileBox_DragDrop);
            this.textBox_FilePathToUpload.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileBox_DragEnter);
            // 
            // button_Upload
            // 
            this.button_Upload.Location = new System.Drawing.Point(99, 303);
            this.button_Upload.Name = "button_Upload";
            this.button_Upload.Size = new System.Drawing.Size(218, 59);
            this.button_Upload.TabIndex = 10;
            this.button_Upload.Text = "Upload!";
            this.button_Upload.UseVisualStyleBackColor = true;
            this.button_Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // button_TakeSnapshot
            // 
            this.button_TakeSnapshot.Location = new System.Drawing.Point(87, 9);
            this.button_TakeSnapshot.Name = "button_TakeSnapshot";
            this.button_TakeSnapshot.Size = new System.Drawing.Size(218, 66);
            this.button_TakeSnapshot.TabIndex = 1;
            this.button_TakeSnapshot.Text = "Take Screen Snapshot";
            this.button_TakeSnapshot.UseVisualStyleBackColor = true;
            this.button_TakeSnapshot.Click += new System.EventHandler(this.butSnapShot_Click);
            // 
            // button_EditPicture
            // 
            this.button_EditPicture.Enabled = false;
            this.button_EditPicture.Location = new System.Drawing.Point(313, 148);
            this.button_EditPicture.Name = "button_EditPicture";
            this.button_EditPicture.Size = new System.Drawing.Size(75, 23);
            this.button_EditPicture.TabIndex = 5;
            this.button_EditPicture.Text = "Edit Image";
            this.button_EditPicture.UseVisualStyleBackColor = true;
            this.button_EditPicture.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // label_FileToSend
            // 
            this.label_FileToSend.AutoSize = true;
            this.label_FileToSend.Location = new System.Drawing.Point(8, 120);
            this.label_FileToSend.Name = "label_FileToSend";
            this.label_FileToSend.Size = new System.Drawing.Size(70, 13);
            this.label_FileToSend.TabIndex = 8;
            this.label_FileToSend.Text = "File To Send:";
            // 
            // label_creditToSuperMath
            // 
            this.label_creditToSuperMath.AutoSize = true;
            this.label_creditToSuperMath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_creditToSuperMath.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label_creditToSuperMath.Location = new System.Drawing.Point(294, 389);
            this.label_creditToSuperMath.Name = "label_creditToSuperMath";
            this.label_creditToSuperMath.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_creditToSuperMath.Size = new System.Drawing.Size(118, 15);
            this.label_creditToSuperMath.TabIndex = 12;
            this.label_creditToSuperMath.Text = "Made by SuperMath";
            // 
            // checkBox_DeleteUploadedFile
            // 
            this.checkBox_DeleteUploadedFile.AutoSize = true;
            this.checkBox_DeleteUploadedFile.Location = new System.Drawing.Point(8, 87);
            this.checkBox_DeleteUploadedFile.Name = "checkBox_DeleteUploadedFile";
            this.checkBox_DeleteUploadedFile.Size = new System.Drawing.Size(191, 17);
            this.checkBox_DeleteUploadedFile.TabIndex = 2;
            this.checkBox_DeleteUploadedFile.Text = "Delete image after upload succeed";
            this.checkBox_DeleteUploadedFile.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.textBox_linkToUpload);
            this.panel.Controls.Add(this.label_UrlInstruction);
            this.panel.Controls.Add(this.button_TakeSnapshot);
            this.panel.Controls.Add(this.checkBox_DeleteUploadedFile);
            this.panel.Controls.Add(this.button_Browse);
            this.panel.Controls.Add(this.textBox_FilePathToUpload);
            this.panel.Controls.Add(this.button_EditPicture);
            this.panel.Controls.Add(this.label_FileToSend);
            this.panel.Location = new System.Drawing.Point(12, 105);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(397, 189);
            this.panel.TabIndex = 14;
            // 
            // textBox_linkToUpload
            // 
            this.textBox_linkToUpload.Enabled = false;
            this.textBox_linkToUpload.Location = new System.Drawing.Point(8, 55);
            this.textBox_linkToUpload.Name = "textBox_linkToUpload";
            this.textBox_linkToUpload.Size = new System.Drawing.Size(380, 20);
            this.textBox_linkToUpload.TabIndex = 9;
            this.textBox_linkToUpload.Visible = false;
            // 
            // label_UrlInstruction
            // 
            this.label_UrlInstruction.AutoSize = true;
            this.label_UrlInstruction.Enabled = false;
            this.label_UrlInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_UrlInstruction.Location = new System.Drawing.Point(8, 24);
            this.label_UrlInstruction.Name = "label_UrlInstruction";
            this.label_UrlInstruction.Size = new System.Drawing.Size(194, 17);
            this.label_UrlInstruction.TabIndex = 14;
            this.label_UrlInstruction.Text = "Copy here full URL to upload:";
            this.label_UrlInstruction.Visible = false;
            // 
            // radioButton_Image
            // 
            this.radioButton_Image.AutoSize = true;
            this.radioButton_Image.Checked = true;
            this.radioButton_Image.Location = new System.Drawing.Point(314, 83);
            this.radioButton_Image.Name = "radioButton_Image";
            this.radioButton_Image.Size = new System.Drawing.Size(91, 17);
            this.radioButton_Image.TabIndex = 15;
            this.radioButton_Image.TabStop = true;
            this.radioButton_Image.Text = "Upload Image";
            this.radioButton_Image.UseVisualStyleBackColor = true;
            this.radioButton_Image.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_Url
            // 
            this.radioButton_Url.AutoSize = true;
            this.radioButton_Url.Location = new System.Drawing.Point(212, 83);
            this.radioButton_Url.Name = "radioButton_Url";
            this.radioButton_Url.Size = new System.Drawing.Size(84, 17);
            this.radioButton_Url.TabIndex = 7;
            this.radioButton_Url.Text = "Upload URL";
            this.radioButton_Url.UseVisualStyleBackColor = true;
            this.radioButton_Url.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // button_SwitchServer
            // 
            this.button_SwitchServer.BackgroundImage = global::ImageUploader.Properties.Resources._switch;
            this.button_SwitchServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_SwitchServer.Location = new System.Drawing.Point(12, 371);
            this.button_SwitchServer.Name = "button_SwitchServer";
            this.button_SwitchServer.Size = new System.Drawing.Size(47, 34);
            this.button_SwitchServer.TabIndex = 17;
            this.button_SwitchServer.UseVisualStyleBackColor = true;
            this.button_SwitchServer.Click += new System.EventHandler(this.button_SwitchServer_Click);
            // 
            // pictureBox_ServerImageLogo
            // 
            this.pictureBox_ServerImageLogo.Location = new System.Drawing.Point(12, 7);
            this.pictureBox_ServerImageLogo.Name = "pictureBox_ServerImageLogo";
            this.pictureBox_ServerImageLogo.Size = new System.Drawing.Size(397, 77);
            this.pictureBox_ServerImageLogo.TabIndex = 16;
            this.pictureBox_ServerImageLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(21, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::ImageUploader.Properties.Resources.ajax_loader1;
            this.pictureBoxLoading.Location = new System.Drawing.Point(19, 8);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxLoading.TabIndex = 4;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // UploadForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 410);
            this.Controls.Add(this.button_SwitchServer);
            this.Controls.Add(this.pictureBox_ServerImageLogo);
            this.Controls.Add(this.radioButton_Url);
            this.Controls.Add(this.radioButton_Image);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.label_creditToSuperMath);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.button_Upload);
            this.Controls.Add(this.pictureBoxLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ServerImageLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.TextBox textBox_FilePathToUpload;
        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.Button button_TakeSnapshot;
        private System.Windows.Forms.Button button_EditPicture;
        private System.Windows.Forms.Label label_FileToSend;
        private System.Windows.Forms.Label label_creditToSuperMath;
        private System.Windows.Forms.CheckBox checkBox_DeleteUploadedFile;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.RadioButton radioButton_Image;
        private System.Windows.Forms.RadioButton radioButton_Url;
        private System.Windows.Forms.Label label_UrlInstruction;
        private System.Windows.Forms.TextBox textBox_linkToUpload;
        private System.Windows.Forms.PictureBox pictureBox_ServerImageLogo;
        private System.Windows.Forms.Button button_SwitchServer;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

