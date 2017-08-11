using System;
using System.IO;
using System.Net;
using System.Text;

namespace ImageUploader
{
    public class PostImagesUploader : IUploader
    {
        #region IUploader Members

        /// <summary>
        /// Upload to server
        /// </summary>
        /// <param name="pathOrUrl">the image to upload</param>
        /// <returns>Image URL</returns>
        public string Upload(string pathOrUrl)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            bool isUrl;
            byte[] buffer = generateHttpMultipartRequestBuffer(pathOrUrl, boundary, out isUrl);
            return uploadDataToServer(buffer, boundary, isUrl);
        }

        #endregion

        #region Private Members

        // From website code
        private string rand_string(int length)
        {
            var str = "";
            var possibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (var i = 0; i < length; i++)
                str += possibles[rand.Next(possibles.Length - 1)];
            return str;
        }

        private string getToken()
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(@"http://postimages.org/");
                if (string.IsNullOrEmpty(htmlCode))
                {
                    throw new Exception("Could not reach server");
                }

                int start = htmlCode.IndexOf("token\",\"") + "token\",\"".Length;
                int end = htmlCode.IndexOf("\"", start + 1);
                string token = htmlCode.Substring(start, end - start);

                return token;
            }
        }

        /// <summary>
        /// Generates the buffer which will be sended to send the server via http post-multipart request
        /// </summary>
        /// <param name="file"> Image path. </param>
        /// <param name="boundary"> The boundary to seperate the fields in the multipart form. </param>
        /// <returns> Byte array with all the request content. </returns>
        private byte[] generateHttpMultipartRequestBuffer(string file, string boundary, out bool isUrl)
        {
            // tamplates
            string imageType = AccessoryFuncs.GetMimeType(file);
            const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; \r\n\r\n{1}";
            string headerFileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: " + imageType + "\r\n\r\n";
            string boundaryInContnet = "\r\n--" + boundary + "\r\n";

            using (Stream memStream = new MemoryStream())
            {
                StringBuilder builder = new StringBuilder();
                byte[] contentBytes = Encoding.UTF8.GetBytes(boundaryInContnet);
                memStream.Write(contentBytes, 0, contentBytes.Length);

                isUrl = !File.Exists(file);
                if (!isUrl)
                {
                    string header = string.Format(headerFileTemplate, "Filedata", file.Substring(file.LastIndexOf("\\") + 1));
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
                    builder.Append(string.Format(headerTemplate, "token", getToken()));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "upload_session", rand_string(32)));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "numfiles", "1"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "gallery", ""));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "ui", "24__1920__1080__true__?__?__" + DateTime.Now.ToString("MM/dd/yyyy, h:mm:ss tt") +
                        "__Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36__"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "optsize", "0"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "expire", "0"));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "upload_referer", ""));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "session_upload", AccessoryFuncs.GetEpochTime().ToString()));
                    builder.Append(boundaryInContnet);
                    builder.Append(string.Format(headerTemplate, "forum", ""));
                    builder.Append(boundaryInContnet);
                }
                else // URL
                {
                    // For some reason stopped working. also in browser...
                    isUrl = true;
                    string date = DateTime.Now.ToString("MM/dd/yyyy, h%3Amm%3Ass+tt").Replace("/", "%2F7").Replace(",", "%2C").Replace(" ", "+");
                    var str = "token={0}&upload_session={1}&url={2}&numfiles=1&gallery=&ui=24__1920__1080__true__%3F__%3F__{3}__Mozilla%2F5.0+(Windows+NT+10.0%3B+Win64%3B+x64)+AppleWebKit%2F537.36+(KHTML%2C+like+Gecko)+Chrome%2F59.0.3071.115+Safari%2F537.36__&optsize=0&expire=0&session_upload={4}";
                    builder.Append(string.Format(str, getToken(), rand_string(32), file, date, ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString()));
                }

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
        /// <param name="isUrl"> is given path is url? </param>
        /// <returns> The generated link from server. </returns>
        private string uploadDataToServer(byte[] buffer, string boundary, bool isUrl)
        {
            // Headers
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://postimages.org/json/rr");
            myRequest.Method = "POST";
            myRequest.ContentType = isUrl ? "application/x-www-form-urlencoded; charset=UTF-8" :
                                            "multipart/form-data; boundary=" + boundary;            
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
                // we get the the url of the image  
                int start = htmlCode.IndexOf("url\":\"") + "url\":\"".Length;
                int end = htmlCode.IndexOf("\"", start);
                string url = "http:" + htmlCode.Substring(start, end - start).Replace("\\", "");

                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(url);

                    // we get the the url of the image  
                    int loc = htmlCode.IndexOf("id=\"code_direct\"");
                    start = htmlCode.IndexOf("value=\"", loc) + "value=\"".Length;
                    end = htmlCode.IndexOf("\"", start + "value=\"".Length);
                    string imageUrl = htmlCode.Substring(start, end - start);

                    return imageUrl;
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
