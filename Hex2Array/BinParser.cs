using System.Collections.Generic;
using System.IO;

namespace Hex2Array
{
    public class BinParser : IParser
    {
        public byte[] BuildImage(Stream input)
        {
            var image = new List<byte>();

            var value = input.ReadByte();
            while (value != -1)
            {
                image.Add((byte)value);
                value = input.ReadByte();
            }

            return image.ToArray();
        }
    }
}
