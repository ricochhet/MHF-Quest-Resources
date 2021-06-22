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
    class QuestWriter
    {
        public static byte[] BaseFile;
        public static byte[] NewFile;
        public static BinaryReader brInput = QuestReader.brInput;

        public static Objects.QuestInfo.ReturnObjectiveMainDict ReturnObjectiveMainDict = QuestReader.ReturnObjectiveMainDict;

        public static string fileName;
        // public static StreamWriter writeStream;
        public static bool CreateLogFile = false;
        public static bool StrToHex = false;
        public static bool WriteQuest = false;
            public static string outputFileName;
        public static bool Debug = true;
        public static bool ExecuteWriteFile = true;
        public static bool OverwriteInvalidData = false;

            public static bool WriteQuestLocation = false;

            public static bool WriteMainQuestType = false;
            public static bool WriteSubAQuestType = false;
            public static bool WriteSubBQuestType = false;

            public static bool WriteMainQuestGoal = false;
            public static bool WriteSubAQuestGoal = false;
            public static bool WriteSubBQuestGoal = false;

        public static bool LoadLocationsCheck(byte[] src, int index)
        {
            bool check = false;
            string location = null;

            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(src, index), out location))
            {
                check = true;
                Console.WriteLine("[LOCATION][CHECK] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", location, LocationList.Locations.Count, index);
            }
            else
            {
                check = false;
                Console.WriteLine("[LOCATION][CHECK] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", location, LocationList.Locations.Count, index);
            }

            return check;
        }

        public static string LoadLocations(byte[] src, int index)
        {
            string location = null;

            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(src, index), out location))
            {
                Console.WriteLine("[LOCATION][RETURN] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", location, LocationList.Locations.Count, index);
            }
            else
            {
                Console.WriteLine("[LOCATION][RETURN] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", location, LocationList.Locations.Count, index);
                location = BitConverter.ToInt32(src, index).ToString();
            }

            return location;
        }

        public static bool ReturnObjectiveMonsterCheck(byte[] src, int index)
        {
            bool check = false;
            string monster = null;

            if (Monsters.MonsterNames.TryGetValue(src[index], out monster))
            {
                check = true;
                Console.WriteLine("[MONSTER][CHECK] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", monster, Monsters.MonsterNames.Count, index);
            }
            else
            {
                check = false;
                Console.WriteLine("[MONSTER][CHECK] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", monster, Monsters.MonsterNames.Count, index);
            }

            return check;
        }

        public static string ReturnObjectiveMonster(byte[] src, int index)
        {
            string monster = null;

            if (Monsters.MonsterNames.TryGetValue(src[index], out monster))
            {
                Console.WriteLine("[MONSTER][RETURN] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", monster, Monsters.MonsterNames.Count, index);
            }
            else
            {
                Console.WriteLine("[MONSTER][RETURN] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", monster, Monsters.MonsterNames.Count, index);
                monster = BitConverter.ToInt16(src, index).ToString();
            }
            

            return monster;
        }

        public static bool ReturnObjectiveItemCheck(byte[] src, int index)
        {
            bool check = false;
            string item = null;

            if (Items.ItemIDs.TryGetValue(BitConverter.ToInt16(src, index), out item))
            {
                check = true;
                Console.WriteLine("[ITEM][CHECK] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", item, Items.ItemIDs.Count, index);
            }
            else
            {
                check = false;
                Console.WriteLine("[ITEM][CHECK] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", item, Items.ItemIDs.Count, index);
            }

            return check;
        }

        public static string ReturnObjectiveItem(byte[] src, int index)
        {
            string item = null;

            if (Items.ItemIDs.TryGetValue(BitConverter.ToInt16(src, index), out item))
            {
                Console.WriteLine("[ITEM][RETURN] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", item, Items.ItemIDs.Count, index);
            }
            else
            {
                Console.WriteLine("[ITEM][RETURN] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", item, Items.ItemIDs.Count, index);
                item = BitConverter.ToInt16(src, index).ToString();
            }
            
            return item.ToString();
        }

        public static bool ReturnObjectiveTypeCheck(byte[] src, int index)
        {
            string objHex = QuestReader.ReturnObjectiveHex(src, index);
            bool check = false;
            string objType = null;

            if (QuestObjectives.Objectives.TryGetValue(objHex, out objType))
            {
                check = true;
                Console.WriteLine("[OBJTYPE][CHECK] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", objType, QuestObjectives.Objectives.Count, index);
            }
            else
            {
                check = false;
                Console.WriteLine("[OBJTYPE][CHECK] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", objType, QuestObjectives.Objectives.Count, index);
            }

            return check;
        }

        public static string ReturnObjectiveType(byte[] src, int index)
        {
            string objHex = QuestReader.ReturnObjectiveHex(src, index);
            string objType = null;

            if (QuestObjectives.Objectives.TryGetValue(objHex, out objType))
            {
                Console.WriteLine("[OBJTYPE][RETURN] (Success) Found Type: {0}     Data Length: {1}     Index: {2}", objType, QuestObjectives.Objectives.Count, index);
            }
            else
            {
                Console.WriteLine("[OBJTYPE][RETURN] (Fail) Found Type: {0}     Data Length: {1}     Index: {2}", objType, QuestObjectives.Objectives.Count, index);
                objType = objHex;
            }

            return objType.ToString();
        }

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

        public static void Initialize(string[] args)
        {
            if (args.Length < 1) { Console.WriteLine("Too few arguments."); return; }
            string input = args[0];
            if (args.Any("-log".Contains)) { CreateLogFile = true; StrToHex = false; WriteQuest = false; }
            if (args.Any("-strToHex".Contains)) { StrToHex = true; }
            if (args.Any("-edit".Contains)) { WriteQuest = true; }
                if (args.Any("-mobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = true;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = false;
                }

                if (args.Any("-aobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = true;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = false;
                }

                if (args.Any("-bobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = true;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = false;
                }
                
                if (args.Any("-mobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = true;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = false;
                }

                if (args.Any("-aobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = true;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = false;
                }

                if (args.Any("-bobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = true;

                    WriteQuestLocation = false;
                }

                if (args.Any("-objLocal".Contains) && WriteQuest)
                {
                    WriteMainQuestGoal = false;
                    WriteSubAQuestGoal = false;
                    WriteSubBQuestGoal = false;

                    WriteMainQuestType = false;
                    WriteSubAQuestType = false;
                    WriteSubBQuestType = false;

                    WriteQuestLocation = true; 
                }

            if (StrToHex)
            {
                string argStr = StringUtils.ReturnArgParams(args, "-strToHex ");
                Console.WriteLine("Output from \"{0}\": {1}", argStr, StringUtils.StringToHex(argStr, "shift-jis"));
            }
            // Check file 
            else if (File.Exists(input) || Directory.Exists(input))
            {
                FileAttributes inputAttr = File.GetAttributes(input);
                // Directories
                if (inputAttr.HasFlag(FileAttributes.Directory))
                {
                    Console.WriteLine("ERROR: Please specify only a single file.");
                }
                // Single file
                else
                {
                    FileLoader.LoadFile(input);
                    Console.WriteLine("Load File: {0}", input);
                    // We need to set these variables after LoadFile because FileLoader is not set until the function is called.
                    // Console.WriteLine("Setting variables.");
                    BaseFile = FileLoader.BaseFile;
                    NewFile = BaseFile;

                    if (CreateLogFile) 
                    {
                        Directory.CreateDirectory("output");
                        fileName = $"output\\{Path.GetFileName(input)}.log";
                        // StringUtils.WriteStream = writeStream;
                        StringUtils.FileName = fileName;
                        // File.Open(fileName, FileMode.Create);
                        if (File.Exists(fileName))    
                        {    
                            // File.Delete(fileName);    
                        }  
                        using(File.Create(fileName)) 
                        {
                            Console.WriteLine("==============================");
                            Console.WriteLine("Creating LOGFILE: {0}", fileName);
                            // writeStream = new StreamWriter(fileName);
                        }

                        QuestReader.WriteQuestLogFile(input);
                        Console.WriteLine("Done.");

                        // writeStream = new StreamWriter(fileName);
                    }

                    if (WriteQuest)
                    {
                        // Initialize
                        fileName = $"output\\{Path.GetFileName(input)}.log";
                        outputFileName = $"output\\{Path.GetFileName(input)}";
                        // StringUtils.WriteStream = writeStream;
                        StringUtils.FileName = fileName;
                        // QuestReader.InitQuestDataLoaders();
                        QuestReader.InitQuestDataLoaders();

                        if (WriteQuestLocation)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -objLocal");
                            Console.WriteLine("==============================");
                            Console.WriteLine("Process 1 Byte Replace");
                            Console.WriteLine("Current Location: {0}", LoadLocations(NewFile, QuestReader.LocationIndex));

                            if (LoadLocationsCheck(NewFile, QuestReader.LocationIndex) || OverwriteInvalidData)
                            {
                                ExecuteWriteFile = true;
                                NewFile = Replace1ByteArray(NewFile, int.Parse(argStr), QuestReader.LocationIndex);

                                if (LoadLocationsCheck(NewFile, QuestReader.LocationIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("New Location: {0}", LoadLocations(NewFile, QuestReader.LocationIndex));
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

                        if (WriteMainQuestType)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -mobjType ");
                            Console.WriteLine("==============================");
                            Console.WriteLine("Process 4 Byte Replace");
                            Console.WriteLine("Current Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.MainObjHexIndex));

                            if (ReturnObjectiveTypeCheck(NewFile, QuestReader.MainObjHexIndex) || OverwriteInvalidData)
                            {
                                ExecuteWriteFile = true;
                                NewFile = Replace4ByteArray(NewFile, new byte[] { 
                                    Convert.ToByte(argStr.Split(",")[0]), 
                                    Convert.ToByte(argStr.Split(",")[1]), 
                                    Convert.ToByte(argStr.Split(",")[2]), 
                                    Convert.ToByte(argStr.Split(",")[3]) }, 
                                    QuestReader.MainObjHexIndex);

                                if (ReturnObjectiveTypeCheck(NewFile, QuestReader.MainObjHexIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("New Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.MainObjHexIndex));
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

                        if (WriteSubAQuestType)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -aobjType ");
                            Console.WriteLine("==============================");
                            Console.WriteLine("Process 4 Byte Replace");
                            Console.WriteLine("Current Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.SubAObjHexIndex));

                            if (ReturnObjectiveTypeCheck(NewFile, QuestReader.SubAObjHexIndex) || OverwriteInvalidData)
                            {
                                ExecuteWriteFile = true;
                                NewFile = Replace4ByteArray(NewFile, new byte[] { 
                                    Convert.ToByte(argStr.Split(",")[0]), 
                                    Convert.ToByte(argStr.Split(",")[1]), 
                                    Convert.ToByte(argStr.Split(",")[2]), 
                                    Convert.ToByte(argStr.Split(",")[3]) }, 
                                    QuestReader.SubAObjHexIndex);

                                if (ReturnObjectiveTypeCheck(NewFile, QuestReader.SubAObjHexIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("New Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.SubAObjHexIndex));
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

                        if (WriteSubBQuestType)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -bobjType ");
                            Console.WriteLine("==============================");
                            Console.WriteLine("Process 4 Byte Replace");
                            Console.WriteLine("Current Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.SubBObjHexIndex));

                            if (ReturnObjectiveTypeCheck(NewFile, QuestReader.SubBObjHexIndex) || OverwriteInvalidData)
                            {
                                ExecuteWriteFile = true;
                                NewFile = Replace4ByteArray(NewFile, new byte[] { 
                                    Convert.ToByte(argStr.Split(",")[0]), 
                                    Convert.ToByte(argStr.Split(",")[1]), 
                                    Convert.ToByte(argStr.Split(",")[2]), 
                                    Convert.ToByte(argStr.Split(",")[3]) }, 
                                    QuestReader.SubBObjHexIndex);

                                if (ReturnObjectiveTypeCheck(NewFile, QuestReader.SubBObjHexIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("New Objective Type: {0}", ReturnObjectiveType(NewFile, QuestReader.SubBObjHexIndex));
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

                        if (WriteMainQuestGoal)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -mobjGoal ");
                            string objType = ReturnObjectiveType(NewFile, QuestReader.MainObjHexIndex);
                            
                            if (objType == "Hunt" | objType == "Slay" | objType == "Damage" | objType == "Slay or Damage" | objType == "Capture")
                            {
                                Console.WriteLine("==============================");
                                Console.WriteLine("Process 1 Byte Replace");
                                Console.WriteLine("Current Objective Type: {0}", objType);
                                
                                if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.MainObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.MainObjIndex));
                                    NewFile = Replace1ByteArray(NewFile, int.Parse(argStr), QuestReader.MainObjIndex);

                                    if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.MainObjIndex) || OverwriteInvalidData) 
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.MainObjIndex));
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

                                if (ReturnObjectiveItemCheck(NewFile, QuestReader.MainObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.MainObjIndex));
                                    NewFile = Replace2ByteArray(NewFile, int.Parse(argStr), QuestReader.MainObjIndex);

                                    if (ReturnObjectiveItemCheck(NewFile, QuestReader.MainObjIndex) || OverwriteInvalidData)
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.MainObjIndex));
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

                        if (WriteSubAQuestGoal)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -aobjGoal ");
                            string objType = ReturnObjectiveType(NewFile, QuestReader.SubAObjHexIndex);
                            
                            if (objType == "Hunt" | objType == "Slay" | objType == "Damage" | objType == "Slay or Damage" | objType == "Capture")
                            {
                                Console.WriteLine("==============================");
                                Console.WriteLine("Process 1 Byte Replace");
                                Console.WriteLine("Current Objective Type: {0}", objType);
                                
                                if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.SubAObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.SubAObjIndex));
                                    NewFile = Replace1ByteArray(NewFile, int.Parse(argStr), QuestReader.SubAObjIndex);

                                    if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.SubAObjIndex) || OverwriteInvalidData) 
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.SubAObjIndex));
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

                                if (ReturnObjectiveItemCheck(NewFile, QuestReader.SubAObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.SubAObjIndex));
                                    NewFile = Replace2ByteArray(NewFile, int.Parse(argStr), QuestReader.SubAObjIndex);

                                    if (ReturnObjectiveItemCheck(NewFile, QuestReader.SubAObjIndex) || OverwriteInvalidData)
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.SubAObjIndex));
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

                        if (WriteSubBQuestGoal)
                        {
                            string argStr = StringUtils.ReturnArgParams(args, "-edit -bobjGoal ");
                            string objType = ReturnObjectiveType(NewFile, QuestReader.SubBObjHexIndex);
                            
                            if (objType == "Hunt" | objType == "Slay" | objType == "Damage" | objType == "Slay or Damage" | objType == "Capture")
                            {
                                Console.WriteLine("==============================");
                                Console.WriteLine("Process 1 Byte Replace");
                                Console.WriteLine("Current Objective Type: {0}", objType);
                                
                                if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.SubBObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.SubBObjIndex));
                                    NewFile = Replace1ByteArray(NewFile, int.Parse(argStr), QuestReader.SubBObjIndex);

                                    if (ReturnObjectiveMonsterCheck(NewFile, QuestReader.SubBObjIndex) || OverwriteInvalidData) 
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveMonster(NewFile, QuestReader.SubBObjIndex));
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
                                Console.WriteLine("\n==============================");
                                Console.WriteLine("Process 2 Byte Replace");
                                Console.WriteLine("Current Objective Type: {0}", objType);

                                if (ReturnObjectiveItemCheck(NewFile, QuestReader.SubBObjIndex) || OverwriteInvalidData)
                                {
                                    ExecuteWriteFile = true;
                                    Console.WriteLine("Current Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.SubBObjIndex));
                                    NewFile = Replace2ByteArray(NewFile, int.Parse(argStr), QuestReader.SubBObjIndex);

                                    if (ReturnObjectiveItemCheck(NewFile, QuestReader.SubBObjIndex) || OverwriteInvalidData)
                                    {
                                        ExecuteWriteFile = true;
                                        Console.WriteLine("New Goal/Target/Objective: {0}", ReturnObjectiveItem(NewFile, QuestReader.SubBObjIndex));
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

                        if (ExecuteWriteFile)
                        {
                            if (Debug)
                            {
                                Console.WriteLine("[DEBUG] Debug mode is enabled.");
                            }
                            else
                            {
                                File.WriteAllBytes(outputFileName, NewFile);
                            }
                        }
                        else
                        {
                            Console.WriteLine("ExecuteWriteFile is off.");
                        }
                    }
                }
            }
            else Console.WriteLine("ERROR: Input file does not exist.");
        }
    }
}
