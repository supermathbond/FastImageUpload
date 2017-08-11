using System;
using System.IO;
using System.Net;
using System.Text;

namespace ImageUploader
{
    public class ImageBinUploader : IUploader
    {
        #region IUploader Members

        /// <summary>
        /// Upload to server
        /// </summary>
        /// <param name="PathOrUrl">the image to upload</param>
        /// <returns>Image URL</returns>
        public string Upload(string PathOrUrl)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] buffer = generateHttpMultipartRequestBuffer(PathOrUrl, boundary);
            return uploadDataToServer(buffer, boundary);
        }

        #endregion

        #region Private Members

        private string getProgressKey()
        {
            using (WebClient client = new WebClient())
            {
                string siteCode = client.DownloadString("https://imagebin.ca");
                int loc = siteCode.IndexOf("id=\"progress_key\"");
                int start = siteCode.IndexOf("value=\"", loc) + "value=\"".Length;
                int end = siteCode.IndexOf("\"", start);
                return siteCode.Substring(start, end - start);
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
                byte[] contentBytes;

                StringBuilder builder = new StringBuilder();
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "MAX_FILE_SIZE", "15000000"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "APC_UPLOAD_PROGRESS", getProgressKey()));
                builder.Append(boundaryInContnet);
                contentBytes = Encoding.UTF8.GetBytes(builder.ToString());
                memStream.Write(contentBytes, 0, contentBytes.Length);

                // file contains the path of the file.
                string header = string.Format(headerFileTemplate, "file", file.Substring(file.LastIndexOf("\\") + 1));
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

                builder.Length = 0; // truncate stringBuilder.
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "title", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "description", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "tags", ""));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "category", "general"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "private", "false"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "submit", ""));
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
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://imagebin.ca/upload.php");
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
                    if (htmlCode.Contains("error"))
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
                return htmlCode.Replace("url:", "");
            }
            catch
            {
                throw new Exception("Error while uploading file");
            }
        }

        #endregion
    }
}
