using System.IO.Compression;
using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Jpeg;
using Aspose.Imaging.ImageOptions;
using Clean.Domain.Enums;
using SeekOrigin = System.IO.SeekOrigin;

namespace Clean.Infrastructure.ServiceImpls.FileHelperImpl;

public class JpegHelper : IImageHelper
{
    public byte[] Compress(byte[] bytes, CompresstionLevel level)
    {
        JpegCompressionMode compressionMode;
        int quality;
        switch (level)
        {
            case CompresstionLevel.MAX_LEVEL:
                quality = 5;
                break;
            case CompresstionLevel.HEAVY_LEVEL:
                quality = 10;
                break;
            case CompresstionLevel.NORMAL_LEVEL:
                quality = 20;
                break;
            case CompresstionLevel.LIGHT_LEVEL:
                quality = 30;
                break;
            case CompresstionLevel.MIN_LEVEL:
                quality = 40;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }

        byte[] result = null;
        using (var outputStream = new MemoryStream())
        {
            using (var inputStream = new MemoryStream(bytes))
            {
                inputStream.Seek(0, SeekOrigin.Begin);
                using (var original = Image.Load(inputStream))
                {
                    var jpegOptions = new JpegOptions()
                    {
                        ColorType = JpegCompressionColorMode.Rgb,
                        CompressionType = JpegCompressionMode.Progressive,
                        Quality = quality
                    };
                    original.Save(outputStream, jpegOptions);
                }
            }

            outputStream.Seek(0, SeekOrigin.Begin);
            result = outputStream.ToArray();
        }

        return result;
    }
}