using System.IO;
using System;

namespace FlatDir.Model
{
    internal class MainArgs
    {
        internal readonly string tempDir;
        internal readonly string targetDir;

        public MainArgs(string[] args)
        {
            if (args.Length == 2)
            {
                if (Directory.Exists(args[0]))
                {
                    tempDir = args[0];
                }
                else
                {
                    Console.WriteLine(String.Concat("Cannot access \"", args[0], "\""));
                    Console.ReadLine();
                    Environment.Exit(1);
                }

                if (Directory.Exists(args[1]))
                {
                    targetDir = args[1];
                }
                else
                {
                    Console.WriteLine(String.Concat("Cannot access \"", args[1], "\""));
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.WriteLine("Two directory arguments are required:");
                Console.WriteLine("    First directory for temporary files.");
                Console.WriteLine("    Second directory for deduplication.");
                Console.WriteLine();
                Console.WriteLine("Usage: FlatDir.exe \"C:\\Users\\Public\\Temp\" \"C:\\Users\\Public\\Pictures\"");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }
    }
}
