using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    [Flags]
    public enum Alignments
    {
        Left = 0,
        Right = 1,
        Bottom = 3,
        Top = 4
    }

    public static class Demo
    {
        public static void DemoFunc()
        {
            Alignments leftRightCombine = Alignments.Left | Alignments.Right;
            Alignments bottomTopCombine = Alignments.Top | Alignments.Bottom;

            // check enum
            var isContains = (leftRightCombine & Alignments.Right) != 0;
            isContains = bottomTopCombine.HasFlag(Alignments.Bottom);

            // combining combinations
            var all = bottomTopCombine | leftRightCombine;

            //Remove/add flag
            all ^= Alignments.Top; // remove if it have
            all ^= Alignments.Top; // add if it don't have
        }
    }
}
