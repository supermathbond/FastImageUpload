namespace ImageUploader
{
    public interface IUploader
    {
        /// <summary>
        /// Upload to server
        /// </summary>
        /// <param name="pathOrUrl">the image to upload</param>
        /// <returns>Image URL</returns>
        string Upload(string pathOrUrl);
    }
}
