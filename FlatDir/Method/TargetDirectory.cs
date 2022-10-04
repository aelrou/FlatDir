using System.Collections.Generic;
using System.IO;

namespace FlatDir.Method
{
    internal static class TargetDirectory
    {
        public static List<string> Explore(string targetDir, List<string> filepathList)
        {
            string[] filepathArray = Directory.GetFiles(targetDir);
            foreach (string filepath in filepathArray)
            {
                filepathList.Add(filepath);
            }

            string[] subdirArray = Directory.GetDirectories(targetDir);
            foreach (string subdir in subdirArray)
            {
                Explore(subdir, filepathList);
            }
            return filepathList;
        }
    }
}
