using System.IO;

namespace Hex2Array
{
    public class HeaderWriter
    {
        public void Run(byte[] image, Stream output)
        {
            using var writer = new StreamWriter(output);

            writer.WriteLine("const unsigned char program[] = {");
            writer.WriteLine("   /*            0     1     2     3     4     5     6     7     8     9     A     B     C     D     E     F */");
            var outputLine = "   /* 000x */ ";
            for (var i = 0; i < image.Length; i++)
            {
                outputLine += ByteToHex(image[i]);
                if (i != image.Length - 1)
                    outputLine += ", ";

                if ((i + 1) % 16 == 0 || i == image.Length - 1)
                {
                    writer.WriteLine(outputLine);
                    outputLine = "   /* " + IntToHex(i + 1) + " */ ";
                }
            }

            writer.WriteLine("};");
            writer.Flush();
        }

        private string ByteToHex(byte value)
        {
            return $"0x{value:X2}";
        }

        private string IntToHex(int value)
        {
            var hexStr = value.ToString("X4");
            return $"{hexStr[..3]}x";
        }
    }
}
