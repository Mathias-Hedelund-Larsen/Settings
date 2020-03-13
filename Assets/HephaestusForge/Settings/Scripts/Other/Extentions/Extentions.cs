using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HephaestusForge
{
    namespace SettingsManagement
    {
        public static class Extentions
        {
            /// <summary>
            /// Replaces the values of the encapsulation in the string
            /// </summary>
            /// <param name="origin"></param>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            public static string ReplaceEncaptiolation(this string origin, char start, char end, Func<int, string> searchByIndex, bool usePointingFunctionality)
            {
                if (usePointingFunctionality)
                {
                    string returned = origin;

                    var matches = Regex.Matches(origin, string.Format(@"\{0}(.*?)\{1}", start, end));

                    for (int i = 0; i < matches.Count; i++)
                    {
                        var sub = searchByIndex.Invoke(int.Parse(matches[i].Groups[1].Value));

                        returned = returned.Replace(matches[i].Groups[0].Value, sub);
                    }

                    return returned;
                }

                return origin;
            }

            /// <summary>
            /// This method is called to set the SentenceEntity. _EnablePointingFuncitonality on editor time in the OnInspect method in the LocalizationKitManager
            /// </summary>
            /// <param name="origin"></param>
            /// <param name="start"></param>
            /// <param name="end"></param>
            /// <returns></returns>
            public static bool EnableEncaptiolation(this string origin, char start, char end)
            {
                if (origin == null || origin == string.Empty) return false;

                List<int> starts = new List<int>(), ends = new List<int>();
                string returned = origin;

                var matches = Regex.Matches(origin, string.Format(@"\{0}(.*?)\{1}", start, end));

                if (matches.Count > 0)
                {
                    return int.TryParse(matches[0].Groups[1].Value, out int placeHolder);
                }
                return false;
            }

            public static void GetSubStringBetweenChars(this string origin, char start, char end, out string fullMatch, out string insideEncapsulation)
            {
                var match = Regex.Match(origin, string.Format(@"\{0}(.*?)\{1}", start, end));
                fullMatch = match.Groups[0].Value;
                insideEncapsulation = match.Groups[1].Value;
            }

            public static void GetSubStringsBetweenChars(this string origin, char start, char end, out string[] fullMatch, out string[] insideEncapsulation)
            {
                var matches = Regex.Matches(origin, string.Format(@"\{0}(.*?)\{1}", start, end));
                fullMatch = new string[matches.Count];
                insideEncapsulation = new string[matches.Count];

                for (int i = 0; i < matches.Count; i++)
                {
                    fullMatch[i] = matches[i].Groups[0].Value;

                    insideEncapsulation[i] = matches[i].Groups[1].Value;
                }
            }

            public static T Find<T>(this T[] origin, Predicate<T> predicate, out bool wasSuccesful)
            {
                for (int i = 0; i < origin.Length; i++)
                {
                    if (predicate.Invoke(origin[i]))
                    {
                        wasSuccesful = true;
                        return origin[i];
                    }
                }

                wasSuccesful = false;
                return default;
            }
        }
    }
}