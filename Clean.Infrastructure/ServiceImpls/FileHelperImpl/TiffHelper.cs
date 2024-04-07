using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Tiff.Enums;
using Aspose.Imaging.ImageOptions;
using Clean.Domain.Enums;
using SeekOrigin = System.IO.SeekOrigin;

namespace Clean.Infrastructure.ServiceImpls.FileHelperImpl;

public class TiffHelper:IImageHelper
{
    public byte[] Compress(byte[] bytes, CompresstionLevel level)
    {
        byte[] result = null;
        using (var outputStream = new MemoryStream())
        {
            using (var inputStream = new MemoryStream(bytes))
            {
                using (var original = Image.Load(inputStream))
                {
                    TiffOptions outputSettings = new TiffOptions(TiffExpectedFormat.Default);
                    outputSettings.BitsPerSample = new ushort[] { 4 };
                    outputSettings.Compression = TiffCompressions.Lzw;
                    outputSettings.Photometric = TiffPhotometrics.Palette;
                    outputSettings.Palette = ColorPaletteHelper.Create4BitGrayscale(false);
                    original.Save(outputStream, outputSettings);
                }
            }
            outputStream.Seek(0, SeekOrigin.Begin);
            result= outputStream.ToArray();
        }
        return result;
    }

}