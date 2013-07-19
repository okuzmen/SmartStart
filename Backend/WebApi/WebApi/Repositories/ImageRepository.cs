using System.Drawing;
using ImageTools;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private const string DataFolderConfigKey = "DataFolder";
        private const string FullsizeFolderConfigKey = "FullSizeFolder";
        private const string PreviewFolderConfigKey = "PreviewSizeFolder";

        //could be moved to configuration
        private readonly ImageFormat imageFormat = ImageFormat.Png;

        private readonly string previewsFolder;
        private readonly string fullSizeImagesFolder;
        private readonly string appDataPath;

        public ImageRepository()
        {
            appDataPath = string.Concat("~/", ConfigurationManager.AppSettings[DataFolderConfigKey]);

            previewsFolder = Path.Combine(appDataPath, ConfigurationManager.AppSettings[PreviewFolderConfigKey]);
            fullSizeImagesFolder = Path.Combine(appDataPath, ConfigurationManager.AppSettings[FullsizeFolderConfigKey]);

            if (!Directory.Exists(previewsFolder))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(previewsFolder));
            }

            if (!Directory.Exists(fullSizeImagesFolder))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(fullSizeImagesFolder));
            }
        }

        public ImagePath Add(ImageSource imageSource)
        {
            var id = Guid.NewGuid();
            var img = Base64ToImage(imageSource.Get64BaseString());
            return Add(id, img);
        }


        public void Update(Guid id, Image newImage)
        {
            Add(id, newImage);
        }

        public void Remove(Guid id)
        {
            string fullSize = Path.Combine(fullSizeImagesFolder, string.Concat(id.ToString(), ".", imageFormat));
            string preview = Path.Combine(previewsFolder, string.Concat(id.ToString(), ".", imageFormat));

            if (File.Exists(fullSize))
            {
                File.Delete(fullSize);
            }

            if (File.Exists(preview))
            {
                File.Delete(preview);
            }
        }

        private ImagePath Add(Guid id, Image image)
        {
            var fullSizeImagePath = Path.Combine(fullSizeImagesFolder, string.Concat(id.ToString(), ".", imageFormat));
            var previewSizeImagePath = Path.Combine(previewsFolder, string.Concat(id.ToString(), ".", imageFormat));

            var previewImage = ImageResizer.Resize(image, 200, 0, true);
            image.Save(HttpContext.Current.Server.MapPath(fullSizeImagePath));
            previewImage.Save(HttpContext.Current.Server.MapPath(previewSizeImagePath));

            return new ImagePath
            {
                FullSizeImage = fullSizeImagePath.Remove(0, 2), 
                PreviewSizeImage = previewSizeImagePath.Remove(0, 2)
            };
        }

        private Image Base64ToImage(string encodedImage)
        {
            byte[] imageBytes = Convert.FromBase64String(encodedImage);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }
    }
}
