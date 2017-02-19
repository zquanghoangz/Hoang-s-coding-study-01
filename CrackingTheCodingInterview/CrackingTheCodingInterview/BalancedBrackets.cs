using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackingTheCodingInterview
{
    public class BalancedBrackets
    {
        public static void Func()
        {
            //int t = Convert.ToInt32(Console.ReadLine());
            //for (int a0 = 0; a0 < t; a0++)
            //{
            //    string expression = Console.ReadLine();
            //}
            CheckBracket("{{[[(())]]}}");
        }

        private static void CheckBracket(string expression)
        {
            int length = expression.Length;
            int index = -1;
            char[] openBrackets = new char[length];

            foreach (var ch in expression)
            {
                if (ch == '(' || ch == '{' || ch == '[')
                {
                    index++;
                    openBrackets[index] = ch;
                    
                    continue;
                }
                
                if (ch==')')
                {
                    if (index<0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }

                    if (openBrackets[index] == '(')
                    {
                        index--;
                    }
                    else
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
                else if (ch == ']')
                {
                    if (index < 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }

                    if (openBrackets[index] == '[')
                    {
                        index--;
                    }
                    else
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
                else if (ch == '}')
                {
                    if (index < 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }

                    if (openBrackets[index] == '{')
                    {
                        index--;
                    }
                    else
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
            }

            if (index>=0)
            {
                Console.WriteLine("NO");

            }
            else
            {
                Console.WriteLine("YES");
            }
        }

    }
}
