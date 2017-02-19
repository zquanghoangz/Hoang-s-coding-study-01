using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "testfile.txt";

            //Normally we will write code like this
            using (var fileStream = new StreamWriter(fileName))
            {
                ReadFile(fileStream);
            }

            //How to use Disposable. StreamWriter is a IDisposable
            Disposable.Using(
                () => new StreamWriter(fileName),
                ReadFile);
        }

        private static string ReadFile(StreamWriter fileStream)
        {
            var text = "This is the sample text in file";
            fileStream.WriteLine(text);
            return text;
        }
    }
}
