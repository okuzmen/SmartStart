using ImageTools;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.DirectoryServices;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web;
using WebApi.Interfaces;


namespace WebApi.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly string DataFolderConfigKey = "DataFolder";
        private readonly string FullsizeFolderConfigKey = "FullSizeFolder";
        private readonly string PreviewFolderConfigKey = "PreviewSizeFolder";

        //could be moved to configuration
        private ImageFormat storeImageFormat = ImageFormat.Png;

        private string previewsFolder;
        private string fullSizeImagesFolder;
        private readonly string appDataPath;

        public ImageRepository()
        {
            appDataPath = HttpContext.Current.Server.MapPath(
                string.Concat("~/", ConfigurationManager.AppSettings[DataFolderConfigKey]));

            previewsFolder = Path.Combine(appDataPath, ConfigurationManager.AppSettings[PreviewFolderConfigKey]);
            fullSizeImagesFolder = Path.Combine(appDataPath, ConfigurationManager.AppSettings[FullsizeFolderConfigKey]);

            if (!Directory.Exists(previewsFolder))
            {
                Directory.CreateDirectory(previewsFolder);
            }

            if (!Directory.Exists(fullSizeImagesFolder))
            {
                Directory.CreateDirectory(fullSizeImagesFolder);
            }
        }

        public string[] Add(string encodedImage)
        {
            var id = Guid.NewGuid();
            var img = Base64ToImage(encodedImage);

            return Add(id, img);
        }


        public void Update(Guid id, Image newImage)
        {
            Add(id, newImage);
        }

        public void Remove(Guid id)
        {
            string fullSize = Path.Combine(fullSizeImagesFolder, string.Concat(id.ToString(), ".", storeImageFormat));
            string preview = Path.Combine(previewsFolder, string.Concat(id.ToString(), ".", storeImageFormat));

            if (File.Exists(fullSize))
            {
                File.Delete(fullSize);
            }

            if (File.Exists(preview))
            {
                File.Delete(preview);
            }
        }

        private string[] Add(Guid id, Image image)
        {
            string fullSize = Path.Combine(fullSizeImagesFolder, string.Concat(id.ToString(), ".", storeImageFormat));
            string preview = Path.Combine(previewsFolder, string.Concat(id.ToString(), ".", storeImageFormat));

            image.Save(fullSize);
            ImageResizer.Resize(image, 200, 0, true).Save(preview);

            return new string[] { fullSize, preview };
        }

        private Image Base64ToImage(string encodedImage)
        {
            int strL = encodedImage.Length;
            byte[] imageBytes = Convert.FromBase64String(encodedImage);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
