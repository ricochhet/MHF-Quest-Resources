using System;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.Text.RegularExpressions;

namespace QuestEditor
{
    class FileLoader
    {
        public static byte[] BaseFile;
        public static BinaryReader brInput;

        public static Structs.QuestInfo LoadFile(string FilePath)
        {
            var crc32 = new Crc32();
            string hash = string.Empty;
            var fs = File.Open(FilePath, FileMode.Open);
            foreach (byte b in crc32.ComputeHash(fs))
            {
                hash += b.ToString("x2").ToLower();
            }
            fs.Close();

            BaseFile = File.ReadAllBytes(FilePath);
            brInput = new BinaryReader(new FileStream(FilePath, FileMode.Open));
            BaseFile.Reverse();
            return default;
        }
    }
}
