﻿namespace ImageUploader
{
    public class SizProperties : IServerProperties
    {
        /// <summary>
        /// contains the url of the server
        /// </summary>
        public string URL_OF_SERVER
        {
            get { return @"http://www.siz.co.il/"; }
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
            get { return 5; }
        }

        /// <summary>
        /// Contains the maximum number of urls that can be uploaded simultaneously.
        /// </summary>
        public int MaximumNumberOfURLsToUploadSimultaneously
        {
            get { return 0; }
        }

        /// <summary>
        /// get the image logo of the site
        /// </summary>
        /// <returns>image logo of the site</returns>
        public System.Drawing.Bitmap GetBitmap()
        {
            return Properties.Resources.SizLogo;
        }

        /// <summary>
        /// get the image logo of the site
        /// </summary>
        /// <returns>image logo of the site</returns>
        public System.Drawing.Bitmap GetBitmapButton()
        {
            return Properties.Resources.SizLogo;
        }
    }
}
