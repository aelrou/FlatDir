using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FlatDir.Method
{
    internal static class FilenameCollision
    {
        public static string Increment(string filepath, int increment)
        {
            string output = null;
            string fileExtention = Path.GetExtension(filepath);
            string filename = Path.GetFileName(filepath);
            string filenameWithoutExtention = Path.GetFileNameWithoutExtension(filepath);
            filenameWithoutExtention = filenameWithoutExtention.Trim((char)32, (char)160);

            string regexEndsWithIncrement = "(.+)([^\\da-zA-Z])(\\d+)([^\\da-zA-Z])$";
            Match matchIncrement = Regex.Match(filenameWithoutExtention, regexEndsWithIncrement, RegexOptions.IgnoreCase);

            string regexEndsWithLikeIncrement = "(.+)([^\\d])(\\d+)$";
            Match matchLikeIncrement = Regex.Match(filenameWithoutExtention, regexEndsWithLikeIncrement, RegexOptions.IgnoreCase);

            if (matchIncrement.Success)
            {
                int groupCount = matchIncrement.Groups.Count;
                if (groupCount == 5)
                {
                    Group mainFilename = matchIncrement.Groups[1];
                    Group beforeIncrement = matchIncrement.Groups[2];
                    Group oldIncrementText = matchIncrement.Groups[3];
                    Group afterIncrement = matchIncrement.Groups[4];
                    int oldIncrement = Int32.Parse(oldIncrementText.Value);
                    oldIncrement = oldIncrement + increment;
                    filenameWithoutExtention = String.Concat(mainFilename, beforeIncrement, oldIncrement, afterIncrement);
                }
            }
            else if (matchLikeIncrement.Success)
            {
                int groupCount = matchLikeIncrement.Groups.Count;
                if (groupCount == 4)
                {
                    Group mainFilename = matchLikeIncrement.Groups[1];
                    Group beforeIncrement = matchLikeIncrement.Groups[2];
                    Group oldIncrementText = matchLikeIncrement.Groups[3];
                    int oldIncrement = Int32.Parse(oldIncrementText.Value);
                    oldIncrement = oldIncrement + increment;
                    filenameWithoutExtention = String.Concat(mainFilename, beforeIncrement, oldIncrement);
                }
            }
            else
            {
                filenameWithoutExtention = String.Concat(filenameWithoutExtention, " (", increment, ")");
            }

            int filenamePosition = filepath.LastIndexOf(filename);
            string filepathWithoutfilename = filepath.Remove(filenamePosition);
            filepathWithoutfilename = filepathWithoutfilename.Trim((char)92);
            if (filepathWithoutfilename.Trim((char)32, (char)160).Equals(""))
            {
                output = String.Concat(filenameWithoutExtention, fileExtention);
            }
            else
            {
                output = String.Concat(filepathWithoutfilename, "\\", filenameWithoutExtention, fileExtention);
            }
            return output;
        }
    }
}
