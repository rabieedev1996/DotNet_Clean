using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;
using Clean.Domain.Enums;
using SeekOrigin = System.IO.SeekOrigin;

namespace Clean.Infrastructure.ServiceImpls.FileHelperImpl;

public class PngHelper : IImageHelper
{
    public byte[] Compress(byte[] bytes, CompresstionLevel level)
    {
        int compresstionLevel = 0;
        switch (level)
        {
            case CompresstionLevel.MAX_LEVEL:
                compresstionLevel = 9;
                break;
            case CompresstionLevel.HEAVY_LEVEL:
                compresstionLevel = 7;
                break;
            case CompresstionLevel.NORMAL_LEVEL:
                compresstionLevel = 5;
                break;
            case CompresstionLevel.LIGHT_LEVEL:
                compresstionLevel =3;
                break;
            case CompresstionLevel.MIN_LEVEL:
                compresstionLevel = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }

        byte[] result = null;
        using (var outputStream = new MemoryStream())
        {
            using (var inputStream = new MemoryStream(bytes))
            {
                using (var image = PngImage.Load(inputStream))
                {
                    // Create an instance of PngOptions for each resultant PNG, Set CompressionLevel and  Save result on disk
                    PngOptions options = new PngOptions();
                    options.CompressionLevel = compresstionLevel;
                    options.Progressive = true;
                    options.Palette = ColorPaletteHelper.GetCloseImagePalette((RasterImage)image, 1 << 5);
                    options.ColorType = PngColorType.TruecolorWithAlpha;
                    ((PngImage)image).Save(outputStream,options);
                }
            }

            outputStream.Seek(0, SeekOrigin.Begin);
            result = outputStream.ToArray();
        }

        return result;
    }

    public byte[] Resize(byte[] bytes, int width, int height)
    {
        throw new NotImplementedException();
    }
}