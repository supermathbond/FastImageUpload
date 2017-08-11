namespace ImageUploader
{
    public interface IServerProperties
    {
        /// <summary>
        /// Contains the url of the server
        /// </summary>
        string URL_OF_SERVER
        {
            get;
        }

        /// <summary>
        /// Indicates whether the site can get a url of image in order to upload it, or not.
        /// </summary>
        bool CanUploadUrl
        {
            get;
        }

        /// <summary>
        /// For future use.
        /// Contains the maximum number of images that can be uploaded simultaneously.
        /// </summary>
        int MaximumNumberOfImagesToUploadSimultaneously
        {
            get;
        }

        /// <summary>
        /// For future use.
        /// Contains the maximum number of urls that can be uploaded simultaneously.
        /// </summary>
        int MaximumNumberOfURLsToUploadSimultaneously
        {
            get;
        }

        /// <summary>
        /// get the image logo of the site
        /// </summary>
        /// <returns>image logo of the site</returns>
        System.Drawing.Bitmap GetBitmap();
        
        
        /// <summary>
        /// get the image logo of the site for buttons
        /// </summary>
        /// <returns>image logo of the site for buttons</returns>
        System.Drawing.Bitmap GetBitmapButton();
    }
}
