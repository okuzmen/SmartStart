using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageTools
{
    public static class ImageResizer
    {
        /// <summary>
        /// Specifies the level of compression for an image.
        /// The range of useful values for the quality category is from 0 to 100. 
        /// The lower the number specified, the higher the compression and therefore the lower the quality of the image. 
        /// Zero would give you the lowest quality image and 100 the highest.
        /// </summary>
        private const byte ImageQuality = 100;

        #region Public
        /// <summary>
        /// Method to resize the image.
        /// </summary>
        /// <param name="image">Bitmap image.</param>
        /// <param name="maxWidth">Resize width.</param>
        /// <param name="maxHeight">Resize height.</param>
        /// <param name="preserveAspectRation">Defines if a proportional relationship between width and height should be preserved.
        /// If set to true, the maxHeight parameter is ignored and height will be adjusted automatically.</param>
        /// <returns>A resized bitmap image.</returns>
        public static Image Resize(Image image, int maxWidth, int maxHeight, bool preserveAspectRation)
        {
            if (maxWidth <= 0)
            {
                throw new ArgumentException("Width should be greater than 0");
            }

            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;
            int newWidth = maxWidth;
            int newHeight = maxHeight;

            if (preserveAspectRation)
            {
                // To preserve the aspect ratio
                float ratio = (float)maxWidth / (float)originalWidth;

                // New width and height based on aspect ratio
                newWidth = (int)(originalWidth * ratio);
                newHeight = (int)(originalHeight * ratio);
            }
            else
            {
                if (maxHeight <= 0)
                {
                    throw new ArgumentException("Height should be greater than 0"); 
                }
            }

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Creates a resized copy of an image. 
        /// </summary>
        /// <param name="originalPath">The path to image which should be resized.</param>
        /// <param name="resultPath">The path for the resized image.</param> 
        /// <param name="width">Desired width.</param>
        /// <param name="height">Desired height.</param>
        /// <param name="preserveAspectRation">Defines if a proportional relationship between width and height should be preserved.
        /// If set to true, the maxHeight parameter is ignored and height will be adjusted automatically.</param>
        /// <param name="resultFileType">The image file type that should be use while saving.</param>
        public static void ResizeImage(string originalPath, string resultPath, int width, int height, ImageFormat resultFileType, bool preserveAspectRation)
        {
            // Load the original image
            Image original = new Bitmap(originalPath);

            // Resize it with provided width and height
            Image resizedImage = Resize(original, width, height, preserveAspectRation);

            // Get an ImageCodecInfo object that represents the provided codec.
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(resultFileType);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image with provided file type specifying quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, ImageQuality);
            encoderParameters.Param[0] = encoderParameter;
            resizedImage.Save(resultPath, imageCodecInfo, encoderParameters);

            // Free the resources
            original.Dispose();
            resizedImage.Dispose();
        }
        #endregion


        #region Private 
        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format.</param>
        /// <returns>image codec info.</returns>
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        #endregion
    }
}
