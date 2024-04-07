using Clean.Domain.Enums;

namespace Clean.Infrastructure.ServiceImpls.FileHelperImpl;

public interface IImageHelper
{
    byte[] Compress(byte[] bytes, CompresstionLevel level);
}