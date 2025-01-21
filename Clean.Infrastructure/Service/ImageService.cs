using System.IO.Compression;
using Aspose.Imaging;
using Clean.Application.Contract.Services;
using Clean.Domain;
using Clean.Domain.Enums;
using Clean.Infrastructure.ServiceImpls.FileHelperImpl;
using SeekOrigin = System.IO.SeekOrigin;

namespace Clean.Infrastructure.Service;

public class ImageService : FileService,IImageService
{
    public ImageService(Configs configs) : base(configs)
    {
        License imagingLicense = new License();
        using (var licenseStream = new FileStream(OSPathAdapter(Environment.CurrentDirectory + "/aspose.lic"),
                   FileMode.Open,
                   FileAccess.Read))
        {
            imagingLicense.SetLicense(licenseStream);
        };
    }

    public void Compress(CompresstionLevel level)
    {
        if (!_loadedStream)
        {
            base.PathToStream();
        }

        IImageHelper imageHelper;
        switch (_fileType.ToLower())
        {
            case "jpg":
                imageHelper = new JpegHelper();
                break;
            case "jpeg":
                imageHelper = new JpegHelper();
                break;
            case "png":
                imageHelper = new PngHelper();
                break;
            case "tiff":
                imageHelper = new TiffHelper();
                break;
            default:
                throw new Exception("image type not support");
        }

        _byteArray = imageHelper.Compress(_byteArray, level);
    }

    public void Resize(int width, int height)
    {
        License imagingLicense = new License();
        using (var licenseStream = new FileStream(OSPathAdapter(Environment.CurrentDirectory + "/aspose.lic"),
                   FileMode.Open,
                   FileAccess.Read))
        {
            imagingLicense.SetLicense(licenseStream);
        }

        using (var outputStream = new MemoryStream())
        {
            using (var inputStream = new MemoryStream(_byteArray))
            {
                using (var image = Image.Load(inputStream))
                {
                    // Resize the image using the resize type AdaptiveResample
                    image.Resize(width, height, ResizeType.AdaptiveResample);
                    // Save the resized image to disk in the desired format
                    image.Save(outputStream);
                }
            }

            outputStream.Seek(0, SeekOrigin.Begin);
            _byteArray = outputStream.ToArray();
        }
    }

    public void Resize(double factor)
    {
        using (var outputStream = new MemoryStream())
        {
            using (var inputStream = new MemoryStream(_byteArray))
            {       
                inputStream.Seek(0, SeekOrigin.Begin);
                using (var image = Image.Load(inputStream))
                {
                    // Resize the image using the resize type AdaptiveResample
                    image.Resize((int)(image.Width * factor), (int)(image.Height * factor), ResizeType.AdaptiveResample);
                    // Save the resized image to disk in the desired format
                    image.Save(outputStream);
                }
            }

            outputStream.Seek(0, SeekOrigin.Begin);
            _byteArray = outputStream.ToArray();
        }
    }
}