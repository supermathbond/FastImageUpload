using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ImageUploader
{
    /// <summary>
    /// A static class of accessory functions.
    /// </summary>
    public static class AccessoryFuncs
    {
        /// <summary>
        /// Checks if server is available.
        /// </summary>
        /// <param name="WebServerToCheck"> Web server to check. </param>
        /// <returns>
        /// True - if there is a  connection.
        /// Otherwise it will return false
        /// </returns>
        public static bool CheckForInternetConnection(string WebServerToCheck)
        {
            // Get the url from server properties
            Uri Url = new Uri(WebServerToCheck);

            WebRequest WebReq;
            WebResponse Response;

            // Make an empty request.
            WebReq = WebRequest.Create(Url);

            try
            {
                // Get the response.
                Response = WebReq.GetResponse();
                Response.Close();

                // Server is available.
                return true;
            }
            catch
            {
                // Server is unavailable.               
                return false;
            }
            finally
            {
                WebReq = null;
                Response = null;
                Url = null;
            }
        }

        /// <summary>
        /// Take a snapshot of the screen.
        /// </summary>
        /// <returns> Snapshot of the screen </returns>
        public static Bitmap TakeScreenPicture()
        {
            Rectangle bounds = default(Rectangle);
            System.Drawing.Bitmap screenshot = default(System.Drawing.Bitmap);
            Graphics graph = default(Graphics);
            bounds = Screen.PrimaryScreen.Bounds;
            screenshot = new System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            graph = Graphics.FromImage(screenshot);
            graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }

        /// <summary>
        /// Checks if a url is valid.
        /// </summary>
        /// <param name="thisString"> Url to check. </param>
        /// <returns> Is url. </returns>
        public static bool IsURL(String thisString)
        {
            return Regex.IsMatch(thisString, @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*\/?");
        }

        public static string GetMimeType(string filePath)
        {
            switch (filePath.Substring(filePath.LastIndexOf(".")).ToUpper())
            {
                case ".JPEG": return "image/jpeg"; 
                case ".JPG": return "image/jpeg"; 
                case ".PNG": return "image/png"; 
                case ".GIF": return "image/gif"; 
                case ".TIF": return "image/tiff";
                case ".BMP": return "image/bmp";
                default: throw new Exception("Bad input");
            }
        }

        public static long GetEpochTime()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
