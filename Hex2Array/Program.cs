using System;
using System.IO;

namespace Hex2Array
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Error: Invalid number of arguments.");
                Console.WriteLine("Run  hex2header.exe -h  for a list of valid arguments");
            }

            if (args.Length == 1 && args[0].ToLower().Equals("-h"))
            {
                PrintHelp();
                return;
            }

            var inputFormat = args[0].ToLower();
            var inputFile = args[1];
            var outputFile = inputFile + ".h";

            byte[] image = null;
            using (var input = File.OpenRead(inputFile))
            {
                if (inputFormat.Equals("-fh"))
                    image = new HexParser().BuildImage(input);
                else if (inputFormat.Equals("-fb"))
                    image = new BinParser().BuildImage(input);
            }

            using var output = File.Create(outputFile);
            new HeaderWriter().Run(image, output);
        }

        private static void PrintHelp()
        {
            Console.WriteLine("hex2header -f<input-format> <input-file>");
            Console.WriteLine("   -f[h|b]     input format: 'h' for hex, 'b' for binary");
            Console.WriteLine("   input-file  path and filename of input file");
            Console.WriteLine("   -h          print this help");
        }
    }
}
