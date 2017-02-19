using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackingTheCodingInterview
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //RansomNote.MainFunction(
    //        //    "8 5",
    //        //    "you to give me one grand today night",
    //        //    "give one grand today to");
    //        //BalancedBrackets.Func();
    //        Console.WriteLine("{0} {1} {2} {3}",(byte)+1,(char)+1,(int)+1,(long)-1);
    //        Console.ReadKey();
    //    }
    //}
    class Program
    {
        //public static int i = 0;
        public static void Main()
        {
            Derived d = new Derived();
            int i = 10;
            d.Foo(i);

            //Console.WriteLine(60 + 25 + "B");
            //Console.WriteLine("B" + 60 + 25);
            //(i++)
            //    .Print();

            Console.ReadKey(true);
        }
    }

    //static class Extensions
    //{
    //    public static void Print(this int i)
    //    {
    //        Console.WriteLine(Program.i);
    //        Console.WriteLine(i);
    //    }
    //}

    public class Base
    {
        public virtual void Foo(int x)
        {
            Console.WriteLine("Base.Foo(int)");
        }
    }
    public class Derived : Base
    {
        public override void Foo(int x)
        {
            Console.WriteLine("Derived.Foo(int)");
        }
        public void Foo(object o)
        {
            Console.WriteLine("Derived.Foo(object)");
        }
    }
}
