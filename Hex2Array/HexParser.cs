using System.Collections.Generic;
using System.IO;

namespace Hex2Array
{
    public class HexParser : IParser
    {
        public byte[] BuildImage(Stream input)
        {
            var inputLines = LoadInput(input);

            var parsedInput = ParseInput(inputLines);

            var bufferSize = CalculateBufferSize(parsedInput);
            var buffer = new byte[bufferSize];

            FillBuffer(parsedInput, buffer);

            return buffer;
        }

        private List<string> LoadInput(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());
            }

            return lines;
        }

        private List<HexRecord> ParseInput(List<string> inputLines)
        {
            var parsedInput = new List<HexRecord>();
            foreach (var line in inputLines)
            {
                if (!string.IsNullOrEmpty(line))
                    parsedInput.Add(ParseLine(line));
            }

            return parsedInput;
        }

        private HexRecord ParseLine(string line)
        {
            var parsedLine = new HexRecord
            {
                StartCode = line[0],
                ByteCount = HexString2Byte(line.Substring(1, 2)),
                Address = HexString2Int(line.Substring(3, 4)),
                RecordType = HexString2Byte(line.Substring(7, 2)),
                Data = ParseData(line.Substring(9, line.Length - 11)),
                CheckSum = HexString2Byte(line.Substring(line.Length - 2, 2))
            };

            return parsedLine;
        }

        private byte[] ParseData(string data)
        {
            var byteCount = data.Length / 2;
            var buffer = new byte[byteCount];

            for (var i = 0; i < byteCount; i++)
                buffer[i] = HexString2Byte(data.Substring(i * 2, 2));

            return buffer;
        }

        private int CalculateBufferSize(List<HexRecord> cache)
        {
            var lastAddress = 0;
            foreach (var line in cache)
            {
                var endAddress = line.Address + line.ByteCount;

                if (endAddress > lastAddress)
                    lastAddress = endAddress;
            }

            return lastAddress;
        }

        private void FillBuffer(List<HexRecord> lines, byte[] buffer)
        {
            foreach (var line in lines)
            {
                var address = line.Address;
                foreach (var value in line.Data)
                {
                    buffer[address] = value;
                    address++;
                }
            }
        }

        private int HexString2Int(string hexStr)
        {
            return int.Parse(hexStr, System.Globalization.NumberStyles.HexNumber);
        }

        private byte HexString2Byte(string hexStr)
        {
            return byte.Parse(hexStr, System.Globalization.NumberStyles.HexNumber);

        }
    }
}
