using System;
using System.IO;
using System.Net;
using System.Text;

namespace ImageUploader
{
    public class EZPhotoShareUploader : IUploader
    {
        #region IUploader Members
        
        /// <summary>
        /// Upload to server
        /// </summary>
        /// <param name="pathOrUrl">the image to upload</param>
        /// <returns>Image URL</returns>
        public string Upload(string pathOrUrl)
        {
            fetchDataFromServerAndFillClassFields();
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] buffer = generateHttpMultipartRequestBuffer(pathOrUrl, boundary);
            return uploadDataToServer(buffer, boundary);
        }

        #endregion

        #region Private Members

        private string _cookie;
        private string _authToken;

        private void fetchDataFromServerAndFillClassFields()
        {
            using (WebClient client = new WebClient())
            {
                string siteCode = client.DownloadString("https://www.ezphotoshare.com");
                string cookie = client.ResponseHeaders["Set-Cookie"];
                _cookie = cookie.Substring(0, cookie.IndexOf(";") + 1);
                int start = siteCode.IndexOf("PF.obj.config.auth_token = \"") + "PF.obj.config.auth_token = \"".Length;
                int end = siteCode.IndexOf("\"", start);
                _authToken = siteCode.Substring(start, end - start);
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
            // tamplates
            string imageType = AccessoryFuncs.GetMimeType(file);
            const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; \r\n\r\n{1}";
            string headerFileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: " + imageType + "\r\n\r\n";
            string startBoundaryInContnet = "--" + boundary + "\r\n";
            string boundaryInContnet = "\r\n--" + boundary + "\r\n";
            string endBoundaryInContnet = "\r\n--" + boundary + "--\r\n";

            bool isUrl = !File.Exists(file);

            using (Stream memStream = new MemoryStream())
            {                
                byte[] contentBytes = Encoding.UTF8.GetBytes(startBoundaryInContnet);
                memStream.Write(contentBytes, 0, contentBytes.Length);

                StringBuilder builder = new StringBuilder();
                if (!isUrl)
                {
                    // the file.
                    string header = string.Format(headerFileTemplate, "source",
                        file.Substring(file.LastIndexOf("\\") + 1));
                    byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[10240];
                        int bytesRead = 0;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            memStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "source", "null"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "type", "file"));
                }
                else
                {
                    builder.Append(string.Format(headerTemplate, "source", file));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "type", "url"));
                }
                
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "action", "upload"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "privacy", "public"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "timestamp", AccessoryFuncs.GetEpochTime()));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "auth_token", _authToken));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "category_id", "null"));
                builder.Append(boundaryInContnet);
                builder.Append(string.Format(headerTemplate, "nsfw", "0"));
                builder.Append(endBoundaryInContnet);
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
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://www.ezphotoshare.com/json");
            myRequest.Method = "POST";
            myRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            myRequest.KeepAlive = true;
            myRequest.ContentLength = buffer.Length;
            myRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            myRequest.Headers.Add("Cookie", _cookie + " _ga=GA1.2.478396703.1502401452; _gid=GA1.2.822785749.1502401452; _gat=1");
            myRequest.Referer = "https://www.ezphotoshare.com/";
            

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
                // we get the the url of the image 
                int start = htmlCode.IndexOf("\"url\":\"") + "\"url\":\"".Length;
                int end = htmlCode.IndexOf("\"", start);
                string pageUrl = htmlCode.Substring(start, end - start).Replace("\\", "");

                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(pageUrl);
                    start = htmlCode.IndexOf("[img]") + "[img]".Length;
                    end = htmlCode.IndexOf("[/img]", start);
                    return htmlCode.Substring(start, end - start).Replace("\\", "");
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
