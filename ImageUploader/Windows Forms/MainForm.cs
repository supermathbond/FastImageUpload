using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

// http://www.findimagehost.com/image-hosting-all.php
// http://imgur.com/
// http://www.imgland.net/
// http://www.gonrad.com/
// http://www.abload.de/
// http://bayimg.com/

namespace ImageUploader
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// File path to upload.
        /// </summary>
        private string m_FilePath = "";

        /// <summary>
        /// Url to upload.
        /// </summary>
        private string m_Url = "";

        /// <summary>
        /// Sets if the file will be deleted after upload process was finished.
        /// </summary>
        private bool m_IsFileWillBeDeletedAfterUploading = false;

        /// <summary>
        /// The upload form.
        /// </summary>
        UploadForm m_UploadForm = new UploadForm();
        
        /// <summary>
        /// Constructor of the form
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            m_UploadForm.OnUserSwitchServer += new UploadForm.UserSwitchServer(UploadForm_OnUserSwitchServer);

            // Set a tool tip in the "check for updates pictureBox".
            toolTip.SetToolTip(pictureBox_UpdateButton, "Check for updates");
        }


        /// <summary>
        /// When the form is load, this method is called.
        /// This method draw all the buttons of the servers.
        /// </summary>
        /// <param name="sender"> The form. </param>
        /// <param name="e"> Useful parameters. </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Consts for ordering the form.
            const int Y_LOCATION_OF_UPDATE_BUTTON = 6;
            
            const int NUMBER_OF_BUTTON_COLUMNS = 3;
            const int HEIGHT_OF_BUTTON = 75;
            const int WIDTH_OF_BUTTON = 140;
            const int HEIGHT_SPACE_BETWEEN_BUTTONS = 23;
            const int WIDTH_SPACE_BETWEEN_BUTTONS = 30;
            const int WIDTH_OF_FORM_UNTIL_PANEL = 30;
            const int WIDTH_OF_FORM_AFTER_PANEL = 20;
            const int VERTICAL_DISTANCE_FROM_UPDATE_BUTTON_TO_PANEL = 15;
            const int MAX_NUMBER_OF_ROWS_IN_PANEL = 4;

            const int HORIZONAL_DISTANCE_OF_CREDIT_LABEL_FROM_DOWN_RIGHT_FORM_POINT = 10;
            const int VERTICAL_DISTANCE_OF_CREDIT_LABEL_FROM_DOWN_RIGHT_PANEL = 5;
            const int VERTICAL_DISTANCE_AFTER_CREDIT_LABEL = 50;

            ListOfServerProperties listServers = ListOfServerProperties.Instance;

            int RealNumberOfRows = (int) Math.Ceiling(
                ((double) listServers.NumberOfServers / NUMBER_OF_BUTTON_COLUMNS));
            int NumberOfRows = (RealNumberOfRows > MAX_NUMBER_OF_ROWS_IN_PANEL) ?
                                    MAX_NUMBER_OF_ROWS_IN_PANEL : RealNumberOfRows;

            // Panel with buttons.

            const int X_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON = 10;
            const int X_ADDITIONAL_SIZE_FOR_PANEL_AFTER_LAST_BUTTON = 30; // Must be at least double than the X_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON.
            const int Y_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON = 10;
            const int Y_ADDITIONAL_SIZE_FOR_PANEL_AFTER_LAST_BUTTON = 10;

            int startOfPanelX = WIDTH_OF_FORM_UNTIL_PANEL -
                X_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON;
            int StartOfPanelY = Y_LOCATION_OF_UPDATE_BUTTON
                + pictureBox_UpdateButton.Height + 
                VERTICAL_DISTANCE_FROM_UPDATE_BUTTON_TO_PANEL -
                Y_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON;
            int WidthOfPanel = X_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON + 
                ((NUMBER_OF_BUTTON_COLUMNS - 1) *
                WIDTH_SPACE_BETWEEN_BUTTONS) + (NUMBER_OF_BUTTON_COLUMNS * WIDTH_OF_BUTTON)
                + X_ADDITIONAL_SIZE_FOR_PANEL_AFTER_LAST_BUTTON;
            int HeightOfPanel = Y_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON + 
                ((NumberOfRows - 1) * HEIGHT_SPACE_BETWEEN_BUTTONS) +
                (NumberOfRows * HEIGHT_OF_BUTTON) +
                Y_ADDITIONAL_SIZE_FOR_PANEL_AFTER_LAST_BUTTON;

            // Create the panel.
            Panel panel = new Panel();
            panel.Location = new Point(startOfPanelX, StartOfPanelY);
            panel.Size = new Size(WidthOfPanel, HeightOfPanel);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.AutoScroll = true;

            this.Controls.Add(panel);

            for (int i = 0; i < listServers.NumberOfServers; i++)
            {
                // Saves the current column.
                int column = i % NUMBER_OF_BUTTON_COLUMNS;

                // Saves the current row.
                int row = i / NUMBER_OF_BUTTON_COLUMNS;

                // draw all buttons
                Button but = new Button();
                but.Size = new Size(WIDTH_OF_BUTTON, HEIGHT_OF_BUTTON);
                but.Location = new Point(X_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON
                    + column * (WIDTH_OF_BUTTON + WIDTH_SPACE_BETWEEN_BUTTONS),
                    Y_ADDITIONAL_SIZE_FOR_PANEL_UNTIL_FIRST_BUTTON + row *
                    (HEIGHT_OF_BUTTON + HEIGHT_SPACE_BETWEEN_BUTTONS));
                
                but.BackgroundImage = listServers.GetBitmapButton(i);
                but.BackgroundImageLayout = ImageLayout.Stretch;
                but.UseVisualStyleBackColor = true;
                but.TabIndex = i;
                but.Tag = i; // important: this is according the ENUM
                but.Click += new System.EventHandler(this.button_Click);

                // Set a tool tip for button.
                toolTip.SetToolTip(but, listServers.GetURL(i));

                // Adds button to form.
                panel.Controls.Add(but);

                // Set the minimum size of the form to its current size.
                this.MinimumSize = new Size(this.Width, this.Height);
            }

            // Set form width.
            this.Width = panel.Width +
                WIDTH_OF_FORM_UNTIL_PANEL + WIDTH_OF_FORM_AFTER_PANEL;

            // Credit label location.
            label_SuperMathCredit.Location = new Point(
                this.ClientRectangle.Width - HORIZONAL_DISTANCE_OF_CREDIT_LABEL_FROM_DOWN_RIGHT_FORM_POINT
                - label_SuperMathCredit.Width,
                panel.Location.Y + panel.Height +
                VERTICAL_DISTANCE_OF_CREDIT_LABEL_FROM_DOWN_RIGHT_PANEL);
            label_SuperMathCredit.Visible = true;

            // Change label of "Pick Server" location.
            label_ChooseServer.Location = new Point(startOfPanelX, //DISTANCE_OF_LABEL_FROM_LEFT_EDGE,
                //this.ClientRectangle.Width - DISTANCE_OF_LABEL_FROM_RIGHT_EDGE- label_ChooseServer.Width,
                label_ChooseServer.Location.Y);

            // Set pictureBox_UpdateButton Location.
            pictureBox_UpdateButton.Location = new Point(startOfPanelX + panel.Width - pictureBox_UpdateButton.Width,//this.ClientRectangle.Width -
                //DISTANCE_OF_LABEL_FROM_RIGHT_EDGE - pictureBox_UpdateButton.Width,
                Y_LOCATION_OF_UPDATE_BUTTON);

            // Set form height.
            this.Height = label_SuperMathCredit.Location.Y +
                + label_SuperMathCredit.Height + VERTICAL_DISTANCE_AFTER_CREDIT_LABEL;

            // Display form in the center of the screen.
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2),
                (Screen.PrimaryScreen.WorkingArea.Height / 2) - (this.Height / 2));
        }


        /// <summary>
        /// server was chosen
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">useful parameteres</param>
        private void button_Click(object sender, EventArgs e)
        {
            UploadServer SiteToUpload = (UploadServer)((Button)sender).Tag;

            m_UploadForm.SetServer(SiteToUpload, m_FilePath, m_Url, m_IsFileWillBeDeletedAfterUploading);
            m_UploadForm.FormClosed += new FormClosedEventHandler(form_FormClosed);

            m_UploadForm.Show();
            this.Hide();
        }

        /// <summary>
        /// The "child" form was closed
        /// </summary>
        /// <param name="obj"> The child form. </param>
        /// <param name="e"> Useful parameters. </param>
        private void form_FormClosed(object obj, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        /// <summary>
        /// Raised when User Switched Server event.
        /// </summary>
        /// <param name="sender"> The form. </param>
        /// <param name="filePath"> File for uploading. </param>
        /// <param name="url"> Url for uploading. </param>
        /// <param name="isFileWillBeDeletedAfterUpload"> If file will be deleted. </param>
        private void UploadForm_OnUserSwitchServer(object sender, string filePath, string url, bool isFileWillBeDeletedAfterUpload)
        {
            m_FilePath = filePath;
            m_Url = url;
            m_IsFileWillBeDeletedAfterUploading = isFileWillBeDeletedAfterUpload;

            this.Show();
            this.Activate();

            ((Form)sender).Hide();
        }

        /// <summary>
        /// Show a special messageBox.
        /// Used for invoking.
        /// </summary>
        /// <param name="msg"> Message to show. </param>
        private void ShowSpecialMessegeBox(MyMessageBox msg)
        {
            msg.Show();
        }

        #region Update From Web Button

        /// <summary>
        /// Check for updates image was pressed.
        /// </summary>
        /// <param name="sender"> The pictureBox of the "check for update". </param>
        /// <param name="e"> Useful parameters. </param>
        private void pictureBox_UpdateButton_Click(object sender, EventArgs e)
        {
            // Check for updates. (if the userclicked properly).
            if (((PictureBox)sender).Tag.ToString() == true.ToString())
            {
                try
                {
                    // Waits for user to see the effect.
                    System.Threading.Thread.Sleep(50);

                    // Change the update image to bright.
                    ((PictureBox)sender).Image = Properties.Resources.update_Image;

                    // Change image click "flag" to false.
                    ((PictureBox)sender).Tag = false;

                    CheckForUpdates();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Raises when MouseDown on the "check for updates pictureBox".
        /// </summary>
        /// <param name="sender"> The "check for updates pictureBox". </param>
        /// <param name="e"> Useful parameters. </param>
        private void pictureBox_UpdateButton_MouseDown(object sender, MouseEventArgs e)
        {
            // Calculate the clicked point in the image.
            Point point = new Point();
            point.X = (int)(e.X * ((float) ((PictureBox)sender).Image.Width /
                ((PictureBox)sender).Width));
            point.Y = (int)(e.Y * ((float) ((PictureBox)sender).Image.Height /
                ((PictureBox)sender).Height));

            // Checks if the click is in the image, and not in transparent area.
            if (!((Bitmap)((PictureBox)sender).Image).GetPixel(point.X, point.Y).ToArgb().Equals(Color.Transparent.ToArgb()))
            {
                // Change the update image to faint.
                ((PictureBox)sender).Image = Properties.Resources.update_ImagePressed;

                // Change image click "flag" to true. - means, the user clicked on the image.
                ((PictureBox)sender).Tag = true;
            }
        }

        /// <summary>
        /// Raises when MouseUp on the "check for updates pictureBox".
        /// </summary>
        /// <param name="sender">  The "check for updates pictureBox".  </param>
        /// <param name="e"> Useful parameters. </param>
        private void pictureBox_UpdateButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Change the update image to bright.
            ((PictureBox)sender).Image = Properties.Resources.update_Image;

            // Change image click "flag" to false.
            ((PictureBox)sender).Tag = false;
        }

        /// <summary>
        /// Check if the application is up to date.
        /// </summary>
        private void CheckForUpdates()
        {
            // My special message box.
            MyMessageBox msg;

            try
            {
                if (AccessoryFuncs.CheckForInternetConnection("https://github.com"))
                {
                    using (WebClient client = new WebClient())
                    {
                        string htmlCode =
                            client.DownloadString(
                                "https://raw.githubusercontent.com/supermathbond/FastImageUpload/master/ImageUploader/Versions.txt#");

                        if (htmlCode == "")
                        {
                            msg = new MyMessageBox("Message", "Connection with server aborted");
                            this.Invoke(new Action<MyMessageBox>(ShowSpecialMessegeBox), msg);
                            return;
                        }

                        // Searchs for the version number.
                        int start = htmlCode.IndexOf("[version]") + "[version]".Length;
                        int end = htmlCode.IndexOf("[/version]", start);
                        string version = htmlCode.Substring(start, end - start);

                        // Searchs for the Last improvements list.
                        start = htmlCode.IndexOf("[Improvements]") + "[Improvements]".Length;
                        end = htmlCode.IndexOf("[/Improvements]", start);
                        string improvements = htmlCode.Substring(start, end - start);

                        // Checks if the application is up to date.
                        if (version == System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
                        {
                            msg = new MyMessageBox("Message", "Version is up to date.",
                                string.Empty, improvements);
                            this.Invoke(new Action<MyMessageBox>(ShowSpecialMessegeBox), msg);
                        }
                        else
                        {
                            // Searchs for the new version link.
                            start = htmlCode.IndexOf("[Download]") + "[Download]".Length;
                            end = htmlCode.IndexOf("[/Download]", start);
                            string link = htmlCode.Substring(start, end - start);
                            msg = new MyMessageBox("Message", "There is a new version.\nFor download:",
                                link, improvements);
                            this.Invoke(new Action<MyMessageBox>(ShowSpecialMessegeBox), msg);
                        }
                    }
                }
                else
                {
                    // No connection.
                    msg = new MyMessageBox("Message", "Can't reach server,\ncheck your internet connection.");
                    this.Invoke(new Action<MyMessageBox>(ShowSpecialMessegeBox), msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
