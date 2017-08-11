namespace ImageUploader
{
    public class PasteBoardProperties : IServerProperties
    {
        /// <summary>
        /// Contains the url of the server
        /// </summary>
        public string URL_OF_SERVER
        {
            get { return @"https://pasteboard.co/"; }
        }

        /// <summary>
        /// Indicates whether the site can get a url of image in order to upload it, or not.
        /// </summary>
        public bool CanUploadUrl
        {
            get { return false; }
        }

        /// <summary>
        /// Contains the maximum number of images that can be uploaded simultaneously.
        /// </summary>
        public int MaximumNumberOfImagesToUploadSimultaneously
        {
            get { return 1; }
        }

        /// <summary>
        /// Contains the maximum number of urls that can be uploaded simultaneously.
        /// </summary>
        public int MaximumNumberOfURLsToUploadSimultaneously
        {
            get { return 0; }
        }

        /// <summary>
        /// Get the image logo of the site
        /// </summary>
        /// <returns> Image logo of the site </returns>
        public System.Drawing.Bitmap GetBitmap()
        {
            return Properties.Resources.PasteboardLogo;
        }

        /// <summary>
        /// Get the image logo of the site
        /// </summary>
        /// <returns> Image logo of the site </returns>
        public System.Drawing.Bitmap GetBitmapButton()
        {
            return Properties.Resources.PasteboardLogo;
        }
    }
}
