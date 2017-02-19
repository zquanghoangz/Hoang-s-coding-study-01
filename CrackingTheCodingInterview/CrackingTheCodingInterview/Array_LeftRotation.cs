using System;
using System.Linq;

namespace CrackingTheCodingInterview
{
    class Solution
    {



        public static void LeftRotation(int n = 5, int k = 4)
        {

            int[] a = new[] { 1, 2, 3, 4, 5 };

            //Start working
            for (int i = k - 1; i < n; i++)
            {
                Console.Write("{0} ", a[i]);
            }

            for (int i = 0; i < k - 1; i++)
            {
                Console.Write("{0} ", a[i]);
            }
        }

        public static void MakingAnagrams(string a = "cde", string b = "abc")
        {
            //order
            a = string.Join(string.Empty, a.OrderBy(x => x));
            b = string.Join(string.Empty, b.OrderBy(x => x));
            //remove
            int i = 0, j = 0;
            while (i < a.Length && j < b.Length)
            {
                if (a[i] < b[j])
                {
                    i++;
                }
                else if (a[i] > b[j])
                {
                    j++;
                }
                else
                {
                    a = a.Remove(i, 1);
                    b = b.Remove(j, 1);
                }
            }
            //get length
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.Write(a.Length + b.Length);
        }
    }
}
