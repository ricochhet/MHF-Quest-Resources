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
    class StringUtils
    {
        public static StreamWriter WriteStream;
        public static string FileName = null;

        public static string ReturnArgParams(string[] Args, string Pattern)
        {
            return string.Join(" ", Args, 1, Args.Length - 1).Replace(Pattern, "");
        }

        public static string ReturnByteArrayString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }

            return sb.ToString();
        }

        public static string ReturnByteArrayHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }

            return sb.ToString();
        }

        public static byte[] UnsignedBytesFromSignedBytes(sbyte[] signed)
        {
            var unsigned = new byte[signed.Length];
            Buffer.BlockCopy(signed, 0, unsigned, 0, signed.Length);
            return unsigned;
        }

        public static void WriteLine(string String, params object[] Objs) 
        {
            using(WriteStream = new StreamWriter(FileName, true)) {
                WriteStream.WriteLine(String, Objs);
            }
        }

        public static void WriteLine(string String) 
        {
            using(WriteStream = new StreamWriter(FileName, true)) {
                WriteStream.WriteLine(String);
            }
        }

        public static void ReadDict(Dictionary<string, string> dictionary) 
        {
            for (int i = 0; i < dictionary.Count; i++) 
            {
                Console.WriteLine("Key: {0}        Value: {1}", 
                    dictionary.ElementAt(i).Key, dictionary.ElementAt(i).Value);
            }
        }

        public static void NewLine()
        {
            Console.WriteLine("\n");
        }

        public static void ObjectDumper(object obj)
        {
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                Console.WriteLine("Key: {0}        Value: {1}",name,value);
            }
        }

        public static StringBuilder StringToHex(string String, string enc) 
        {
            /* Replace new line string with a real new line, C# treats "\n" as System.Environment.Newline, 
            while it treats "\\n" as a string with the text, "\n" */
            String = String.Replace("\\n", "\n").Replace("<NLINE>", "\n"); // Optionally alternative: System.Environment.NewLine;
            byte[] bytes = Encoding.GetEncoding(enc).GetBytes(String);
            StringBuilder hex = new StringBuilder();

            foreach (byte b in bytes)
            {
                /* We have to use "X2" otherwise certain bytes don't show nicely. */
                hex.Append(b.ToString("X2") + " ");
            }

            return hex;
        }

        public static StringBuilder StringToRawHex(string String, string enc) 
        {
            /* Replace new line string with a real new line, C# treats "\n" as System.Environment.Newline, 
            while it treats "\\n" as a string with the text, "\n" */
            String = String.Replace("\\n", "\n").Replace("<NLINE>", "\n"); // Optionally alternative: System.Environment.NewLine;
            byte[] bytes = Encoding.GetEncoding(enc).GetBytes(String);
            StringBuilder hex = new StringBuilder();

            foreach (byte b in bytes)
            {
                /* We have to use "X2" otherwise certain bytes don't show nicely. */
                hex.Append(b.ToString() + " ");
            }

            return hex;
        }
    }
}
