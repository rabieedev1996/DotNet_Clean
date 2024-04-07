using System.Net;
using Amazon.S3;
using Amazon.S3.Transfer;
using HeyRed.Mime;
using Clean.Application.Contract.Services;
using Clean.Domain.Enums;

namespace Clean.Infrastructure.Service;

public class FileService : IFileService
{
    protected Byte[] _byteArray;
    protected string _fileType;
    protected string _contentType;
    protected string _fileName;
    protected string? _path;
    protected bool _loadedStream;
    protected OSTYPE _os;

    private string _amazonAccessKey = "****";
    private string _amazonSecretKey = "****";
    private string _amazonBucketName = "****";
    private string _amazonEndPoint = "****";

    public FileService(OSTYPE os)
    {
        _os = os;
    }

    public void LoadFileFromStorage(string path)
    {
        path = OSPathAdapter(path);
        _path = path;
        _fileType = MimeTypesMap.GetMimeType(path);
        _contentType = Path.GetExtension(path).TrimStart('.');
        _loadedStream = false;
    }

    public void LoadFileFromByteArray(byte[] byteArray, string? fileType = null, string? contentType = null)
    {
        if (!string.IsNullOrEmpty(fileType))
        {
            _contentType = MimeTypesMap.GetMimeType(fileType); // => image/jpeg
            _fileType = fileType;
        }
        else if (!string.IsNullOrEmpty(contentType))
        {
            _fileType = MimeTypesMap.GetExtension(contentType);
            _contentType = contentType;
        }
        else
        {
            throw new Exception("fileType or mimType is empty");
        }

        _byteArray = byteArray;
        _loadedStream = true;
    }

    public void LoadFileFromStream(Stream stream, string? fileType = null, string? contentType = null)
    {
        if (!string.IsNullOrEmpty(fileType))
        {
            _contentType = MimeTypesMap.GetMimeType(fileType); // => image/jpeg
            _fileType = fileType;
        }
        else if (!string.IsNullOrEmpty(contentType))
        {
            _fileType = MimeTypesMap.GetExtension("image/jpeg");
            _contentType = contentType;
        }
        else
        {
            throw new Exception("fileType or mimType is empty");
        }

        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            _byteArray = buffer;
        }

        _loadedStream = true;
    }

    public string SaveToFile(string destinationPath)
    {
        destinationPath = OSPathAdapter(destinationPath);
        if (!_loadedStream)
        {
            PathToStream();
        }

        using (FileStream fileStream = System.IO.File.Create(destinationPath))
        {
            fileStream.Write(_byteArray, 0, _byteArray.Length);
        }

        return destinationPath;
    }

    public string SaveToAWS(string directory = null)
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
      
        if (!string.IsNullOrEmpty(directory))
        {
            directory = directory.TrimEnd("/".ToCharArray()).TrimEnd("\\".ToCharArray());
            directory = directory.TrimStart("/".ToCharArray()).TrimStart("\\".ToCharArray());
        }

        if (!_loadedStream)
        {
            PathToStream();
        }

        AmazonS3Config config = new AmazonS3Config();
        config.ServiceURL = _amazonEndPoint;
        AmazonS3Client s3Client = new AmazonS3Client(
            _amazonAccessKey,
            _amazonSecretKey,
            config
        );
        var key = (directory ?? "") + "/" + Guid.NewGuid().ToString() + "." + _fileType;
        using (var ms = new MemoryStream(_byteArray))
        {
            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                PartSize = _byteArray.Length,
                BucketName = _amazonBucketName,
                InputStream = ms,
                //StorageClass = S3StorageClass.StandardInfrequentAccess,
                Key = key,
                CannedACL = S3CannedACL.PublicRead,
                
            };
            var fileTransferUtility = new TransferUtility(s3Client);
            fileTransferUtility.Upload(fileTransferUtilityRequest);
        }

        return key;
    }

    protected void PathToStream()
    {
        using (var sourceFileStream = new FileStream(_path, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = sourceFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                _byteArray = buffer;
                _loadedStream = true;
            }
        }
    }

    protected string OSPathAdapter(string path)
    {
        switch (_os)
        {
            case OSTYPE.WINDOWS:
                return path.Replace("/", "\\");
            case OSTYPE.LINUX:
                return path.Replace(@"\", "/").Replace(@"\\", "/");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}