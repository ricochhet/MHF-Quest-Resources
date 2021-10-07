using System;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FrontierTools
{
    class ByteUtils
    {
        public static byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl, int index)
        {
            byte[] dst = null;
            // int index = FindBytes(src, search);
            Console.WriteLine("==============================");

            Console.WriteLine("Byte Group (Search)  [Hex]     Start Index: {1} : {0}", StringUtils.ReturnByteArrayHexString(search), index);
            Console.WriteLine("Byte Group (Search)  [Str]     Start Index: {1} : {0}", StringUtils.ReturnByteArrayString(search), index);

            Console.WriteLine("Byte Group (Replace) [Hex]     Start Index: {1} : {0}", StringUtils.ReturnByteArrayHexString(repl), index);
            Console.WriteLine("Byte Group (Replace) [Str]     Start Index: {1} : {0}", StringUtils.ReturnByteArrayString(repl), index);

            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];
                Buffer.BlockCopy(src, 0, dst, 0, index);
                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
                Buffer.BlockCopy(src, index + search.Length, dst, index + repl.Length, src.Length - (index + search.Length));
            }

            Console.WriteLine("==============================");
            return dst;
        }

        public static byte[] Replace1ByteArray(byte[] FileData, byte[] Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index] }, 
                new byte[] { Replace[0] }, 
                Index);
        }

        public static byte[] Replace1ByteArray(byte[] FileData, int Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index] }, 
                new byte[] { BitConverter.GetBytes(Replace)[0] }, 
                Index);
        }

        public static byte[] Replace2ByteArray(byte[] FileData, byte[] Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index], FileData[Index + QuestReader.ObjectiveHexIncIndexA] }, 
                new byte[] { Replace[0], Replace[1] }, 
                Index);
        }

        public static byte[] Replace2ByteArray(byte[] FileData, int Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index], FileData[Index + QuestReader.ObjectiveHexIncIndexA] }, 
                new byte[] { BitConverter.GetBytes(Replace)[0], BitConverter.GetBytes(Replace)[1] }, 
                Index);
        }

        public static byte[] Replace4ByteArray(byte[] FileData, byte[] Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexA], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexB], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexC] }, 
                new byte[] { Replace[0], Replace[1], Replace[2], Replace[3] }, 
                Index);
        }

        public static byte[] Replace4ByteArray(byte[] FileData, int Replace, int Index)
        {
            return ReplaceBytes(
                FileData, 
                new byte[] { FileData[Index], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexA], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexB], 
                FileData[Index + QuestReader.ObjectiveHexIncIndexC] }, 
                new byte[] { BitConverter.GetBytes(Replace)[0], BitConverter.GetBytes(Replace)[1], BitConverter.GetBytes(Replace)[2], BitConverter.GetBytes(Replace)[3] } , 
                Index);
        }
    }
}
