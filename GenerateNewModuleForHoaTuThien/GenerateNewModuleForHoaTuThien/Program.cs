using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace GenerateNewModuleForHoaTuThien
{
    class Program
    {
        static void Main(string[] args)
        {
            const string rootPath = @"D:\Projects\Products\HoaTuThien\hoatuthien\HoaTuThien";
            //input new module name
            //Console.WriteLine("Input new module name:");
            //var module = Console.ReadLine();
            const string moduleName = "";
            HoaTuThienHepler hepler = new HoaTuThienHepler(); hepler.Init(moduleName, rootPath);

            Console.ReadKey();


        }
    }

    public class HoaTuThienHepler
    {
        public void Init(string moduleName, string rootPath)
        {
            ValidateModuleName(moduleName);
        }

        private void ValidateModuleName(string moduleName)
        {
            throw new NotImplementedException();
        }
    }

    public static class Folders
    {
        public const string DalEntities = @"HoaTuThien.DAL\Entities";
    }
}
