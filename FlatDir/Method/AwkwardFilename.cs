using System;
using System.IO;

namespace FlatDir.Method
{
    internal static class AwkwardFilename
    {
        public static readonly string FORMAT_ISO_8601 = "yyyy-MM-ddTHHmmssFFF";

        public static string Normalize(string input)
        {
            string awkwardArray = String.Concat(
                (char)127, (char)128, (char)129,
                (char)130, (char)131, (char)132, (char)133, (char)134, (char)135, (char)136, (char)137, (char)138, (char)139,
                (char)140, (char)141, (char)142, (char)143, (char)144, (char)145, (char)146, (char)147, (char)148, (char)149,
                (char)150, (char)151, (char)152, (char)153, (char)154, (char)155, (char)156, (char)157, (char)158, (char)159
            );

            string filename = Path.GetFileName(input);
            foreach (char awkwardCharacter in awkwardArray)
            {
                filename = filename.Replace(awkwardCharacter, (char)32);
            }

            filename = filename.Trim((char)32, (char)160);

            if (filename.Length == 0)
            {
                string random = RandomString.Create(8);
                filename = String.Concat("!", random);

            }

            return filename;
        }
    }
}
