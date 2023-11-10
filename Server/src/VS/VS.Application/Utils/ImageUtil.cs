using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace VS.Application.Utils
{
    public class ImageUtil : IDisposable
    {
        private Image _img;

        public ImageUtil(Stream input)
        {
            input.Position = 0;
            _img = Image.Load(input);
        }

        public ImageUtil(byte[] input)
        {
            _img = Image.Load(input);
        }

        /// <summary>
        /// Height and width of image.
        /// </summary>
        public (int, int) Resolution => (_img.Height, _img.Width);

        /// <summary>
        /// Width of image.
        /// </summary>
        /// <value></value>
        public int Width => _img.Width;

        /// <summary>
        /// Height of image.
        /// </summary>
        /// <value></value>
        public int Height => _img.Height;

        public void Dispose()
        {
            if (_img != null)
            {
                _img.Dispose();
                _img = null;
            }
        }

        /// <summary>
        /// Crop image.
        /// </summary>
        /// <param name="height">New height</param>
        /// <param name="width">New width</param>
        public void Crop(int height, int width)
        {
            _img.Mutate(_ => _.Resize(width, height));
        }

        /// <summary>
        /// Change resolution of image.
        /// </summary>
        /// <param name="height">New height</param>
        /// <param name="width">New width</param>
        public void Resize(int height, int width)
        {
            _img.Mutate(_ => _.Resize(width, height));

        }

        /// <summary>
        /// Get gif bytes of image.
        /// </summary>
        /// <returns></returns>
        public byte[] ToGif()
        {
            using (var stream = new MemoryStream())
            {
                _img.SaveAsGif(stream);
                return StreamToBytes(stream);
            }
        }

        /// <summary>
        /// Get png bytes of image.
        /// </summary>
        /// <param name="compressionLevel">Compression (level 1-9). Defaults to 9.</param>
        /// <returns>Bytes of operation.</returns>
        public byte[] ToPng(PngCompressionLevel pngCompressionLevel = PngCompressionLevel.Level9)
        {
            using (var stream = new MemoryStream())
            {
                _img.SaveAsPng(stream, new PngEncoder
                {
                    CompressionLevel = pngCompressionLevel
                });
                return StreamToBytes(stream);
            }
        }

        /// <summary>
        /// Get bmp bytes of image.
        /// </summary>
        /// <returns>Bytes of operation.</returns>
        public byte[] ToBmp()
        {
            using (var stream = new MemoryStream())
            {
                _img.SaveAsBmp(stream);
                return StreamToBytes(stream);
            }
        }

        /// <summary>
        /// Get jpeg bytes of image.
        /// </summary>
        /// <param name="quality">Image quality (level 1-100). Defaults to 100.</param>
        /// <returns>Bytes of operation.</returns>
        public byte[] ToJpeg(int quality = 100)
        {
            using (var stream = new MemoryStream())
            {
                _img.SaveAsJpeg(stream, new JpegEncoder
                {
                    Quality = quality
                });
                return StreamToBytes(stream);
            }
        }

        /// <summary>
        /// Get image format.
        /// </summary>
        /// <param name="input">Stream with image.</param>
        /// <returns>Image format.</returns>
        public static string GetImageFormat(Stream input)
        {
            return Image.DetectFormat(input).Name;
        }

        /// <summary>
        /// Check image in the input stream.
        /// </summary>
        /// <param name="input">Stream with image.</param>
        /// <returns>Bool result of the operation.</returns>
        public static bool IsImage(Stream input)
        {
            return Image.DetectFormat(input) != null;
        }

        /// <summary>
        /// Check image in the input bytes.
        /// </summary>
        /// <param name="input">Stream with image.</param>
        /// <returns>Bool result of the operation.</returns>
        public static bool IsImage(byte[] input)
        {
            return Image.DetectFormat(input) != null;
        }

        private static byte[] StreamToBytes(Stream input)
        {
            input.Position = 0;
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
