using System;
using System.IO;
using System.Linq;

namespace FlatDir.Method
{
    internal static class TestFiles
    {
        public static void Generate(string tempDir)
        {
            string filesystemChars = String.Concat(
                (char)34, // double quote "
                (char)42, // asterisk *
                (char)47, // forward slash /
                (char)58, // colon :
                (char)60, // less than <
                (char)62, // greater than >
                (char)63, // question mark ?
                (char)92, // back slash \
                (char)124 // pipe |
            );
            char character = (char)32;
            string content = null;
            string filename = null;
            for (int i = 32; i < 256; i++)
            {
                if (filesystemChars.Contains((char)i))
                {
                    character = (char)i;
                    content = character.ToString();
                    Console.WriteLine(String.Concat(i, " SKIPPED ", content));
                }
                else
                {
                    character = (char)i;
                    content = character.ToString();
                    Console.WriteLine(String.Concat(i, " ", content));
                    filename = String.Concat("test", i, "char", content, "char.txt");
                    File.WriteAllText(String.Concat(tempDir, "\\", filename), content);
                }
            }
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
