using System;
using System.Windows.Forms;
using System.Drawing;

namespace ImageUploader
{
    public partial class GetImage : Form
    {
        #region Constants

        private const int VERTICAL_DISTANCE_FROM_TOP_OF_THE_FORM_TO_THE_FIRST_CONTROL = 41;
        private const int VERTICAL_DISTANCE_FROM_BOTTOM_OF_THE_FORM_TO_THE_LAST_CONTROL = 41;

        private const int HORIZONAL_DISTANCE_FROM_LEFT_SIDE_OF_THE_FORM_TO_THE_LABELS = 8;
        private const int HORIZONAL_DISTANCE_FROM_LABELS_TO_THE_TEXTBOX = 80;
        private const int HORIZONAL_DISTANCE_FROM_TEXTBOX_TO_THE_BUTTON = 7;

        private const int HORIZONAL_DISTANCE_FROM_RIGHT_SIDE_OF_THE_FORM_TO_THE_BUTTONS = 30;

        private const int VERTICAL_DIFFERENCE_BETWEEN_THE_LABELSS_AND_THE_TEXTBOX = -3;
        private const int VERTICAL_DIFFERENCE_BETWEEN_THE_BUTTONS_AND_THE_LABELS = -5;

        private const int VERTICAL_DISTANCE_BETWEEN_ROWS = 25;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url"> Url to show. </param>
        public GetImage(string url)
        {
            InitializeComponent();
            
            // Place url in form.
            textBox_ImageDirectUrl.Text = url;
            textBox_ImageBBcodeUrl.Text = "[img]" + url + "[/img]";

            // Organize all controls.
            OrganizeControlsInForm();
        }

        /// <summary>
        /// Organize the controls in the form.
        /// </summary>
        private void OrganizeControlsInForm()
        {
            this.Size = new System.Drawing.Size(601, 188);

            // Set first row of form.
            int yStartPoint = VERTICAL_DISTANCE_FROM_TOP_OF_THE_FORM_TO_THE_FIRST_CONTROL;
            button_CopyDirectLink.Location = new Point(this.Width -
                HORIZONAL_DISTANCE_FROM_RIGHT_SIDE_OF_THE_FORM_TO_THE_BUTTONS -
                button_CopyDirectLink.Width,
                yStartPoint + VERTICAL_DIFFERENCE_BETWEEN_THE_BUTTONS_AND_THE_LABELS);
            textBox_ImageDirectUrl.Location = new Point(button_CopyDirectLink.Location.X -
                HORIZONAL_DISTANCE_FROM_TEXTBOX_TO_THE_BUTTON -
                textBox_ImageDirectUrl.Width,
                yStartPoint + VERTICAL_DIFFERENCE_BETWEEN_THE_LABELSS_AND_THE_TEXTBOX);
            label_DirectLink.Location = new Point(
                textBox_ImageDirectUrl.Location.X - HORIZONAL_DISTANCE_FROM_LABELS_TO_THE_TEXTBOX,
                yStartPoint);


            // Set second row of form.
            yStartPoint = yStartPoint + button_CopyDirectLink.Height +
                VERTICAL_DISTANCE_BETWEEN_ROWS;
            button_CopyBBcodeLink.Location = new Point(
                this.Width -
                HORIZONAL_DISTANCE_FROM_RIGHT_SIDE_OF_THE_FORM_TO_THE_BUTTONS -
                button_CopyBBcodeLink.Width,
                yStartPoint + VERTICAL_DIFFERENCE_BETWEEN_THE_BUTTONS_AND_THE_LABELS);
            textBox_ImageBBcodeUrl.Location = new Point(button_CopyBBcodeLink.Location.X -
                HORIZONAL_DISTANCE_FROM_TEXTBOX_TO_THE_BUTTON -
                textBox_ImageBBcodeUrl.Width,
                yStartPoint + VERTICAL_DIFFERENCE_BETWEEN_THE_LABELSS_AND_THE_TEXTBOX);
            label_BBcode.Location = new Point(
                textBox_ImageBBcodeUrl.Location.X - HORIZONAL_DISTANCE_FROM_LABELS_TO_THE_TEXTBOX,
                yStartPoint);

        }

        /// <summary>
        /// First "Copy" button was pressed.
        /// </summary>
        /// <param name="sender"> The button. </param>
        /// <param name="e"> Useful parameters. </param>
        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox_ImageDirectUrl.Text);
        }

        /// <summary>
        /// Second "Copy" button was pressed.
        /// </summary>
        /// <param name="sender"> The button. </param>
        /// <param name="e"> Useful parameters. </param>
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox_ImageBBcodeUrl.Text);
        }

        /// <summary>
        /// First textbox was focused.
        /// </summary>
        /// <param name="sender"> The textbox. </param>
        /// <param name="e"> Useful parameters. </param>
        private void textBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
            // ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// Second textbox was focused.
        /// </summary>
        /// <param name="sender"> The textbox. </param>
        /// <param name="e"> Useful parameters. </param>
        private void textBox_MouseDown(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
            // ((TextBox)sender).SelectAll();
        }
    }
}
