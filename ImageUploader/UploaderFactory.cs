using System;

namespace ImageUploader
{
    /// <summary>
    /// Factory for Uploading.
    /// </summary>
    public static class UploaderFactory
    {
        /// <summary>
        /// Start uploadiing to site.
        /// </summary>
        /// <param name="server"> Server to upload (An enum). </param>
        /// <param name="pathOrUrl"> Path of file. (or URL if the site suppot it) </param>
        /// <returns>
        /// The URL of the image.
        /// </returns>
        public static string StartUpload(UploadServer server, string pathOrUrl)
        {
            IUploader uploader;

            switch (server)
            {
                case UploadServer.PasteBoard:
                    {
                        // upload to PasteBoard
                        uploader = new PasteBoardUploader();
                    }
                    break;
                case UploadServer.EZPhotoShare:
                    {
                        // upload to ImgBB
                        uploader = new EZPhotoShareUploader();
                    }
                    break;
                case UploadServer.FreeImageHosting:
                    {
                        // upload to FreeImageHosting
                        uploader = new FreeImageHostingUploader();                            
                    }
                    break;
                case UploadServer.ImageBin:
                    {
                        // upload to ImageCross
                        uploader = new ImageBinUploader();
                    }
                    break;
                case UploadServer.PostImage:
                    {
                        // upload to EZPicShare
                        uploader = new PostImagesUploader();
                    }
                    break;
                case UploadServer.PicFront:
                    {
                        // upload to PicFront
                        uploader = new PicFrontUploader();
                    }
                    break;
                case UploadServer.ImageVenue:
                    {
                        // upload to ImageVenue
                        uploader = new ImageVenueUploader();
                    }
                    break;
                case UploadServer.Zimg:
                    {
                        // upload to Folderized
                        uploader = new ZimgUploader();
                    }
                    break;
                case UploadServer.CwebPix:
                    {
                        // upload to ImageDoll
                        uploader = new CwebPixUploader();
                    }
                    break;
                case UploadServer.Siz:
                    {
                        // upload to Siz
                        uploader = new SizUploader();
                    }
                    break;
                case UploadServer.ImgBB:
                    {
                        // upload to Up2Me
                        uploader = new ImgBBUploader();
                    }
                    break;
                case UploadServer.MyG:
                    {
                        // upload to MyG
                        uploader = new MyGUploader();
                    }
                    break;
                default: throw new Exception("server should be selected");
            }

            // Upload file.
            return uploader.Upload(pathOrUrl);
        }
    }
}
