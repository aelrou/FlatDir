using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FlatDir.Method;
using FlatDir.Model;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace FlatDir
{
    internal class Program

    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            MainArgs sessionArgs = new MainArgs(args);

            //TestFiles.Generate(sessionArgs.tempDir);

            Console.WriteLine("Exploring directory");
            List<string> filepathList = new List<string>();
            TargetDirectory.Explore(sessionArgs.targetDir, filepathList);

            Console.WriteLine();
            Console.WriteLine("Stripping awkward characters");
            foreach (string filepath in filepathList)
            {
                int filenamePosition = filepath.LastIndexOf("\\");
                string directoryPath = filepath.Remove(filenamePosition);
                string normalFilename = AwkwardFilename.Normalize(filepath);
                File.Move(filepath, String.Concat(directoryPath, "\\", normalFilename));
            }

            Console.WriteLine();
            Console.WriteLine("Re-exploring directory");
            filepathList.Clear();
            TargetDirectory.Explore(sessionArgs.targetDir, filepathList);

            Console.WriteLine();
            Console.WriteLine("Collecting relative paths");
            List<string> relativeFilepathList = new List<string>();
            foreach (string filepath in filepathList)
            {
                string filename = Path.GetFileName(filepath);
                int filenamePosition = filepath.LastIndexOf("\\");
                string directoryPath = filepath.Remove(filenamePosition);
                string relativeDirectoryPath = directoryPath.Replace(sessionArgs.targetDir, "");
                relativeDirectoryPath = relativeDirectoryPath.Trim((char)92);
                if (relativeDirectoryPath.Trim((char)32, (char)160).Equals(""))
                {
                    relativeFilepathList.Add(String.Concat(filename));
                }
                else
                {
                    relativeFilepathList.Add(String.Concat(relativeDirectoryPath, "\\", filename));
                }
            }
            Console.WriteLine(relativeFilepathList.Count);

            Console.WriteLine();
            Console.WriteLine("Saving JSON");
            string relativeFilepathsJson = JsonConvert.SerializeObject(relativeFilepathList, Formatting.Indented);
            File.WriteAllText(String.Concat(sessionArgs.tempDir, "\\relativeFilepaths.json"), relativeFilepathsJson);

            Console.WriteLine();
            Console.WriteLine("Computing hashes");
            int loopCount = 0;
            List<HashPair> hashPairList = new List<HashPair>();
            foreach (string relativeFilepath in relativeFilepathList)
            {
                loopCount++;
                HashPair hashPair = WrapHash.Compute(sessionArgs.targetDir, relativeFilepath);
                hashPairList.Add(hashPair);
                if (loopCount % 64 == 0)
                {
                    Console.WriteLine(loopCount);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Saving JSON");
            string hashPairsJson = JsonConvert.SerializeObject(hashPairList, Formatting.Indented);
            File.WriteAllText(String.Concat(sessionArgs.tempDir, "\\hashPairs.json"), hashPairsJson);

            Console.WriteLine();
            Console.WriteLine("Pruning duplicates");
            Deduplicate.Prune(sessionArgs.targetDir, hashPairList);

            bool flatten = false;
            while (flatten == false)
            {
                Console.WriteLine();
                Console.WriteLine("Do you want to flatten the directory structure? [y/n]");
                string option = Console.ReadLine();
                if (option.ToLower().StartsWith("y"))
                {
                    flatten = true;
                }
                else if (option.ToLower().StartsWith("n"))
                {
                    break;
                }
            }
            
            if (flatten == true)
            {
                Console.WriteLine();
                Console.WriteLine("Re-exploring directory");
                filepathList.Clear();
                TargetDirectory.Explore(sessionArgs.targetDir, filepathList);

                Console.WriteLine();
                Console.WriteLine("Collecting relative paths");
                relativeFilepathList.Clear();
                foreach (string filepath in filepathList)
                {
                    string filename = Path.GetFileName(filepath);
                    int filenamePosition = filepath.LastIndexOf("\\");
                    string directoryPath = filepath.Remove(filenamePosition);
                    string relativeDirectoryPath = directoryPath.Replace(sessionArgs.targetDir, "");
                    relativeDirectoryPath = relativeDirectoryPath.Trim((char)92);
                    if (relativeDirectoryPath.Trim((char)32, (char)160).Equals(""))
                    {
                        relativeFilepathList.Add(String.Concat(filename));
                    }
                    else
                    {
                        relativeFilepathList.Add(String.Concat(relativeDirectoryPath, "\\", filename));
                    }
                }
                Console.WriteLine(relativeFilepathList.Count);

                Console.WriteLine();
                Console.WriteLine("Moving files out of subdirectories");
                foreach (string relativeFilepath in relativeFilepathList)
                {
                    string filename = relativeFilepath.Replace((char)92, (char)32);
                    string sourceFilepath = String.Concat(sessionArgs.targetDir, "\\", relativeFilepath);
                    string destinationFilepath = String.Concat(sessionArgs.targetDir, "\\", filename);

                    int moveLoopCount = 0;
                    while (true)
                    {
                        if (moveLoopCount > 9)
                        {
                            Console.WriteLine("Unable to move file after trying 10 names.");
                            Console.WriteLine(sourceFilepath);
                            Console.ReadLine();
                            Environment.Exit(1);
                        }
                        moveLoopCount++;
                        try
                        {
                            File.Move(sourceFilepath, destinationFilepath);
                            break; // Successfully break out of while loop
                        }
                        catch (IOException)
                        {
                            destinationFilepath = FilenameCollision.Increment(destinationFilepath, moveLoopCount);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.GetType().Name);
                            Console.WriteLine(e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
