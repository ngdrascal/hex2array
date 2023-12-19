using System.IO;

namespace Hex2Array
{
    public interface IParser
    {
        byte[] BuildImage(Stream input);
    }
}
