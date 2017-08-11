using System;
using System.IO;
using System.Net;
using System.Text;

namespace ImageUploader
{
    public class PicFrontUploader : IUploader
    {
        #region IUploader Members

        /// <summary>
        /// Upload to server
        /// </summary>
        /// <param name="PathOrUrl">the image to upload</param>
        /// <returns>Image URL</returns>
        public string Upload(string PathOrUrl)
        {
            FetchDataFromWebSite();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] buffer = generateHttpMultipartRequestBuffer(PathOrUrl, boundary);
            return uploadDataToServer(buffer, boundary);
        }

        #endregion

        #region Private Members

        // Parameters of the website from the html code.
        private string Sid;
        private string HostWww;
        private string HostEnd;
        private string UploadIdentifier;
        private string AddressToUpload;


        /// <summary>
        /// Get parameters from website html.
        /// </summary>
        private void FetchDataFromWebSite()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string htmlCode = client.DownloadString(@"http://picfront.org/");

                    if ((client == null) || string.IsNullOrEmpty(htmlCode))
                    {
                        throw new Exception("Could not access server.");
                    }

                    // Get action adderss.
                    int loc = htmlCode.IndexOf("form enctype=\"multipart/form-data\"");
                    int start = htmlCode.IndexOf("action=\"", loc) + "action=\"".Length;
                    int end = htmlCode.IndexOf("\"", start);

                    AddressToUpload = htmlCode.Substring(start, end - start);

                    // Get SID.
                    loc = htmlCode.IndexOf("name=\"SID\"");
                    start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                    end = htmlCode.IndexOf("\"", start);

                    Sid = htmlCode.Substring(start, end - start);

                    // Get Host_WWW
                    loc = htmlCode.IndexOf("name=\"HOST_WWW\"");
                    start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                    end = htmlCode.IndexOf("\"", start);

                    HostWww = htmlCode.Substring(start, end - start);

                    // Get Host_End
                    loc = htmlCode.IndexOf("name=\"HOST_END\"");
                    start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                    end = htmlCode.IndexOf("\"", start);

                    HostEnd = htmlCode.Substring(start, end - start);

                    //Get Upload Identifier.
                    loc = htmlCode.IndexOf("name=\"UPLOAD_IDENTIFIER\"");
                    start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                    end = htmlCode.IndexOf("\"", start);

                    UploadIdentifier = htmlCode.Substring(start, end - start);
                }
            }
            catch
            {
                throw new Exception("Error while uploading file. Server might change API.");
            }
        }

        /// <summary>
        /// Generates the buffer which will be sended to send the server via http post-multipart request
        /// </summary>
        /// <param name="file"> Image path. </param>
        /// <param name="boundary"> The boundary to seperate the fields in the multipart form. </param>
        /// <returns> Byte array with all the request content. </returns>
        private byte[] generateHttpMultipartRequestBuffer(string file, string boundary)
        {
            string boundaryInContnet = "\r\n--" + boundary + "\r\n";
            using (Stream memStream = new MemoryStream())
            {
                // tamplates
                string imageType = AccessoryFuncs.GetMimeType(file);
                const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; \r\n\r\n{1}";
                string headerFileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: " + imageType + "\r\n\r\n";
                const string headerEmptyFileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"\"\r\nContent-Type:  application/octet-stream\r\n\r\n";
                byte[] contentBytes;

                StringBuilder builder = new StringBuilder();
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "SID", Sid));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "HOST_WWW", HostWww));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "HOST_END", HostEnd));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "UPLOAD_IDENTIFIER", UploadIdentifier));
                builder.Append(boundaryInContnet);

                if (File.Exists(file)) // Upload file.
                {
                    builder.Append(string.Format(headerEmptyFileTemplate, "file_1"));
                    builder.Append(boundaryInContnet);
                    contentBytes = Encoding.UTF8.GetBytes(builder.ToString());
                    memStream.Write(contentBytes, 0, contentBytes.Length);
                    builder.Length = 0; // Truncate StringBuilder

                    // file contains the path of the file.
                    string header = string.Format(headerFileTemplate, "file_0", file.Substring(file.LastIndexOf("\\") + 1));
                    byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            memStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "urlimage", ""));
                    builder.Append(boundaryInContnet);
                }
                else // Upload URL.
                {
                    builder.Append(string.Format(headerEmptyFileTemplate, "file_0"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "urlimage", ""));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "urluploadfile_0", file));
                    builder.Append(boundaryInContnet);
                }

                builder.Append(string.Format(headerTemplate, "x", "30"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "y", "26"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "resize_x", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "resize_y", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerEmptyFileTemplate, "branding_image"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "branding_urlimage", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "branding_text", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "thumbbar", "1"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "rotate", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "mirror", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "colormode_do", "1"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "camerashakestep", "1"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "folder", "0"));
                builder.Append(boundaryInContnet);
                contentBytes = Encoding.UTF8.GetBytes(builder.ToString());
                memStream.Write(contentBytes, 0, contentBytes.Length);

                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                return tempBuffer;
            }
        }

        /// <summary>
        /// Uploads the byte array buffer to server via HTTP Post request (multipart).
        /// </summary>
        /// <param name="buffer"> Byte array content. </param>
        /// <param name="boundary"> The boundary to seperate the fields in the multipart form. </param>
        /// <returns> The generated link from server. </returns>
        private string uploadDataToServer(byte[] buffer, string boundary)
        {
            // Headers
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(AddressToUpload);
            myRequest.Method = "POST";
            myRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            myRequest.KeepAlive = true;
            myRequest.ContentLength = buffer.Length;

            using (Stream requestStream = myRequest.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                WebResponse webResponse = myRequest.GetResponse();
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string htmlCode = reader.ReadToEnd().Trim();
                    webResponse.Close();

                    // Checks if there was an eror during he upload.
                    if (htmlCode.Contains("error") || htmlCode.Contains("failed"))
                        throw new Exception("Error while uploading file");

                    // Get the url from the html code.
                    return GetImageFromHTML(htmlCode);
                }
            }
        }


        /// <summary>
        /// get the URL of the image from the response
        /// </summary>
        /// <param name="htmlCode">the html code from the response</param>
        private string GetImageFromHTML(string htmlCode)
        {
            try
            {
                // we get the the url of the image  
                int loc = htmlCode.IndexOf("Image Page:");
                int start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                int end = htmlCode.IndexOf("\"", start);
                string pageUrl = htmlCode.Substring(start, end - start);

                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(pageUrl);

                    if ((client == null) || string.IsNullOrEmpty(htmlCode))
                    {
                        throw new Exception("Error while uploading file");
                    }

                    loc = htmlCode.IndexOf("id=\"pic_div\"");
                    start = htmlCode.IndexOf("src=\"", loc) + "src=\"".Length;
                    end = htmlCode.IndexOf("\"", start);
                    pageUrl = htmlCode.Substring(start, end - start);

                    htmlCode = client.DownloadString(pageUrl);

                    if ((client == null) || string.IsNullOrEmpty(htmlCode))
                    {
                        throw new Exception("Error while uploading file");
                    }

                    start = htmlCode.IndexOf("src=\"") + "src=\"".Length;
                    end = htmlCode.IndexOf("\"", start);
                    return htmlCode.Substring(start, end - start);
                }
            }
            catch
            {
                throw new Exception("Error while uploading file");
            }
        }

        #endregion
    }
}
