using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageUploader
{
    public partial class MyMessageBox : Form
    {
        private const int VERTICAL_DISTANCE_BETWEEN_MESSAGE_LABEL_TO_LINK_TEXTBOX = 20;

        private const int Y_LOCATION_OF_MESSAGE_LABEL = 33;
        private const int VERTICAL_DISTANCE_FROM_LINK_TEXTBOX = 20;

        private const int X_LOCATION_OF_OPEN_LINKLABEL = 12;
        private const int VERTICAL_DISTANCE_FROM_OPEN_LINKLABEL = 20;

        private const int X_LOCATION_OF_VERSIONS_IMPROVEMENTS_TEXTBOX = 12;
        private const int VERTICAL_DISTANCE_FROM_VERSIONS_IMPROVEMENTS_TEXTBOX = 20;

        private const int VERTICAL_DISTANCE_FROM_BUTTON_TO_FORM = 30;


        private bool m_ImprovementsISShowing = false;

        #region Class Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text"> Text message of messageBox. </param>
        public MyMessageBox(string text) :
            this(string.Empty, text, string.Empty, string.Empty) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"> Title of messageBox. </param>
        /// <param name="text"> Text message of messageBox. </param>
        public MyMessageBox(string title, string text) :
            this(title, text, string.Empty, string.Empty) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"> Title of messageBox. </param>
        /// <param name="text"> Text message of messageBox. </param>
        /// <param name="link"> Link to new version. </param>
        public MyMessageBox(string title, string text, string link) :
            this(title, text, link, string.Empty) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"> Title of messageBox. </param>
        /// <param name="text"> Text message of messageBox. </param>
        /// <param name="link"> Link to new version. </param>
        /// <param name="versionImprovements"> Notes of versions improvements. </param>
        public MyMessageBox(string title, string text, string link, string versionImprovements)
        {
            InitializeComponent();

            // Set the messeage and the title.
            label_MessageString.Text = text;
            this.Text = title;

            // Set the link. (if there is one)
            textBox_link.Text = link;
            if (link.Equals(string.Empty))
            {
                textBox_link.Enabled = false;
                textBox_link.Visible = false;
            }

            // Set the versions improvements. (if there is one)
            textBox_VersionsImprovements.Text = versionImprovements;
            if (versionImprovements.Equals(string.Empty))
            {
                textBox_VersionsImprovements.Enabled = false;
                textBox_VersionsImprovements.Visible = false;
                linkLabel_OpenTextBox.Enabled = false;
                linkLabel_OpenTextBox.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// Raises when the form loads.
        /// </summary>
        /// <param name="sender"> The form. </param>
        /// <param name="e"> Useful parameters. </param>
        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            int yCounter = 0;

            // Set the message location.
            label_MessageString.Location = new Point(12, Y_LOCATION_OF_MESSAGE_LABEL);
            yCounter = label_MessageString.Location.Y + label_MessageString.Height +
                VERTICAL_DISTANCE_FROM_BUTTON_TO_FORM;

            // Set the link textBox location. or hide it if the link is empty.
            if (textBox_link.Text != string.Empty)
            {
                textBox_link.Location = new Point(textBox_link.Location.X, yCounter);
                yCounter += textBox_link.Height + VERTICAL_DISTANCE_FROM_LINK_TEXTBOX;
            }

            // Set the versions improvements textBox location.
            // or hide it if the versions improvements is empty.
            if (textBox_VersionsImprovements.Text != string.Empty)
            {
                // Set the link label location.
                linkLabel_OpenTextBox.Location = new Point(X_LOCATION_OF_OPEN_LINKLABEL,
                    yCounter);
                linkLabel_OpenTextBox.Text = "Show Versions Improvements";
                yCounter += linkLabel_OpenTextBox.Height + VERTICAL_DISTANCE_FROM_OPEN_LINKLABEL;

                // Set the multiline textBox location.
                textBox_VersionsImprovements.Location = new Point(
                    X_LOCATION_OF_VERSIONS_IMPROVEMENTS_TEXTBOX, yCounter);
            }

            // Set OK button location.
            button_OK.Location = new Point(button_OK.Location.X, yCounter);
            yCounter += linkLabel_OpenTextBox.Height + VERTICAL_DISTANCE_FROM_OPEN_LINKLABEL;

            // Set form height.
            this.Height = button_OK.Location.Y + button_OK.Height +
                VERTICAL_DISTANCE_FROM_BUTTON_TO_FORM +
                (this.Height - this.ClientRectangle.Height) / 2;
        }   

        /// <summary>
        /// The linkLabel was pressed.
        /// </summary>
        /// <param name="sender"> The linkLabel. </param>
        /// <param name="e"> Useful parameters. </param>
        private void linkLabel_OpenTextBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // If the versions improvements is appearing.
            if (m_ImprovementsISShowing)
            {
                linkLabel_OpenTextBox.Text = "Show Versions Improvements";

                button_OK.Location = new Point(button_OK.Location.X,
                linkLabel_OpenTextBox.Location.Y + linkLabel_OpenTextBox.Height +
                VERTICAL_DISTANCE_FROM_OPEN_LINKLABEL);

                this.Height = button_OK.Location.Y + button_OK.Height +
                    VERTICAL_DISTANCE_FROM_BUTTON_TO_FORM +
                    (this.Height - this.ClientRectangle.Height) / 2;

                textBox_VersionsImprovements.Visible = false;
                textBox_VersionsImprovements.Enabled = false;

                m_ImprovementsISShowing = false;
            }
            else
            {
                // If the versions improvements is hided.

                linkLabel_OpenTextBox.Text = "Hide Versions Improvements";

                button_OK.Location = new Point(button_OK.Location.X,
                textBox_VersionsImprovements.Location.Y + textBox_VersionsImprovements.Height +
                VERTICAL_DISTANCE_FROM_VERSIONS_IMPROVEMENTS_TEXTBOX);

                this.Height = button_OK.Location.Y + button_OK.Height +
                    VERTICAL_DISTANCE_FROM_BUTTON_TO_FORM +
                    (this.Height - this.ClientRectangle.Height) / 2;

                textBox_VersionsImprovements.Visible = true;
                textBox_VersionsImprovements.Enabled = true;
                
                m_ImprovementsISShowing = true;
            }
        }

        /// <summary>
        /// Ok button was pressed.
        /// </summary>
        /// <param name="sender"> The button. </param>
        /// <param name="e"> Useful parameters. </param>
        private void butOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Form was closed.
        /// </summary>
        /// <param name="sender"> The form. </param>
        /// <param name="e"> Useful parameters. </param>
        private void MyMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #region Link TextBox Was Focused

        /// <summary>
        /// Link textbox was focused.
        /// </summary>
        /// <param name="sender"> The textbox. </param>
        /// <param name="e"> Useful parameters. </param>
        private void textBox_link_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// Link textbox was focused.
        /// </summary>
        /// <param name="sender"> The textbox. </param>
        /// <param name="e"> Useful parameters. </param>
        private void textBox_link_MouseDown(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        #endregion
    }
}
