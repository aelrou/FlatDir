using FlatDir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FlatDir.Method
{
    internal static class WrapHash
    {
        public static readonly string EMPTY_FILE = "FILE_SIZE_IS_ZERO";

        public static HashPair Compute(string targetDir, string relativeFilepath)
        {
            string certUtilExe = "C:\\Windows\\System32\\certutil.exe";
            int listLength = 0;
            string line = null;
            string lastLine = null;
            string secondLastLine = null;
            bool hashSuccess = false;
            string hash = null;

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = certUtilExe,
                    Arguments = String.Concat("-hashfile \"", targetDir, "\\", relativeFilepath, "\" SHA256"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            List<string> certUtilLines = new List<string>();
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                line = proc.StandardOutput.ReadLine();
                certUtilLines.Add(line);
            }

            listLength = certUtilLines.Count;
            lastLine = certUtilLines.ElementAt(listLength - 1);
            if (lastLine.Equals("CertUtil: -hashfile command completed successfully."))
            {
                secondLastLine = certUtilLines.ElementAt(listLength - 2);
                if (!string.IsNullOrEmpty(secondLastLine))
                {
                    hashSuccess = true;
                    hash = secondLastLine;
                }
            }
            else if (lastLine.Equals("CertUtil: The volume for a file has been externally altered so that the opened file is no longer valid."))
            {
                FileInfo file = new FileInfo(String.Concat(targetDir, "\\", relativeFilepath));
                long size = file.Length;
                if (size == 0)
                {
                    hashSuccess = true;
                    hash = EMPTY_FILE;
                }
            }

            HashPair result = new HashPair();
            if (hashSuccess)
            {
                if (hash.Equals(EMPTY_FILE))
                {
                    result.File = String.Concat(relativeFilepath);
                    result.Hash = EMPTY_FILE;
                }
                else
                {
                    result.File = String.Concat(relativeFilepath);
                    result.Hash = hash;
                }
            }
            else
            {
                Console.WriteLine(lastLine);
                Console.ReadLine();
                Environment.Exit(1);
            }
            return result;
        }
    }
}
