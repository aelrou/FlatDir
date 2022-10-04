using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatDir.Model;

namespace FlatDir.Method
{
    internal static class Deduplicate
    {
        public static void Prune(string targetDir, List<HashPair> hashPairList)
        {
            for (int i = 0; i < hashPairList.Count; i++)
            {
                for (int j = i + 1; j < hashPairList.Count; j++)
                {
                    if (hashPairList.ElementAt(i).Hash.Equals(hashPairList.ElementAt(j).Hash))
                    {
                        string filepath1 = hashPairList.ElementAt(i).File;
                        string filepath2 = hashPairList.ElementAt(j).File;
                        if (hashPairList.ElementAt(i).Hash.Equals(WrapHash.EMPTY_FILE))
                        {
                            File.Delete(String.Concat(targetDir, "\\", filepath1));
                            File.Delete(String.Concat(targetDir, "\\", filepath2));
                        }
                        else
                        {
                            //Console.WriteLine();
                            string choice = FilenameSwitch.Choose(filepath1, filepath2);
                            if (choice.Equals(filepath1))
                            {
                                //Console.WriteLine(String.Concat("Keep   ", filepath1));
                                //Console.WriteLine(String.Concat("Delete ", filepath2));
                                File.Delete(String.Concat(targetDir, "\\", filepath2));
                            }
                            else if (choice.Equals(filepath2))
                            {
                                //Console.WriteLine(String.Concat("Keep   ", filepath2));
                                //Console.WriteLine(String.Concat("Delete ", filepath1));
                                File.Delete(String.Concat(targetDir, "\\", filepath1));
                            }
                            //Console.WriteLine(hashPairList.ElementAt(i).Hash);
                        }
                    }
                }
            }
        }
    }
}
