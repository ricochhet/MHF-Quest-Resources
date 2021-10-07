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
    class QuestWriter
    {
        public static byte[] NewFile;
        public static bool ExecuteWriteFile = true;
        public static bool OverwriteInvalidData = false;

        public static void QuestRankWriter(string[] args, string param, int index)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            Console.WriteLine("==============================");
            Console.WriteLine("Process 1 Byte Replace");
            Console.WriteLine("Current Quest Rank: {0}", QuestReader.LoadRankInfo(NewFile, index));

            if (QuestReader.LoadRankCheck(NewFile, index) || OverwriteInvalidData)
            {
                ExecuteWriteFile = true;
                NewFile = ByteUtils.Replace1ByteArray(NewFile, int.Parse(argStr), index);

                if (QuestReader.LoadRankCheck(NewFile, index) || OverwriteInvalidData)
                {
                    ExecuteWriteFile = true;
                    Console.WriteLine("New Rank: {0}", QuestReader.LoadRankInfo(NewFile, index));
                }
                else
                {
                    ExecuteWriteFile = false;
                    Console.WriteLine("Attempted to modify with invalid data. (OnWrite)");
                }
            }
            else
            {
                ExecuteWriteFile = false;
                Console.WriteLine("Attempted to modify with invalid data. (BeforeWrite)");
            }

            Console.WriteLine("Done.");
        }

        public static void QuestRewardWriter(string[] args, string param, int index, bool isUnsafe)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            Console.WriteLine("==============================");

            if (isUnsafe)
            {
                Console.WriteLine("Process 4 Byte Replace (UNSAFE)");
            }
            else
            {
                Console.WriteLine("Process 2 Byte Replace");
            }

            Console.WriteLine("Current Quest Reward: {0}", BitConverter.ToInt32(NewFile, index));

            ExecuteWriteFile = true;

            if (isUnsafe)
            {
                NewFile = ByteUtils.Replace4ByteArray(NewFile, new byte[] {
                        Convert.ToByte(argStr.Split(",")[0]),
                        Convert.ToByte(argStr.Split(",")[1]),
                        Convert.ToByte(argStr.Split(",")[2]),
                        Convert.ToByte(argStr.Split(",")[3]) },
                        index);
            }
            else
            {
                NewFile = ByteUtils.Replace2ByteArray(NewFile, new byte[] {
                        Convert.ToByte(argStr.Split(",")[0]),
                        Convert.ToByte(argStr.Split(",")[1]) },
                        index);
            }

            Console.WriteLine("New Quest Reward: {0}", BitConverter.ToInt32(NewFile, index));
            Console.WriteLine("Done.");
        }

        public static void QuestFeeWriter(string[] args, string param, int index, bool isUnsafe)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            Console.WriteLine("==============================");

            if (isUnsafe)
            {
                Console.WriteLine("Process 4 Byte Replace (UNSAFE)");
            }
            else
            {
                Console.WriteLine("Process 2 Byte Replace");
            }

            Console.WriteLine("Current Quest Fee: {0}", BitConverter.ToInt32(NewFile, index));

            ExecuteWriteFile = true;

            if (isUnsafe)
            {
                NewFile = ByteUtils.Replace4ByteArray(NewFile, new byte[] {
                        Convert.ToByte(argStr.Split(",")[0]),
                        Convert.ToByte(argStr.Split(",")[1]),
                        Convert.ToByte(argStr.Split(",")[2]),
                        Convert.ToByte(argStr.Split(",")[3]) },
                        index);
            }
            else
            {
                NewFile = ByteUtils.Replace2ByteArray(NewFile, new byte[] {
                        Convert.ToByte(argStr.Split(",")[0]),
                        Convert.ToByte(argStr.Split(",")[1]) },
                        index);
            }

            Console.WriteLine("New Quest Fee: {0}", BitConverter.ToInt32(NewFile, index));
            Console.WriteLine("Done.");
        }

        public static void LocationWriter(string[] args, string param, int index)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            Console.WriteLine("==============================");
            Console.WriteLine("Process 1 Byte Replace");
            Console.WriteLine("Current Location: {0}", QuestReader.LoadLocationsInfo(NewFile, index));

            if (QuestReader.LoadLocationsCheck(NewFile, index) || OverwriteInvalidData)
            {
                ExecuteWriteFile = true;
                NewFile = ByteUtils.Replace1ByteArray(NewFile, int.Parse(argStr), index);

                if (QuestReader.LoadLocationsCheck(NewFile, index) || OverwriteInvalidData)
                {
                    ExecuteWriteFile = true;
                    Console.WriteLine("New Location: {0}", QuestReader.LoadLocationsInfo(NewFile, index));
                }
                else
                {
                    ExecuteWriteFile = false;
                    Console.WriteLine("Attempted to modify with invalid data. (OnWrite)");
                }
            }
            else
            {
                ExecuteWriteFile = false;
                Console.WriteLine("Attempted to modify with invalid data. (BeforeWrite)");
            }

            Console.WriteLine("Done.");
        }

        public static void QuestTypeWriter(string[] args, string param, int index)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            Console.WriteLine("==============================");
            Console.WriteLine("Process 4 Byte Replace");
            Console.WriteLine("Current Objective Type: {0}", QuestReader.ReturnObjectiveTypeInfo(NewFile, index));

            if (QuestReader.ReturnObjectiveTypeCheck(NewFile, index) || OverwriteInvalidData)
            {
                ExecuteWriteFile = true;
                NewFile = ByteUtils.Replace4ByteArray(NewFile, new byte[] {
                    Convert.ToByte(argStr.Split(",")[0]),
                    Convert.ToByte(argStr.Split(",")[1]),
                    Convert.ToByte(argStr.Split(",")[2]),
                    Convert.ToByte(argStr.Split(",")[3]) },
                    index);

                if (QuestReader.ReturnObjectiveTypeCheck(NewFile, index) || OverwriteInvalidData)
                {
                    ExecuteWriteFile = true;
                    Console.WriteLine("New Objective Type: {0}", QuestReader.ReturnObjectiveTypeInfo(NewFile, index));
                }
                else
                {
                    ExecuteWriteFile = false;
                    Console.WriteLine("Attempted to modify with invalid data. (OnWrite)");
                }
            }
            else
            {
                ExecuteWriteFile = false;
                Console.WriteLine("Attempted to modify with invalid data. (BeforeWrite)");
            }

            Console.WriteLine("Done.");
        }

        public static void QuestGoalWriter(string[] args, string param, int hexIndex, int index)
        {
            string argStr = StringUtils.ReturnArgParams(args, param);
            string objType = QuestReader.ReturnObjectiveTypeInfo(NewFile, hexIndex);

            if (objType == "Hunt" | objType == "Slay" | objType == "Damage" | objType == "Slay or Damage" | objType == "Capture")
            {
                Console.WriteLine("==============================");
                Console.WriteLine("Process 1 Byte Replace");
                Console.WriteLine("Current Objective Type: {0}", objType);

                if (QuestReader.ReturnObjectiveMonsterCheck(NewFile, index) || OverwriteInvalidData)
                {
                    ExecuteWriteFile = true;
                    Console.WriteLine("Current Goal/Target/Objective: {0}", QuestReader.ReturnObjectiveMonsterInfo(NewFile, index));
                    NewFile = ByteUtils.Replace1ByteArray(NewFile, int.Parse(argStr), index);

                    if (QuestReader.ReturnObjectiveMonsterCheck(NewFile, index) || OverwriteInvalidData)
                    {
                        ExecuteWriteFile = true;
                        Console.WriteLine("New Goal/Target/Objective: {0}", QuestReader.ReturnObjectiveMonsterInfo(NewFile, index));
                    }
                    else
                    {
                        ExecuteWriteFile = false;
                        Console.WriteLine("Attempted to modify with invalid data. (OnWrite)");
                    }
                }
                else
                {
                    ExecuteWriteFile = false;
                    Console.WriteLine("Attempted to modify with invalid data. (BeforeWrite)");
                }
            }
            else if (objType == "Break Part")
            {
                Console.WriteLine("Cannot handle OBJTYPE: {0}", objType);
            }
            else if (objType == "Slay All")
            {
                Console.WriteLine("Cannot handle OBJTYPE: {0}", objType);
            }
            else
            {
                Console.WriteLine("==============================");
                Console.WriteLine("Process 2 Byte Replace");
                Console.WriteLine("Current Objective Type: {0}", objType);

                if (QuestReader.ReturnObjectiveItemCheck(NewFile, index) || OverwriteInvalidData)
                {
                    ExecuteWriteFile = true;
                    Console.WriteLine("Current Goal/Target/Objective: {0}", QuestReader.ReturnObjectiveItemInfo(NewFile, index));
                    NewFile = ByteUtils.Replace2ByteArray(NewFile, int.Parse(argStr), index);

                    if (QuestReader.ReturnObjectiveItemCheck(NewFile, index) || OverwriteInvalidData)
                    {
                        ExecuteWriteFile = true;
                        Console.WriteLine("New Goal/Target/Objective: {0}", QuestReader.ReturnObjectiveItemInfo(NewFile, index));
                    }
                    else
                    {
                        ExecuteWriteFile = false;
                        Console.WriteLine("Attempted to modify with invalid data. (OnWrite)");
                    }
                }
                else
                {
                    ExecuteWriteFile = false;
                    Console.WriteLine("Attempted to modify with invalid data. (BeforeWrite)");
                }
            }

            Console.WriteLine("Done.");
        }
    }
}
