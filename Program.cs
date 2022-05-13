using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace frog_updater
{
    class Program
    {
        private static string path = @"c:\t\jinyong\R2.RPG";
        private static readonly int ZIZHI_POS = 0x3bc;
        private static readonly int WUGONG_POS = 0x3c2;

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                int pos = Int16.Parse(args[0], System.Globalization.NumberStyles.HexNumber);
                using (var sr = new BinaryReader(File.OpenRead(path)))
                {
                    sr.BaseStream.Seek(pos, SeekOrigin.Begin);
                    Int16 x = sr.ReadInt16();

                    Console.WriteLine(pos.ToString("X02") + " " + x.ToString("x2"));
                }
                return;
            }


            if (args.Length == 2)
            {
                int pos = Int16.Parse(args[0], System.Globalization.NumberStyles.HexNumber);
                int val = Int16.Parse(args[1], System.Globalization.NumberStyles.HexNumber);
                Console.WriteLine(pos.ToString("X02") + ": " +val.ToString("X02"));
                using (var sw = new BinaryWriter(File.OpenWrite(path)))
                {
                    sw.BaseStream.Seek(pos, SeekOrigin.Begin);                    
                    sw.Write(val);
                }
                return;
            }
            Console.WriteLine("Convert on " + path);
            using (var sr = new BinaryReader(File.OpenRead(path)))
            {
                sr.BaseStream.Seek(ZIZHI_POS, SeekOrigin.Begin);
                Int16 x = sr.ReadInt16();

                sr.BaseStream.Seek(WUGONG_POS, SeekOrigin.Begin);
                Int16 x2 = sr.ReadInt16();
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("z:" + ZIZHI_POS.ToString("X02") + " " + x.ToString("x2"));
                Console.WriteLine("w:" + WUGONG_POS.ToString("X02") + " " + x2.ToString("x2"));
            }

            using (var sw = new BinaryWriter(File.OpenWrite(path)))
            {
                sw.BaseStream.Seek(ZIZHI_POS, SeekOrigin.Begin);
                Int16 newz = 0x22;
                sw.Write(newz);
                Int16 neww = 0x16;
                sw.BaseStream.Seek(WUGONG_POS, SeekOrigin.Begin);
                sw.Write(neww);
            }

            Console.WriteLine("Updated Result:");
            using (var sr = new BinaryReader(File.OpenRead(path)))
            {
                sr.BaseStream.Seek(ZIZHI_POS, SeekOrigin.Begin);
                Int16 x = sr.ReadInt16();

                sr.BaseStream.Seek(WUGONG_POS, SeekOrigin.Begin);
                Int16 x2 = sr.ReadInt16();
                Console.OutputEncoding = System.Text.Encoding.UTF8;                
                Console.WriteLine("z:" + ZIZHI_POS.ToString("X02") + " " + x.ToString("x2"));
                Console.WriteLine("w:" + WUGONG_POS.ToString("X02") + " " + x2.ToString("x2"));
            }


        }
    }
   
}
