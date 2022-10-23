using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF.Services
{
    internal interface IFileManager
    {
        public void Save(string filePath, string content);

        public string Read(string filePath);
    }
    internal class FileManager : IFileManager
    {
        public string Read(string filePath)
        {
            {
                try
                {
                    using var sr = new StreamReader(filePath);
                    return sr.ReadToEnd();
                }
                catch { }
                return "[]";
            }
        }

        public void Save(string filePath, string content)
        {
            try
            {
                using var sw = new StreamWriter(filePath);
                sw.WriteLine(content);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Gick Inte att Spara");
                Console.ReadKey();
            }
        }
    }
}
