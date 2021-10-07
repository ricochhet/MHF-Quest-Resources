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
    class QuestTool
    {
        public static byte[] BaseFile;
        public static byte[] NewFile;
        public static BinaryReader brInput = QuestReader.brInput;

        public static Objects.QuestInfo.ReturnObjectiveMainDict ReturnObjectiveMainDict = QuestReader.ReturnObjectiveMainDict;

        public static string fileName;
        // public static StreamWriter writeStream;
        public static bool CreateLogFile = false;
        public static bool isUnsafe = false;
        public static bool StrToHex = false;
        public static bool DecToByte = false;
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

        public static bool WriteQuestFee = false;
        public static bool WriteQuestPrimaryReward = false;
        public static bool WriteQuestRewardA = false;
        public static bool WriteQuestRewardB = false;

        public static bool WriteQuestRank = false;

        public static void Initialize(string[] args)
        {
            if (args.Length < 1) { Console.WriteLine("Too few arguments."); return; }
            string input = args[0];
            if (args.Any("-log".Contains)) { CreateLogFile = true; StrToHex = false; DecToByte = false; WriteQuest = false; }
            if (args.Any("-strToHex".Contains)) { StrToHex = true; }
            if (args.Any("-decToByte".Contains)) { DecToByte = true; }
            if (args.Any("-unsafe".Contains)) { isUnsafe = true; }
            if (args.Any("-edit".Contains)) { WriteQuest = true; }
                if (args.Any("-mobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = true; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-aobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = true; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-bobjGoal".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = true;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }
                
                if (args.Any("-mobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = true; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-aobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = true; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-bobjType".Contains) && WriteQuest) 
                { 
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = true;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-objLocal".Contains) && WriteQuest)
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = true; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-questFee".Contains) && WriteQuest) 
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = true; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-questRewardMain".Contains) && WriteQuest) 
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = true;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-questRewardA".Contains) && WriteQuest) 
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = true; WriteQuestRewardB = false; WriteQuestRank = false;
                }

                if (args.Any("-questRewardB".Contains) && WriteQuest) 
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = true; WriteQuestRank = false;
                }

                if (args.Any("-questRank".Contains) && WriteQuest) 
                {
                    WriteMainQuestGoal = false; WriteSubAQuestGoal = false; WriteSubBQuestGoal = false;
                    WriteMainQuestType = false; WriteSubAQuestType = false; WriteSubBQuestType = false;
                    WriteQuestLocation = false; WriteQuestFee = false; WriteQuestPrimaryReward = false;
                    WriteQuestRewardA = false; WriteQuestRewardB = false; WriteQuestRank = true;
                }

            if (StrToHex)
            {
                string argStr = StringUtils.ReturnArgParams(args, "-strToHex ");
                Console.WriteLine("Output from \"{0}\": {1}", argStr, StringUtils.StringToHex(argStr, "shift-jis"));
            }
            
            if (DecToByte) {
                string argStr = StringUtils.ReturnArgParams(args, "-decToByte ");
                Console.WriteLine("Output from \"{0}\":\n==> {1}\n==> {2}", argStr, 
                    StringUtils.ReturnByteArrayHexString(BitConverter.GetBytes(int.Parse(argStr))), StringUtils.ReturnByteArrayString(BitConverter.GetBytes(int.Parse(argStr))));
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
                        QuestWriter.NewFile = NewFile;
                        QuestWriter.OverwriteInvalidData = OverwriteInvalidData;

                        if (WriteQuestLocation) { QuestWriter.LocationWriter(args, "-edit -objLocal ", QuestReader.LocationIndex); }
                        
                        if (WriteMainQuestType) { QuestWriter.QuestTypeWriter(args, "-edit -mobjType ", QuestReader.MainObjHexIndex); }
                        if (WriteSubAQuestType) { QuestWriter.QuestTypeWriter(args, "-edit -aobjType ", QuestReader.SubAObjHexIndex); }
                        if (WriteSubBQuestType) { QuestWriter.QuestTypeWriter(args, "-edit -bobjType ", QuestReader.SubBObjHexIndex); }

                        if (WriteMainQuestGoal) { QuestWriter.QuestGoalWriter(args, "-edit -mobjGoal ", QuestReader.MainObjHexIndex, QuestReader.MainObjIndex); }
                        if (WriteSubAQuestGoal) { QuestWriter.QuestGoalWriter(args, "-edit -aobjGoal ", QuestReader.SubAObjHexIndex, QuestReader.SubAObjIndex); }
                        if (WriteSubBQuestGoal) { QuestWriter.QuestGoalWriter(args, "-edit -bobjGoal ", QuestReader.SubBObjHexIndex, QuestReader.SubBObjIndex); }

                        if (WriteQuestFee && !isUnsafe) { QuestWriter.QuestFeeWriter(args, "-edit -questFee ", QuestReader.QuestFeeIndex, false); }
                        if (WriteQuestFee && isUnsafe) { QuestWriter.QuestFeeWriter(args, "-edit -unsafe -questFee ", QuestReader.QuestFeeIndex, true); }

                        if (WriteQuestPrimaryReward && !isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -questRewardMain ", QuestReader.PrimaryRewardIndex, false); }
                        if (WriteQuestPrimaryReward && isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -unsafe -questRewardMain ", QuestReader.PrimaryRewardIndex, true); }

                        if (WriteQuestRewardA && !isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -questRewardA ", QuestReader.RewardAIndex, false); }
                        if (WriteQuestRewardA && isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -unsafe -questRewardA ", QuestReader.RewardAIndex, true); }

                        if (WriteQuestRewardB && !isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -questRewardB ", QuestReader.RewardBIndex, false); }
                        if (WriteQuestRewardB && isUnsafe) { QuestWriter.QuestRewardWriter(args, "-edit -unsafe -questRewardB ", QuestReader.RewardBIndex, true); }

                        if (WriteQuestRank) { QuestWriter.QuestRankWriter(args, "-edit -questRank", QuestReader.RankIndex); }

                        ExecuteWriteFile = QuestWriter.ExecuteWriteFile;
                        Console.WriteLine(ExecuteWriteFile);

                        if (ExecuteWriteFile)
                        {
                            if (Debug)
                            {
                                Console.WriteLine("[DEBUG] Debug mode is enabled.");
                            }
                            else
                            {
                                Console.WriteLine("Writing to file: {0}", outputFileName);
                                File.WriteAllBytes(outputFileName, QuestWriter.NewFile);
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
