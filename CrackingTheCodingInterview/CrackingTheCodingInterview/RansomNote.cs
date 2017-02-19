using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrackingTheCodingInterview
{
    public class RansomNote
    {
        public static void MainFunction(string s_m, string s_magazine, string s_ransom)
        {
            string[] tokens_m = s_m.Split(' ');
            int m = Convert.ToInt32(tokens_m[0]);
            int n = Convert.ToInt32(tokens_m[1]);
            string[] magazine = s_magazine.Split(' ');
            string[] ransom = s_ransom.Split(' ');

            magazine = magazine.Select(x => x.ToUpper()).OrderBy(x => x).ToArray();
            ransom = ransom.Select(x => x.ToUpper()).OrderBy(x => x).ToArray();

            int i = 0;
            for (int j = 0; j < n; j++)
            {
                var ransomWord = ransom[j];
                if (i == m)
                {
                    Console.WriteLine("No");
                    return;
                }

                while (i < m)
                {
                    if (string.CompareOrdinal(magazine[i], ransomWord) > 0)
                    {
                        Console.WriteLine("No");
                        return;
                    }
                    if (string.CompareOrdinal(magazine[i], ransomWord) == 0)
                    {
                        i++;
                        break;
                    }

                    i++;
                }
            }

            Console.WriteLine("Yes");
        }
    }
}
