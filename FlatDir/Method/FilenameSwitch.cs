using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FlatDir.Method
{
    internal static class FilenameSwitch
    {
        public static string Choose(string filepath1, string filepath2)
        {
            string choice = null;
            string fileExt1 = Path.GetExtension(filepath1);
            string fileExt2 = Path.GetExtension(filepath2);
            if (fileExt1.Length > 0 && fileExt2.Length > 0)
            {
                choice = RegexFilters(filepath1, filepath2);
            }
            else if (fileExt1.Length > 0)
            {
                choice = filepath1;
            }
            else if (fileExt2.Length > 0)
            {
                choice = filepath2;
            }
            else
            {
                choice = RegexFilters(filepath1, filepath2);
            }

            return choice;
        }

        private static string RegexFilters(string filepath1, string filepath2)
        {
            string choice = null;
            string regexContainsLikeYear = ".*(?:19\\d{2}|20\\d{2}).*";
            Match match1 = null;
            Match match2 = null;
            match1 = Regex.Match(filepath1, regexContainsLikeYear, RegexOptions.IgnoreCase);
            match2 = Regex.Match(filepath2, regexContainsLikeYear, RegexOptions.IgnoreCase);
            if (match1.Success || match2.Success)
            {
                string regexStartsWithDate = "^\\d{4}[^\\da-zA-Z]\\d{1,2}[^\\da-zA-Z]\\d{1,2}.*";
                match1 = Regex.Match(filepath1, regexStartsWithDate, RegexOptions.IgnoreCase);
                match2 = Regex.Match(filepath2, regexStartsWithDate, RegexOptions.IgnoreCase);
                if (match1.Success && match2.Success)
                {
                    choice = ShorterPath(filepath1, filepath2);
                }
                else if (match1.Success)
                {
                    choice = filepath1;
                }
                else if (match2.Success)
                {
                    choice = filepath2;
                }
                else
                {
                    string regexContainsDate = ".*\\d{4}[^\\da-zA-Z]\\d{1,2}[^\\da-zA-Z]\\d{1,2}.*";
                    match1 = Regex.Match(filepath1, regexContainsDate, RegexOptions.IgnoreCase);
                    match2 = Regex.Match(filepath2, regexContainsDate, RegexOptions.IgnoreCase);
                    if (match1.Success && match2.Success)
                    {
                        choice = ShorterPath(filepath1, filepath2);
                    }
                    else if (match1.Success)
                    {
                        choice = filepath1;
                    }
                    else if (match2.Success)
                    {
                        choice = filepath2;
                    }
                    else
                    {
                        string regexStartsWithLikeDate = "^\\d{1,2}[^\\da-zA-Z]\\d{1,2}[^\\da-zA-Z]\\d{4}.*";
                        match1 = Regex.Match(filepath1, regexStartsWithLikeDate, RegexOptions.IgnoreCase);
                        match2 = Regex.Match(filepath2, regexStartsWithLikeDate, RegexOptions.IgnoreCase);
                        if (match1.Success && match2.Success)
                        {
                            choice = ShorterPath(filepath1, filepath2);
                        }
                        else if (match1.Success)
                        {
                            choice = filepath1;
                        }
                        else if (match2.Success)
                        {
                            choice = filepath2;
                        }
                        else
                        {
                            string regexContainsLikeDate = ".*\\d{1,2}[^\\da-zA-Z]\\d{1,2}[^\\da-zA-Z]\\d{4}.*";
                            match1 = Regex.Match(filepath1, regexContainsLikeDate, RegexOptions.IgnoreCase);
                            match2 = Regex.Match(filepath2, regexContainsLikeDate, RegexOptions.IgnoreCase);
                            if (match1.Success && match2.Success)
                            {
                                choice = ShorterPath(filepath1, filepath2);
                            }
                            else if (match1.Success)
                            {
                                choice = filepath1;
                            }
                            else if (match2.Success)
                            {
                                choice = filepath2;
                            }
                            else
                            {
                                string regexStartsWithPartialDate = "^\\d{4}[^\\da-zA-Z]\\d{1,2}.*";
                                match1 = Regex.Match(filepath1, regexStartsWithPartialDate, RegexOptions.IgnoreCase);
                                match2 = Regex.Match(filepath2, regexStartsWithPartialDate, RegexOptions.IgnoreCase);
                                if (match1.Success && match2.Success)
                                {
                                    choice = ShorterPath(filepath1, filepath2);
                                }
                                else if (match1.Success)
                                {
                                    choice = filepath1;
                                }
                                else if (match2.Success)
                                {
                                    choice = filepath2;
                                }
                                else
                                {
                                    string regexContainsPartialDate = ".*\\d{4}[^\\da-zA-Z]\\d{1,2}.*";
                                    match1 = Regex.Match(filepath1, regexContainsPartialDate, RegexOptions.IgnoreCase);
                                    match2 = Regex.Match(filepath2, regexContainsPartialDate, RegexOptions.IgnoreCase);
                                    if (match1.Success && match2.Success)
                                    {
                                        choice = ShorterPath(filepath1, filepath2);
                                    }
                                    else if (match1.Success)
                                    {
                                        choice = filepath1;
                                    }
                                    else if (match2.Success)
                                    {
                                        choice = filepath2;
                                    }
                                    else
                                    {
                                        string regexStartsWithPartialLikeDate = "^\\d{1,2}[^\\da-zA-Z]\\d{4}.*";
                                        match1 = Regex.Match(filepath1, regexStartsWithPartialLikeDate, RegexOptions.IgnoreCase);
                                        match2 = Regex.Match(filepath2, regexStartsWithPartialLikeDate, RegexOptions.IgnoreCase);
                                        if (match1.Success && match2.Success)
                                        {
                                            choice = ShorterPath(filepath1, filepath2);
                                        }
                                        else if (match1.Success)
                                        {
                                            choice = filepath1;
                                        }
                                        else if (match2.Success)
                                        {
                                            choice = filepath2;
                                        }
                                        else
                                        {
                                            string regexContainsPartialLikeDate = ".*\\d{1,2}[^\\da-zA-Z]\\d{4}.*";
                                            match1 = Regex.Match(filepath1, regexContainsPartialLikeDate, RegexOptions.IgnoreCase);
                                            match2 = Regex.Match(filepath2, regexContainsPartialLikeDate, RegexOptions.IgnoreCase);
                                            if (match1.Success && match2.Success)
                                            {
                                                choice = ShorterPath(filepath1, filepath2);
                                            }
                                            else if (match1.Success)
                                            {
                                                choice = filepath1;
                                            }
                                            else if (match2.Success)
                                            {
                                                choice = filepath2;
                                            }
                                            else
                                            {
                                                string regexContainsLikeMonth = "(?i).*(?:jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec).*";
                                                match1 = Regex.Match(filepath1, regexContainsLikeMonth, RegexOptions.IgnoreCase);
                                                match2 = Regex.Match(filepath2, regexContainsLikeMonth, RegexOptions.IgnoreCase);
                                                if (match1.Success && match2.Success)
                                                {
                                                    choice = ShorterPath(filepath1, filepath2);
                                                }
                                                else if (match1.Success)
                                                {
                                                    choice = filepath1;
                                                }
                                                else if (match2.Success)
                                                {
                                                    choice = filepath2;
                                                }
                                                else
                                                {
                                                    choice = ShorterPath(filepath1, filepath2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                choice = ShorterPath(filepath1, filepath2);
            }

            return choice;
        }

        private static string ShorterPath(string filepath1, string filepath2)
        {
            string choice = null;
                int least = Math.Min(filepath1.Length, filepath2.Length);
                if (least == filepath1.Length)
                {
                    choice = filepath1;
                }
                else if (least == filepath2.Length)
                {
                    choice = filepath2;
                }
            return choice;
        }
    }
}
