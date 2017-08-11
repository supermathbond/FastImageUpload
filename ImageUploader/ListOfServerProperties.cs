using System;

namespace ImageUploader
{
    /// <summary>
    /// Array of the properties of the UploadServers
    /// </summary>
    public class ListOfServerProperties
    {
        /// <summary>
        /// array of all _servers
        /// </summary>
        private readonly IServerProperties[] _servers;


        #region Singelton Implementaion

        /// <summary>
        /// Get property for m_Instance. (the single instance of the class)
        /// </summary>
        public static ListOfServerProperties Instance { get; } = new ListOfServerProperties();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ListOfServerProperties() { }

        /// <summary>
        /// private constructor
        /// </summary>
        private ListOfServerProperties()
        {
            // Initialize the array to the enum "UploadServer" his length.
            _servers = new IServerProperties[ Enum.GetValues(typeof(UploadServer)).Length ];

            // Important: this is according to the enum
            _servers[0] = new PostImagesProperties();
            _servers[1] = new PasteBoardProperties();
            _servers[2] = new ImageVenueProperties();
            _servers[3] = new FreeImageHostingProperties();
            _servers[4] = new ImageBinPoperties();
            _servers[5] = new EZPhotoShareProperties();
            _servers[6] = new ZimgProperties();
            _servers[7] = new CwebPixProperties();
            _servers[8] = new PicFrontProperties();
            _servers[9] = new SizProperties();
            _servers[10] = new ImgBBPoperties();
            _servers[11] = new MyGProperties();
        }

        #endregion

        /// <summary>
        /// Get the bitmap for a button. (in the main form)
        /// </summary>
        /// <param name="index"> Index of server. (according to enum.) </param>
        /// <returns> A bitmap for button. </returns>
        public System.Drawing.Bitmap GetBitmapButton(int index)
        {
            return _servers[index].GetBitmapButton();
        }


        /// <summary>
        /// Get the bitmap of server.
        /// </summary>
        /// <param name="index"> Index of server. (according to enum.) </param>
        /// <returns> Bitmap of server. </returns>
        public System.Drawing.Bitmap GetBitmap(int index)
        {
            return _servers[index].GetBitmap();
        }


        /// <summary>
        /// Get url of server.
        /// </summary>
        /// <param name="index"> Index of server. (according to enum.) </param>
        /// <returns> Url of server. </returns>
        public string GetURL(int index)
        {
            return _servers[index].URL_OF_SERVER;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"> Index of server. (according to enum.) </param>
        /// <returns></returns>
        public bool HasURLUpload(int index)
        {
            return _servers[index].CanUploadUrl;
        }


        /// <summary>
        /// Return the amount of _servers.
        /// </summary>
        public int NumberOfServers
        {
            get { return _servers.Length; }
        }
    }
}
