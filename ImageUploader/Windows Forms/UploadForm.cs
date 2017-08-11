using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageUploader
{
    public partial class UploadForm : Form
    {
        #region Constants % Readonly-s

        const int Y_LOCATION_SERVER_IMAGE_LOGO = 7;
        readonly Size SERVER_IMAGE_LOGO_SIZE = new Size(397, 77);

        const int VERTICAL_DISTANCE_BETWEEN_LOGO_TO_RADIO_BUTTONS = 10;
        const int ESTIMATED_WIDTH_OF_THE_CIRCLE_IN_RADIO_BUTTON = 20;
        const int HORIZONAL_DISTANCE_BETWEEN_RADIO_BUTTONS = 10;
        const int VERTICAL_DISTANCE_BETWEEN_RADIO_BUTTONS_TO_PANEL = 10;
        
        readonly Size PANEL_SIZE_IMAGE_RADIO_BUTTON;
        readonly Size PANEL_SIZE_URL_RADIO_BUTTON;
        const int VERTICAL_DISTANCE_OF_UPLOAD_BUTTON_FROM_PANEL = 20;

        #region In panel constants.

        // Image upload checked.

        const int HORIZONAL_DISTNACE_FROM_IMAGE_UPLOAD_PANEL = 8;
        const int EXTRA_HORIZONAL_DISTNACE_FROM_IMAGE_UPLOAD_PANEL_FOR_CHECK_BOX = 3;
        const int VERTICAL_DISTANCE_FROM_TOP_OF_THE_PANEL_TO_TAKE_SNAPSHOT_BUTTON = 9;

        const int VERTICAL_DISTANCE_FROM_TAKE_SNAPSHOT_BUTTON_TO_DELETE_CHECKBOX = 8;

        const int VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_BROWSE_BUTTON = 21;
        const int X_LOCATION_OF_BROWSE_BUTTON = 313;
        const int VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_IMAGE_PATH_TEXTBOX = 23;
        const int HORIZONAL_DISTANCE_FROM_LABEL_TO_IMAGE_PATH_TEXTBOX = 6;
        const int VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_SEND_FILE_LABEL = 26;

        const int VERTICAL_DISTANCE_FROM_BROWSE_BUTTON_TO_EDIT_BUTTON = 6;
        const int X_LOCATION_OF_EDIT_BUTTON = 313;

        const int VERTICAL_DISTANCE_FROM_EDIT_BUTTON_TO_BOTTOM_OF_THE_PANEL = 21;


        // Url upload checked.

        const int X_LOCATION_OF_UPLOAD_URL_LABEL = 8;
        const int VERTICAL_DISTANCE_FROM_TOP_OF_THE_PANEL_TO_UPLOAD_URL_LABEL = 23;

        const int X_LOCATION_OF_URL_TEXTBOX = 8;
        const int VERTICAL_DISTANCE_FROM_UPLOAD_URL_LABEL_TO_URL_TEXTBOX = 15;

        const int VERTICAL_DISTANCE_FROM_URL_TEXTBOX_TO_BOTTOM_OF_THE_PANEL = 25;

        #endregion


        const int VERTICAL_BLANK_DISTANCE_UNTIL_LOADING_IMAGE = 18;
        const int VERTICAL_BLANK_DISTANCE_UNTIL_LOADING_LABEL = 24;

        const int VERTICAL_BLANK_DISTANCE_UNTIL_CHANGE_SERVER_BUTTON = 10;
        const int VERTICAL_BLANK_DISTANCE_UNTIL_CREDIT_LABEL = 25;
        const int SWITCH_SERVER_BUTTON_X_LOCATION = 12;
        const int HORIZONAL_DISTANCE_FROM_CREDIT_LABEL_TO_BORDER = 10;
        const int VERTICAL_DISTANCE_FROM_SWITCH_BUTTON_TO_BOTTOM_OF_THE_FORM = 50;


        readonly string MSPAINT_PATH = Environment.GetFolderPath(
                                    Environment.SpecialFolder.System) + "\\mspaint.exe";

        #endregion

        #region Private Fields

        /// <summary>
        /// The server to upload
        /// </summary>
        private UploadServer _siteToUpload;

        #endregion

        #region OnUserSwitchServer event

        /// <summary>
        /// delegate for OnUserSwitchServer event.
        /// </summary>
        /// <param name="sender"> The form. </param>
        /// <param name="filePath"> File for uploading. </param>
        /// <param name="url"> Url for uploading. </param>
        /// <param name="isFileWillBeDeletedAfterUpload"> If file will be deleted. </param>
        public delegate void UserSwitchServer(object sender, string filePath, string url, bool isFileWillBeDeletedAfterUpload);
        
        /// <summary>
        /// Event that raise when user want to switch the server.
        /// </summary>
        public event UserSwitchServer OnUserSwitchServer;

        #endregion

        #region Class Constructors

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public UploadForm()
        {
            InitializeComponent();

            // check if the "mspaint.exe" exist, if not button "edit" wil be disabled.
            if (!File.Exists(MSPAINT_PATH))
            {
                button_EditPicture.Visible = false;
                button_EditPicture.Enabled = false;
            }

            // Set toolTips.
            toolTip.SetToolTip(button_SwitchServer, "Change upload server");
            toolTip.SetToolTip(button_TakeSnapshot, "Take image snapshot of screen");
            toolTip.SetToolTip(button_EditPicture, "Edit with Windows mspaint");
            toolTip.SetToolTip(button_Browse, "Choose image path");
            toolTip.SetToolTip(button_Upload, "Upload image to server");

            // Set panel size.
            int symmetricWidth = this.ClientRectangle.Width -
                2 * ((this.ClientRectangle.Width / 2) - (panel.Width / 2));
            PANEL_SIZE_IMAGE_RADIO_BUTTON = new Size(symmetricWidth, 189);
            PANEL_SIZE_URL_RADIO_BUTTON = new Size(symmetricWidth, 100);
        }

        /// <summary>
        /// Constructor. (use the parameterless constructor).
        /// </summary>
        /// <param name="site"> Site to upload. </param>
        public UploadForm(UploadServer site) : this()
        {
            // Saves the site to upload
            _siteToUpload = site;

            // Changes the caption. (for example: upload to imageshack)
            this.Text = "Upload to " + site;

            // this.TopMost = true; // always on top.
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Updates panel according to the radio buttons,
        /// and enable or disable the panel by user demand.
        /// </summary>
        /// <param name="whetherEnableOrNot"> True - enable panel, False - disable it. </param>
        private void UpdatePanel(bool whetherEnableOrNot)
        {
            // Disable.
            if (!whetherEnableOrNot)
            {
                panel.Enabled = false;
                return;
            }

            // Enable.
            panel.Enabled = true;

            if (radioButton_Image.Checked)
            {
                // Change panel size.
                panel.Size = PANEL_SIZE_IMAGE_RADIO_BUTTON;

                // Enable all the image path stuff.
                button_TakeSnapshot.Visible = true;
                button_TakeSnapshot.Enabled = true;
                checkBox_DeleteUploadedFile.Visible = true;
                checkBox_DeleteUploadedFile.Enabled = true;
                label_FileToSend.Visible = true;
                label_FileToSend.Enabled = true;
                textBox_FilePathToUpload.Visible = true;
                textBox_FilePathToUpload.Enabled = true;
                button_Browse.Visible = true;
                button_Browse.Enabled = true;

                // check if the "mspaint.exe" exist, if not button "edit" wil be disabled.
                if (File.Exists(MSPAINT_PATH))
                {
                    button_EditPicture.Visible = true;

                    if (textBox_FilePathToUpload.Text != "")
                        button_EditPicture.Enabled = true;
                }

                // Disable all the url stuff.
                textBox_linkToUpload.Enabled = false;
                textBox_linkToUpload.Visible = false;
                label_UrlInstruction.Enabled = false;
                label_UrlInstruction.Visible = false; 
            }
            else
            {
                // Change panel size.
                panel.Size = PANEL_SIZE_URL_RADIO_BUTTON;

                // Disable all the image path stuff.
                button_TakeSnapshot.Visible = false;
                button_TakeSnapshot.Enabled = false;
                checkBox_DeleteUploadedFile.Visible = false;
                checkBox_DeleteUploadedFile.Enabled = false;
                label_FileToSend.Visible = false;
                label_FileToSend.Enabled = false;
                textBox_FilePathToUpload.Visible = false;
                textBox_FilePathToUpload.Enabled = false;
                button_Browse.Visible = false;
                button_Browse.Enabled = false;
                button_EditPicture.Visible = false;
                button_EditPicture.Enabled = false;

                // Enable all the url stuff.
                textBox_linkToUpload.Enabled = true;
                textBox_linkToUpload.Visible = true;
                label_UrlInstruction.Enabled = true;
                label_UrlInstruction.Visible = true;
            }
        }

        /// <summary>
        /// Orgenize the main panel in the window. (the panel with the url/image path).
        /// </summary>
        private void OrganizePanel()
        {
            int yCounter = 0;

            if (radioButton_Image.Checked)
            {
                yCounter = VERTICAL_DISTANCE_FROM_TOP_OF_THE_PANEL_TO_TAKE_SNAPSHOT_BUTTON;

                button_TakeSnapshot.Location = new Point(
                    (panel.ClientRectangle.Width / 2) - (button_TakeSnapshot.Width / 2),
                    yCounter);
                yCounter += button_TakeSnapshot.Height +
                    VERTICAL_DISTANCE_FROM_TAKE_SNAPSHOT_BUTTON_TO_DELETE_CHECKBOX;


                checkBox_DeleteUploadedFile.Location = new Point(HORIZONAL_DISTNACE_FROM_IMAGE_UPLOAD_PANEL +
                    EXTRA_HORIZONAL_DISTNACE_FROM_IMAGE_UPLOAD_PANEL_FOR_CHECK_BOX, yCounter);

                button_Browse.Location = new Point(X_LOCATION_OF_BROWSE_BUTTON,
                    yCounter + VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_BROWSE_BUTTON);

                label_FileToSend.Location = new Point(HORIZONAL_DISTNACE_FROM_IMAGE_UPLOAD_PANEL,
                    yCounter + VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_SEND_FILE_LABEL);

                textBox_FilePathToUpload.Location = new Point(
                    label_FileToSend.Location.X + label_FileToSend.Width +
                    HORIZONAL_DISTANCE_FROM_LABEL_TO_IMAGE_PATH_TEXTBOX,
                    yCounter +
                    VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_IMAGE_PATH_TEXTBOX);

                yCounter += VERTICAL_DISTANCE_FROM_DELETE_CHECKBOX_TO_BROWSE_BUTTON + 
                    button_Browse.Height + 
                    VERTICAL_DISTANCE_FROM_BROWSE_BUTTON_TO_EDIT_BUTTON;


                button_EditPicture.Location = new Point(X_LOCATION_OF_EDIT_BUTTON,
                    yCounter);
                yCounter +=  button_EditPicture.Height +
                    VERTICAL_DISTANCE_FROM_EDIT_BUTTON_TO_BOTTOM_OF_THE_PANEL;

                panel.Height = yCounter;
            }
            else
            {
                yCounter = VERTICAL_DISTANCE_FROM_TOP_OF_THE_PANEL_TO_UPLOAD_URL_LABEL;

                label_UrlInstruction.Location = new Point(
                    X_LOCATION_OF_UPLOAD_URL_LABEL, yCounter);
                yCounter += label_UrlInstruction.Height +
                    VERTICAL_DISTANCE_FROM_UPLOAD_URL_LABEL_TO_URL_TEXTBOX;

                textBox_linkToUpload.Location = new Point(
                    X_LOCATION_OF_URL_TEXTBOX, yCounter);
                yCounter += textBox_linkToUpload.Height +
                    VERTICAL_DISTANCE_FROM_URL_TEXTBOX_TO_BOTTOM_OF_THE_PANEL;

                panel.Height = yCounter;
            }
        }

        /// <summary>
        /// Organize controls in form.
        /// </summary>
        private void NormalModeFormView()
        {
            // Suspends new controls from appearing in form until resume command.
            SuspendLayout();

            OrganizePanel();

            // Set width.
            this.Width = 429;

            ListOfServerProperties listServers = ListOfServerProperties.Instance;

            // Set server's image logo size and location.
            pictureBox_ServerImageLogo.Size = SERVER_IMAGE_LOGO_SIZE;
            pictureBox_ServerImageLogo.Location = new Point ((this.ClientRectangle.Width / 2) -
                (pictureBox_ServerImageLogo.Width / 2), Y_LOCATION_SERVER_IMAGE_LOGO);

            int panelLocationX = (this.ClientRectangle.Width / 2) - (panel.Width / 2);

            // Update form if the server allows URL uploading.
            if (!listServers.HasURLUpload((int)_siteToUpload))
            {
                // URL isn't allowed.

                // Disable radio buttons.
                radioButton_Url.Visible = false;
                radioButton_Image.Visible = false;
                radioButton_Image.Checked = true;

                // Set panel location.
                panel.Location = new Point(panelLocationX,
                    Y_LOCATION_SERVER_IMAGE_LOGO + SERVER_IMAGE_LOGO_SIZE.Height +
                    VERTICAL_DISTANCE_BETWEEN_RADIO_BUTTONS_TO_PANEL);
            }
            else
            {
                // URL is allowed.

                radioButton_Image.Visible = true;
                radioButton_Url.Visible = true;

                // Calculate size of text in image radioButton.
                Size textSize = TextRenderer.MeasureText(radioButton_Image.Text, radioButton_Image.Font);

                // Set radio buttons location.
                radioButton_Image.Location = new Point(panelLocationX,
                     Y_LOCATION_SERVER_IMAGE_LOGO + SERVER_IMAGE_LOGO_SIZE.Height +
                     VERTICAL_DISTANCE_BETWEEN_LOGO_TO_RADIO_BUTTONS);
                radioButton_Url.Location = new Point(panelLocationX + radioButton_Image.Width,
                     Y_LOCATION_SERVER_IMAGE_LOGO + SERVER_IMAGE_LOGO_SIZE.Height +
                     VERTICAL_DISTANCE_BETWEEN_LOGO_TO_RADIO_BUTTONS);

                // Set panel location.
                panel.Location = new Point(panelLocationX,
                    radioButton_Image.Location.Y + radioButton_Image.Height +
                    VERTICAL_DISTANCE_BETWEEN_RADIO_BUTTONS_TO_PANEL);
            }

            // Set upload button's location.
            button_Upload.Location = new Point(
                (ClientRectangle.Width / 2) - (button_Upload.Width / 2),
                panel.Location.Y + panel.Height +
                VERTICAL_DISTANCE_OF_UPLOAD_BUTTON_FROM_PANEL);

            // Set switch server button's location.
            button_SwitchServer.Location = new Point(SWITCH_SERVER_BUTTON_X_LOCATION,
                button_Upload.Location.Y + button_Upload.Height +
                VERTICAL_BLANK_DISTANCE_UNTIL_CHANGE_SERVER_BUTTON);

            // Set credit label (made by supermath) his location.
            label_creditToSuperMath.Location = new Point(this.ClientRectangle.Width - label_creditToSuperMath.Width -
                HORIZONAL_DISTANCE_FROM_CREDIT_LABEL_TO_BORDER,
                button_Upload.Location.Y + button_Upload.Height +
                VERTICAL_BLANK_DISTANCE_UNTIL_CREDIT_LABEL);

            // Set height of the form.
            this.Height = button_SwitchServer.Location.Y + button_SwitchServer.Height + VERTICAL_DISTANCE_FROM_SWITCH_BUTTON_TO_BOTTOM_OF_THE_FORM;

            // Update all form's controls changes.
            ResumeLayout();
        }

        /// <summary>
        /// Organize controls in form while uploading.
        /// </summary>
        private void UploadingModeFormView()
        {            
            int yCounter = button_Upload.Location.Y + button_Upload.Height;

            status.Location = new Point(165,
                yCounter + VERTICAL_BLANK_DISTANCE_UNTIL_LOADING_LABEL);
            pictureBoxLoading.Location = new Point(106,
                yCounter + VERTICAL_BLANK_DISTANCE_UNTIL_LOADING_IMAGE);

            yCounter += VERTICAL_BLANK_DISTANCE_UNTIL_LOADING_IMAGE + pictureBoxLoading.Height;

            // Set switch server button's location.
            button_SwitchServer.Location = new Point(SWITCH_SERVER_BUTTON_X_LOCATION,
                yCounter + VERTICAL_BLANK_DISTANCE_UNTIL_CHANGE_SERVER_BUTTON);

            // Set credit label (made by supermath) his location.
            label_creditToSuperMath.Location = new Point(this.ClientRectangle.Width - label_creditToSuperMath.Width -
                HORIZONAL_DISTANCE_FROM_CREDIT_LABEL_TO_BORDER,
                yCounter + VERTICAL_BLANK_DISTANCE_UNTIL_CREDIT_LABEL);

            // Set height of the form.
            this.Height = label_creditToSuperMath.Location.Y +
                label_creditToSuperMath.Height +
                VERTICAL_DISTANCE_FROM_SWITCH_BUTTON_TO_BOTTOM_OF_THE_FORM;

            // disable all the form controls and show the "loading image"

            radioButton_Url.Enabled = false;
            radioButton_Image.Enabled = false;
            button_Upload.Enabled = false;
            button_SwitchServer.Enabled = false;

            // Disable panel.
            UpdatePanel(false);

            status.Visible = true;
            pictureBoxLoading.Visible = true;
        }

        /// <summary>
        /// Organize controls in form after stopping the upload.
        /// </summary>
        private void StopUploadingModeFormView()
        {
            // Set switch server button's location.
            button_SwitchServer.Location = new Point(SWITCH_SERVER_BUTTON_X_LOCATION,
                button_Upload.Location.Y + button_Upload.Height +
                VERTICAL_BLANK_DISTANCE_UNTIL_CHANGE_SERVER_BUTTON);

            // Set credit label (made by supermath) his location.
            label_creditToSuperMath.Location = new Point(this.ClientRectangle.Width - label_creditToSuperMath.Width -
                HORIZONAL_DISTANCE_FROM_CREDIT_LABEL_TO_BORDER,
                button_Upload.Location.Y + button_Upload.Height +
                VERTICAL_BLANK_DISTANCE_UNTIL_CREDIT_LABEL);

            // Set height of the form.
            this.Height = label_creditToSuperMath.Location.Y + label_creditToSuperMath.Height + VERTICAL_DISTANCE_FROM_SWITCH_BUTTON_TO_BOTTOM_OF_THE_FORM;

            radioButton_Url.Enabled = true;
            radioButton_Image.Enabled = true;
            button_SwitchServer.Enabled = true;
            button_Upload.Enabled = true;

            // Disable panel.
            UpdatePanel(true);

            // Disable loading picture and label.
            pictureBoxLoading.Visible = false;
            status.Visible = false;
        }

        /// <summary>
        /// Raise when the form activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (pictureBoxLoading.Visible == true)
                pictureBoxLoading.Refresh();
        }

        /// <summary>
        /// Open the "Paint" (of windows) with the image, in order to edit the image.
        /// </summary>
        /// <param name="sender">the button "Edit"</param>
        /// <param name="e">parameters</param>
        private void butEdit_Click(object sender, EventArgs e)
        {
            try
            {
                const string QUOTE = "\"";

                System.Diagnostics.Process paint = new System.Diagnostics.Process();

                paint.StartInfo.FileName = MSPAINT_PATH;
                paint.StartInfo.Arguments = QUOTE + textBox_FilePathToUpload.Text + QUOTE;

                paint.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        /// <summary>
        /// get the dropped file and check it.
        /// </summary>
        /// <param name="sender">textBox</param>
        /// <param name="e">arguments</param>
        private void FileBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    if ((file.EndsWith(".jpg")) || (file.EndsWith(".JPG")) ||
                        (file.EndsWith(".jpeg")) || (file.EndsWith(".JPEG")) ||
                        (file.EndsWith(".PNG")) || (file.EndsWith(".png")) ||
                        (file.EndsWith(".gif")) || (file.EndsWith(".GIF")) ||
                        (file.EndsWith(".tif")) || (file.EndsWith(".TIF")))
                    {
                        textBox_FilePathToUpload.Text = file;
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// File was drag to the textBox
        /// </summary>
        /// <param name="sender">textBox</param>
        /// <param name="e">arguments</param>
        private void FileBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// "Image - radio button" or "Url - radio button" was checked.
        /// </summary>
        /// <param name="sender"> Radio button. </param>
        /// <param name="e"> Arguments. </param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Update panel according to the checked radio button.
            UpdatePanel(true);
            
            // Set the form to normal view.
            NormalModeFormView();
        }

        /// <summary>
        /// take the snapshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSnapShot_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefiledialog = null;

            try
            {
                // Take a picture of screen.
                this.Opacity = 0;
                pictureBox1.Image = AccessoryFuncs.TakeScreenPicture();
                this.Opacity = 100;

                // Open save file dialog, for saving the snapshot.
                savefiledialog = new SaveFileDialog();
                savefiledialog.Filter = "JPEG|*.jpeg|PNG|*.png|GIF|*.gif";

                // If Ok was pressed.
                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    string imageFormat = Path.GetExtension(savefiledialog.FileName);
                    System.Drawing.Imaging.ImageFormat formatToSave;

                    // Check file extension.
                    switch (imageFormat)
                    {
                        case ".jpeg":
                        case ".JPEG":
                            {
                                formatToSave = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            }
                        case ".jpg":
                        case ".JPG":
                            {
                                formatToSave = System.Drawing.Imaging.ImageFormat.Jpeg; 
                                break;
                            }
                        case ".png":
                        case ".PNG":
                            {
                                formatToSave = System.Drawing.Imaging.ImageFormat.Png;
                                break;
                            }
                        case ".gif":
                        case ".GIF":
                            {
                                formatToSave = System.Drawing.Imaging.ImageFormat.Gif;
                                break;
                            }
                        default: throw new Exception("Bad input");
                    }

                    textBox_FilePathToUpload.Text = savefiledialog.FileName;
                    pictureBox1.Image.Save(savefiledialog.FileName, formatToSave);
                    
                    // check if the "mspaint.exe" exist, if not button "edit" wil be disabled.
                    if (File.Exists(MSPAINT_PATH))
                        button_EditPicture.Enabled = true;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            finally
            {
                savefiledialog.Dispose();
            }
        }


        /// <summary>
        /// browse for file
        /// </summary>
        /// <param name="sender"> The buutonn. </param>
        /// <param name="e"> Useful Parameters. </param>
        private void Browse_Click(object sender, EventArgs e)
        {
            try
            {
                // Browse for image to upload.
                openFileDialog.Filter = "Image |*.jpeg;*.jpg;*.png;*.gif;*.tif";

                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;

                // Prevent unwanted input.
                if (!(openFileDialog.FileName.EndsWith(".jpeg") || openFileDialog.FileName.EndsWith(".jpg")
                    || openFileDialog.FileName.EndsWith(".png") || openFileDialog.FileName.EndsWith(".gif")
                    || openFileDialog.FileName.EndsWith(".tif")))
                {
                    MessageBox.Show("Bad Input", "Error in uploaded file", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                    return;
                }

                textBox_FilePathToUpload.Text = openFileDialog.FileName;

                // check if the "mspaint.exe" exist, if not button "edit" wil be disabled.
                if (File.Exists(MSPAINT_PATH))
                    button_EditPicture.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error while opening file", "Error in uploaded file", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
        }


        /// <summary>
        /// Raised when user decided to switch server.
        /// </summary>
        /// <param name="sender"> The button. </param>
        /// <param name="e"> Useful parameters. </param>
        private void button_SwitchServer_Click(object sender, EventArgs e)
        {
            if (OnUserSwitchServer != null)
            {
                OnUserSwitchServer(this, textBox_FilePathToUpload.Text, textBox_linkToUpload.Text, checkBox_DeleteUploadedFile.Checked);
            }
            else
            {
                MessageBox.Show("Inner error :(");
            }
        }

        /// <summary>
        /// the delegate for the event of preesing on "Upload button"
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"> Useful parameters. </param>
        private void Upload_Click(object sender, EventArgs e)
        {
            try
            {
                #region --- Check for user errors. ---

                if (radioButton_Image.Checked == true)
                {
                    // Checks if user entered file.
                    if (textBox_FilePathToUpload.Text == string.Empty)
                        throw new Exception("No file was selected");

                    // Checks that all files exist.
                    if (!File.Exists(textBox_FilePathToUpload.Text))
                        throw new Exception("File does not exist");
                }
                else if (radioButton_Url.Checked == true)
                {
                    // Checks if user entered link.
                    if (textBox_linkToUpload.Text == string.Empty)
                        throw new Exception("No URL was given");

                    // Checks that link is valid.
                    if (!AccessoryFuncs.IsURL(textBox_linkToUpload.Text))
                        throw new Exception("URL is malformed");
                }

                #endregion
                

                // Change GUI.
                UploadingModeFormView();

                if(radioButton_Image.Checked)
                    backgroundWorker.RunWorkerAsync(textBox_FilePathToUpload.Text);
                else
                    backgroundWorker.RunWorkerAsync(textBox_linkToUpload.Text);
            }
            catch (Exception e1)
            {
                MessageBoxOptions options = MessageBoxOptions.RtlReading;
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, options);
            }
        }


        /// <summary>
        /// Start uploading.
        /// </summary>
        /// <param name="sender"> BackgroundWorker. </param>
        /// <param name="e"> Useful parameters. </param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!AccessoryFuncs.CheckForInternetConnection(
                    ListOfServerProperties.Instance.GetURL((int)_siteToUpload)))
                {
                    throw new Exception("Could not access: " + ListOfServerProperties.Instance.GetURL((int)_siteToUpload) + "\nYou might be offline");
                }

                e.Result = UploaderFactory.StartUpload(_siteToUpload, e.Argument as string);
            }
            catch (Exception ex)
            {
                // Cancel thread.
                e.Cancel = true;
                backgroundWorker.CancelAsync();

                // Show error.
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
        }

        /// <summary>
        /// Upload completed.
        /// </summary>
        /// <param name="sender"> BackgroundWorker. </param>
        /// <param name="e"> Useful parameters. </param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // Change GUI.
                StopUploadingModeFormView();

                if (!e.Cancelled)
                {
                    /// TODO: Delete all files.

                    if (checkBox_DeleteUploadedFile.Checked)
                    {
                        File.Delete(textBox_FilePathToUpload.Text);
                        textBox_FilePathToUpload.Text = string.Empty;
                    }

                    // Show result.
                    GetImage ImageForm = new GetImage(e.Result.ToString());
                    ImageForm.Show();
                }
            }
            catch (Exception ex)
            {
                // Show error.
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the server to upload to.
        /// Should be called before form loads.
        /// </summary>
        /// <param name="site"> Site to upload. </param>
        /// <param name="filePath"> File or url for uploading. </param>
        /// <param name="url"> Url for uploading. </param>
        /// <param name="isFileWillBeDeleted"> If file will be deleted. </param>
        public void SetServer(UploadServer site, string filePath, string url, bool isFileWillBeDeleted)
        {
            // saves the site to upload
            _siteToUpload = site;

            // Change the caption. (for example: upload to imageshack)
            this.Text = "Upload to " + site;

            // Set image path and url.
            textBox_FilePathToUpload.Text = filePath;
            textBox_linkToUpload.Text = url;

            // Is file will be deleted.
            checkBox_DeleteUploadedFile.Checked = isFileWillBeDeleted;

            // Set pictureBox of site logo.
            pictureBox_ServerImageLogo.BackgroundImage = ListOfServerProperties.Instance.GetBitmap((int)_siteToUpload);
            pictureBox_ServerImageLogo.BackgroundImageLayout = ImageLayout.Center;

            // Change form.
            NormalModeFormView();
        }

        #endregion
    }
}