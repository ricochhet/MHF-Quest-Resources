using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.Text.RegularExpressions;

namespace QuestBinTools
{
    class Program
    {
        private static byte[] BaseFile;
        private static BinaryReader brInput;

        private static string ReturnLocationInfo;

        private static string ReturnRankInfo;
        private static string ReturnRankValue;
        private static string ReturnRankBands;
        private static string ReturnRankUnk;

        private static string ReturnQuestFee;
        private static string ReturnPrimaryReward;
        private static string ReturnRewardA;
        private static string ReturnRewardB;

        private static string ReturnMonsterVariant1AInfo;
        private static string ReturnMonsterVariant2AInfo;
        private static string ReturnMonsterVariant1BInfo;
        private static string ReturnMonsterVariant2BInfo;

        private static string ReturnObjectiveMainHex;
        private static string ReturnObjectiveMainType;
        private static string ReturnObjectiveMainQuant;
        private static string ReturnMainObj;

        private static string ReturnObjectiveSubAHex;
        private static string ReturnObjectiveSubAType;
        private static string ReturnObjectiveSubAQuant;
        private static string ReturnSubAObj;

        private static string ReturnObjectiveSubBHex;
        private static string ReturnObjectiveSubBType;
        private static string ReturnObjectiveSubBQuant;
        private static string ReturnSubBObj;

        private static string ReturnDeliverString;
        private static string ReturnQuestTypeName;
        private static string ReturnObjMainString;
        private static string ReturnObjAString;
        private static string ReturnObjBString;
        private static string ReturnClearReqString;
        private static string ReturnFailReqString;
        private static string ReturnHirerString;
        private static string ReturnDescriptionString;

        private static string fileName;
        private static StreamWriter writeStream;
        private static bool CreateLogFile;
        private static bool StrToHex;

        private static int QuestStringsStartIndexA = 48;
        private static int QuestStringsStartIndexB = 232;
        private static int QuestStringsStartPointer = 4;

        private static int QuestFeeIndex = 204;
        private static int PrimaryRewardIndex = 208;
        private static int RewardAIndex = 216;
        private static int RewardBIndex = 220;

        private static int LocationIndex = 228;
        
        private static int RankIndex = 72;

        private static int Variant1AIndex = 337;
        private static int Variant2AIndex = 338;
        private static int Variant1BIndex = 345;
        private static int Variant2BIndex = 346;

        private static int ObjectiveHexIncIndexA = 1;
        private static int ObjectiveHexIncIndexB = 2;
        private static int ObjectiveHexIncIndexC = 3;

        private static int InterceptionLoopIndexA = 377;
        private static int InterceptionLoopIndexB = 382;
        private static int InterceptionLoopIndexSub = 1;

        private static int MonsterCoordStartIndex = 24;
        private static int MonsterCoordTypePointer = 8;
        private static int MonsterCoordTypePointerIncIndex = 4;
        private static int MonsterCoordSpawnIncIndex = 1;
        private static int MonsterCoordStatPointer = 12;
        private static int MonsterCoordCheckIndex = 0;
        private static int MonsterCoordLoopIndexSub = 1;

        private static int MainObjHexIndex = 240;
        private static int MainObjIndex = 244;
        private static int MainObjQuantIndex = 246;
        private static int MainObjQuantMult = 100;

        private static int SubAObjHexIndex = 248;
        private static int SubAObjIndex = 252;
        private static int SubAObjQuantIndex = 254;
        private static int SubAObjQuantMult = 100;

        private static int SubBObjHexIndex = 256;
        private static int SubBObjIndex = 260;
        private static int SubBObjQuantIndex = 262;
        private static int SubBObjQuantMult = 100;

        public static Structs.QuestInfo FileLoader(string FilePath)
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

        public static byte[] UnsignedBytesFromSignedBytes(sbyte[] signed)
        {
            var unsigned = new byte[signed.Length];
            Buffer.BlockCopy(signed, 0, unsigned, 0, signed.Length);
            return unsigned;
        }

        static string ReturnByteArrayString(byte[] bytes)
        {
            var sb = new StringBuilder(/*"new byte[] { "*/);
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            // sb.Append("}");
            // Console.WriteLine(sb.ToString());

            return sb.ToString();
        }

        static string ReturnByteArrayHexString(byte[] bytes)
        {
            var sb = new StringBuilder(/*"new byte[] { "*/);
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }
            // sb.Append("}");
            // Console.WriteLine(sb.ToString());

            return sb.ToString();
        }

        public static string ReadNullTerminatedString(BinaryReader brInput, Encoding encoding)
        {
            var charByteList = new List<byte>();
            string str = "";
            if (brInput.BaseStream.Position == brInput.BaseStream.Length)
            {
                var charByteArray = charByteList.ToArray();
                str = encoding.GetString(charByteArray);
                return str;
            }

            byte b = brInput.ReadByte();
            while (b != 0x0 && brInput.BaseStream.Position != brInput.BaseStream.Length)
            {
                charByteList.Add(b);
                b = brInput.ReadByte();
            }

            var char_bytes = charByteList.ToArray();
            str = encoding.GetString(char_bytes);

            Console.WriteLine("\n===== NULL TERMINATED STRING ====={0}",
                $"\nValue: {str.Replace("\n", "<NLINE>")}\n\nBytes: {ReturnByteArrayString(char_bytes)}\n\nHex: {ReturnByteArrayHexString(char_bytes)}\n");

            WriteLine("\n===== NULL TERMINATED STRING ====={0}",
                $"\nValue: {str.Replace("\n", "<NLINE>")}\n\nBytes: {ReturnByteArrayString(char_bytes)}\n\nHex: {ReturnByteArrayHexString(char_bytes)}\n");

            return str;
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

        static void WriteLine(string String, params object[] Objs) 
        {
            using(writeStream = new StreamWriter(fileName, true)) {
                writeStream.WriteLine(String, Objs);
            }
        }

        static void WriteLine(string String) 
        {
            using(writeStream = new StreamWriter(fileName, true)) {
                writeStream.WriteLine(String);
            }
        }

        static void Main(string[] args)
        {
            /* We have to specify an extra coding provider for more encoding options. 
            https://docs.microsoft.com/en-us/dotnet/api/system.text.codepagesencodingprovider?view=net-5.0
            */
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (args.Length < 1) { Console.WriteLine("Too few arguments."); return; }
            string input = args[0];
            if (args.Any("-log".Contains)) { CreateLogFile = true; StrToHex = false;}
            if (args.Any("-strToHex".Contains)) { StrToHex = true; }

            if (StrToHex) 
            {
                string pattern = "-strToHex ";
                string argStr = string.Join(" ", args, 1, args.Length - 1).Replace(pattern, "");

                Console.WriteLine(StringToHex(argStr, "shift-jis"));
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
                    FileLoader(input);
                    if (CreateLogFile) 
                    {
                        Directory.CreateDirectory("output");
                        fileName = $"output\\{Path.GetFileName(input)}.txt";
                        // File.Open(fileName, FileMode.Create);
                        if (File.Exists(fileName))    
                        {    
                            // File.Delete(fileName);    
                        }  
                        using(File.Create(fileName)) 
                        {
                            Console.WriteLine("Creating LOGFILE: {0}", fileName);
                            // writeStream = new StreamWriter(fileName);
                        }

                        // writeStream = new StreamWriter(fileName);
                    }

                    Items.initiate();
                    
                    Console.WriteLine("======================\n===== QUEST DATA =====\n======================\n\n");

                    WriteLine($"{Path.GetFileName(input)}");
                    WriteLine("======================\n===== QUEST DATA =====\n======================\n\n");

                    Console.WriteLine("===============================\n========== RANK DATA ==========\n===============================");
                    WriteLine("===============================\n========== RANK DATA ==========\n===============================");

                    LoadRank(BaseFile);
                    Console.WriteLine("\n\n===== RANK INFO: ====={0}\n===== RANK VALUE: ====={1}\n===== RANK BANDS: ====={2}\n==== RANK UNK: ====={3}\n", 
                        ReturnRankInfo, ReturnRankValue, ReturnRankBands, ReturnRankUnk);

                    WriteLine("\n\n===== RANK INFO: ====={0}\n===== RANK VALUE: ====={1}\n===== RANK BANDS: ====={2}\n==== RANK UNK: ====={3}\n", 
                        ReturnRankInfo, ReturnRankValue, ReturnRankBands, ReturnRankUnk);
                    
                    LoadRewardInfo(BaseFile);
                    Console.WriteLine("===== QUEST FEE: ====={0}\n===== PRIMARY REWARD: ====={1}\n===== REWARD A ====={2}\n===== REWARD B ====={3}\n",
                        ReturnQuestFee, ReturnPrimaryReward, ReturnRewardA, ReturnRewardB);

                    WriteLine("===== QUEST FEE: ====={0}\n===== PRIMARY REWARD: ====={1}\n===== REWARD A ====={2}\n===== REWARD B ====={3}\n",
                        ReturnQuestFee, ReturnPrimaryReward, ReturnRewardA, ReturnRewardB);

                    Console.WriteLine("===================================\n========== LOCATION DATA ==========\n===================================");
                    WriteLine("===================================\n========== LOCATION DATA ==========\n===================================");

                    LoadLocations(BaseFile);
                    Console.WriteLine("\n\n===== LOCATION: ====={0}\n", 
                        ReturnLocationInfo);

                    WriteLine("\n\n===== LOCATION: ====={0}\n", 
                        ReturnLocationInfo);

                    Console.WriteLine("==========================================\n========== MONSTER VARIANT DATA ==========\n==========================================");
                    WriteLine("==========================================\n========== MONSTER VARIANT DATA ==========\n==========================================");

                    LoadMonsterVariant(BaseFile);
                    Console.WriteLine("\n\n===== MONSTER VARIANT 1A: ====={0}\n===== MONSTER VARIANT 1B: ====={1}\n===== MONSTER VARIANT 2A: ====={2}\n===== MONSTER VARIANT 2B: ====={3}\n", 
                        ReturnMonsterVariant1AInfo, ReturnMonsterVariant2AInfo, ReturnMonsterVariant1BInfo, ReturnMonsterVariant2BInfo);

                    WriteLine("\n\n===== MONSTER VARIANT 1A: ====={0}\n===== MONSTER VARIANT 1B: ====={1}\n===== MONSTER VARIANT 2A: ====={2}\n===== MONSTER VARIANT 2B: ====={3}\n", 
                        ReturnMonsterVariant1AInfo, ReturnMonsterVariant2AInfo, ReturnMonsterVariant1BInfo, ReturnMonsterVariant2BInfo);

                    Console.WriteLine("===================================\n========== MAIN OBJ DATA ==========\n===================================");
                    WriteLine("===================================\n========== MAIN OBJ DATA ==========\n===================================");

                    LoadMainObjective(BaseFile);
                    Console.WriteLine("\n\n===== MAIN OBJECTIVE HEX: ====={0}\n===== MAIN OBJECTIVE TYPE: ====={1}\n===== MAIN OBJECTIVE QUANT: ====={2}\n===== MAIN OBJECTIVE: ====={3}\n",
                        ReturnObjectiveMainHex, ReturnObjectiveMainType, ReturnObjectiveMainQuant, ReturnMainObj);

                    WriteLine("\n\n===== MAIN OBJECTIVE HEX: ====={0}\n===== MAIN OBJECTIVE TYPE: ====={1}\n===== MAIN OBJECTIVE QUANT: ====={2}\n===== MAIN OBJECTIVE: ====={3}\n",
                        ReturnObjectiveMainHex, ReturnObjectiveMainType, ReturnObjectiveMainQuant, ReturnMainObj);

                    Console.WriteLine("====================================\n========== SUB A OBJ DATA ==========\n====================================");
                    WriteLine("====================================\n========== SUB A OBJ DATA ==========\n====================================");

                    LoadSubAObjective(BaseFile);
                    Console.WriteLine("\n\n===== SUB A OBJECTIVE HEX: ====={0}\n===== SUB A OBJECTIVE TYPE: ====={1}\n===== SUB A OBJECTIVE QUANT: ====={2}\n===== SUB A OBJECTIVE: ====={3}\n",
                        ReturnObjectiveSubAHex, ReturnObjectiveSubAType, ReturnObjectiveSubAQuant, ReturnSubAObj); 

                    WriteLine("\n\n===== SUB A OBJECTIVE HEX: ====={0}\n===== SUB A OBJECTIVE TYPE: ====={1}\n===== SUB A OBJECTIVE QUANT: ====={2}\n===== SUB A OBJECTIVE: ====={3}\n",
                        ReturnObjectiveSubAHex, ReturnObjectiveSubAType, ReturnObjectiveSubAQuant, ReturnSubAObj); 

                    Console.WriteLine("====================================\n========== SUB B OBJ DATA ==========\n====================================");
                    WriteLine("====================================\n========== SUB B OBJ DATA ==========\n====================================");

                    LoadSubBObjective(BaseFile);
                    Console.WriteLine("\n\n===== SUB B OBJECTIVE HEX: ====={0}\n===== SUB B OBJECTIVE TYPE: ====={1}\n===== SUB B OBJECTIVE QUANT: ====={2}\n===== SUB B OBJECTIVE: ====={3}\n",
                        ReturnObjectiveSubBHex, ReturnObjectiveSubBType, ReturnObjectiveSubBQuant, ReturnSubBObj);

                    WriteLine("\n\n===== SUB B OBJECTIVE HEX: ====={0}\n===== SUB B OBJECTIVE TYPE: ====={1}\n===== SUB B OBJECTIVE QUANT: ====={2}\n===== SUB B OBJECTIVE: ====={3}\n",
                        ReturnObjectiveSubBHex, ReturnObjectiveSubBType, ReturnObjectiveSubBQuant, ReturnSubBObj);

                    Console.WriteLine("========================================\n========== QUEST TEXT STRINGS ==========\n========================================\n");
                    WriteLine("========================================\n========== QUEST TEXT STRINGS ==========\n========================================\n");

                    
                    LoadQuestTextStrings(BaseFile);
                }
            }
            else Console.WriteLine("ERROR: Input file does not exist.");
        }

        static void LoadQuestTextStrings(byte[] FileData) {
            int QuestStringsStart = BitConverter.ToInt32(FileData, QuestStringsStartIndexA);
            int ReadPointer = BitConverter.ToInt32(FileData, QuestStringsStart);
            brInput.BaseStream.Seek(ReadPointer, SeekOrigin.Begin);

            ReturnDeliverString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("Shift-JIS")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== DELIVER STRING =====\n{0}",
                $"Value: {ReturnDeliverString}\n\n\n");

            WriteLine("===== DELIVER STRING =====\n{0}",
                $"Value: {ReturnDeliverString}\n\n\n");

            QuestStringsStart = BitConverter.ToInt32(FileData, QuestStringsStartIndexB);
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnQuestTypeName = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== QUEST TYPE NAME =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            WriteLine("===== QUEST TYPE NAME =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);

            ReturnObjMainString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ MAIN STRING =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            WriteLine("===== OBJ MAIN STRING =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjAString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ A STRING =====\n{0}",
                $"Value: {ReturnObjAString}\n\n\n");

            WriteLine("===== OBJ A STRING =====\n{0}",
                $"Value: {ReturnObjAString}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjBString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ B STRING =====\n{0}",
                $"Value: {ReturnObjBString}\n\n\n");

            WriteLine("===== OBJ B STRING =====\n{0}",
                $"Value: {ReturnObjBString}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnClearReqString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== CLEAR REQ STRING =====\n{0}",
                $"Value: {ReturnClearReqString}\n\n\n");

            WriteLine("===== CLEAR REQ STRING =====\n{0}",
                $"Value: {ReturnClearReqString}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnFailReqString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== FAIL REQ STRING =====\n{0}",
                $"Value: {ReturnFailReqString}\n\n\n");

            WriteLine("===== FAIL REQ STRING =====\n{0}",
                $"Value: {ReturnFailReqString}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnHirerString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== HIRER STRING =====\n{0}",
                $"Value: {ReturnHirerString}\n\n\n");

            WriteLine("===== HIRER STRING =====\n{0}",
                $"Value: {ReturnHirerString}\n\n\n");

            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnDescriptionString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== DESCRIPTION STRING =====\n{0}",
                $"Value: {ReturnDescriptionString}\n\n\n");

            WriteLine("===== DESCRIPTION STRING =====\n{0}",
                $"Value: {ReturnDescriptionString}\n\n\n");
        }

        public static string ReturnItem(byte[] FileData, int index)
        {
            string item = null;
            if (Items.ItemIDs.TryGetValue(BitConverter.ToInt16(FileData, index), out item))
            {
                //
            }
            else
            {
                item = BitConverter.ToInt16(FileData, index).ToString();
            }

            Console.WriteLine("===== ITEM INFO: ====={0}\n", 
                $"\nValue: {item}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, index)}\nBy Index: {FileData[index]}\nBy Index (Hex): 0x{FileData[index].ToString("X2")}\nIndex: {index}\n");
            
            WriteLine("===== ITEM INFO: ====={0}\n", 
                $"\nValue: {item}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, index)}\nBy Index: {FileData[index]}\nBy Index (Hex): 0x{FileData[index].ToString("X2")}\nIndex: {index}\n");
            

            return item;
        }

        public static string ReturnObjectiveHex(byte[] FileData, int index)
        {
            Console.WriteLine("\n\n===== OBJECTIVE HEX: ====={0}",
                $"\nValue: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] }).Replace("-", "")}\nIndexes 1: INDEX: {FileData[index]} | INDEX+1: {FileData[index + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[index + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[index + ObjectiveHexIncIndexC]}\nHex 1: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] })}\nHex 2: {FileData[index]} {FileData[index + ObjectiveHexIncIndexA]} {FileData[index + ObjectiveHexIncIndexB]} {FileData[index + ObjectiveHexIncIndexC]}\nIndex: {index}\n");

            WriteLine("\n\n===== OBJECTIVE HEX: ====={0}",
                $"\nValue: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] }).Replace("-", "")}\nIndexes 1: INDEX: {FileData[index]} | INDEX+1: {FileData[index + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[index + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[index + ObjectiveHexIncIndexC]}\nHex 1: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] })}\nHex 2: {FileData[index]} {FileData[index + ObjectiveHexIncIndexA]} {FileData[index + ObjectiveHexIncIndexB]} {FileData[index + ObjectiveHexIncIndexC]}\nIndex: {index}\n");

            return BitConverter.ToString(new byte[] { FileData[index], FileData[index + 1], FileData[index + 2], FileData[index + 3] }).Replace("-", "");
        }

        public static string ReturnMonster(byte[] FileData, int index)
        {
            string monster = null;
            if (Monsters.MonsterNames.TryGetValue(FileData[index], out monster))
            {
                //
            }
            else
            {
                monster = BitConverter.ToInt16(FileData, index).ToString();
            }

            Console.WriteLine("===== MONSTER INFO A: ====={0}\n", 
                $"\nValue: {monster}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, index)}\nBy Index: {FileData[index]}\nBy Index (Hex): 0x{FileData[index].ToString("X2")}\nIndex: {index}\n");
            
            WriteLine("===== MONSTER INFO A: ====={0}\n", 
                $"\nValue: {monster}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, index)}\nBy Index: {FileData[index]}\nBy Index (Hex): 0x{FileData[index].ToString("X2")}\nIndex: {index}\n");

            return monster;
        }

        public static string ReturnMonster(int monsterID)
        {
            string monster = null;
            if (Monsters.MonsterNames.TryGetValue((byte)monsterID, out monster))
            {
                //
            }
            else
            {
                monster = monsterID.ToString();
            }

            Console.WriteLine("===== MONSTER INFO B: ====={0}\n", 
                $"\nValue: {monster}\nID: {(byte)monsterID}\nStr: {monsterID.ToString()}");

            WriteLine("===== MONSTER INFO B: ====={0}\n", 
                $"\nValue: {monster}\nID: {(byte)monsterID}\nStr: {monsterID.ToString()}");

            return monster;
        }

        public static string ReturnInterception(byte[] FileData)
        {
            string monster = "";
            string monsterAdd = null;
            for (int i = InterceptionLoopIndexA; i <= InterceptionLoopIndexB - InterceptionLoopIndexSub; i++)
            {
                if (FileData[i] == 0)
                    continue;
                if (Monsters.MonsterNames.TryGetValue(FileData[i], out monsterAdd))
                {
                    Console.WriteLine("\n\n===== INTERCEPTION DATA SECT 1 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");

                    WriteLine("\n\n===== INTERCEPTION DATA SECT 1 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                }
                else
                {
                    Console.WriteLine("\n\n===== INTERCEPTION DATA SECT 2 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nSingle {BitConverter.ToSingle(FileData, i).ToString()} / {BitConverter.ToSingle(FileData, i)}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                    
                    WriteLine("\n\n===== INTERCEPTION DATA SECT 2 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nSingle {BitConverter.ToSingle(FileData, i).ToString()} / {BitConverter.ToSingle(FileData, i)}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                        
                    monsterAdd = BitConverter.ToSingle(FileData, i).ToString();
                }

                if (i == InterceptionLoopIndexA)
                {
                    Console.WriteLine("\n\n===== INTERCEPTION DATA SECT 3 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                    
                    WriteLine("\n\n===== INTERCEPTION DATA SECT 3 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                    
                    monster += monsterAdd;
                }
                else
                {
                    Console.WriteLine("\n\n===== INTERCEPTION DATA SECT 4 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");

                    WriteLine("\n\n===== INTERCEPTION DATA SECT 4 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");

                    monster += $", {monsterAdd}";
                }
            }

            Console.WriteLine("\n\n===== INTERCEPTION SECT 5 ====={0}",
                $"\nValue: {monster}\nAdd: {monsterAdd}\n");

            WriteLine("\n\n===== INTERCEPTION SECT 5 ====={0}",
                $"\nValue: {monster}\nAdd: {monsterAdd}\n");

            return monster;
        }

        static void LoadRewardInfo(byte[] FileData)
        {
            int QuestFee = BitConverter.ToInt32(FileData, QuestFeeIndex);
            int PrimaryReward = BitConverter.ToInt32(FileData, PrimaryRewardIndex);
            int RewardA = BitConverter.ToInt32(FileData, RewardAIndex);
            int RewardB = BitConverter.ToInt32(FileData, RewardBIndex);

            ReturnQuestFee = $"\nValue: {QuestFee}\nInt32 (LE): {BitConverter.ToInt32(FileData, QuestFeeIndex)}\nBy Index: {FileData[QuestFeeIndex]}\nBy Index (Hex): 0x{FileData[QuestFeeIndex].ToString("X2")}\nIndex: {QuestFeeIndex}\n";
            ReturnPrimaryReward = $"\nValue: {PrimaryReward}\nInt32 (LE): {BitConverter.ToInt32(FileData, PrimaryRewardIndex)}\nBy Index: {FileData[PrimaryRewardIndex]}\nBy Index (Hex): 0x{FileData[PrimaryRewardIndex].ToString("X2")}\nIndex: {PrimaryRewardIndex}\n";
            ReturnRewardA = $"\nValue: {RewardA}\nInt32 (LE): {BitConverter.ToInt32(FileData, RewardAIndex)}\nBy Index: {FileData[RewardAIndex]}\nBy Index (Hex): 0x{FileData[RewardAIndex].ToString("X2")}\nIndex: {RewardAIndex}\n";
            ReturnRewardB = $"\nValue: {RewardB}\nInt32 (LE): {BitConverter.ToInt32(FileData, RewardBIndex)}\nBy Index: {FileData[RewardBIndex]}\nBy Index (Hex): 0x{FileData[RewardBIndex].ToString("X2")}\nIndex: {RewardBIndex}\n";
        }

        static void LoadLocations(byte[] FileData)
        {
            string location = null;
            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(FileData, LocationIndex), out location))
            {
                //
            }

            ReturnLocationInfo = $"\nValue: {location}\nInt32 (LE): {BitConverter.ToInt32(FileData, LocationIndex)}\nBy Index: {FileData[LocationIndex]}\nBy Index (Hex): 0x{FileData[LocationIndex].ToString("X2")}\nIndex: {LocationIndex}\n";
        }

        static void LoadRank(byte[] FileData) 
        {
            string StatTable = null;
            int RankValue = 0;
            string RankBands = null;
            string RankUnk = null;
            if (Ranks.RankBands.TryGetValue(BitConverter.ToInt32(FileData, RankIndex), out StatTable))
            {
                RankValue = BitConverter.ToInt32(FileData, RankIndex);
                RankBands = StatTable;
                StatTable = $"{BitConverter.ToInt32(FileData, RankIndex)}   |   {StatTable}";
            }
            else
            {
                RankUnk = BitConverter.ToInt32(FileData, RankIndex).ToString();
                StatTable = BitConverter.ToInt32(FileData, RankIndex).ToString();
            }

            ReturnRankInfo = $"\nValue: {StatTable}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankValue = $"\nValue: {RankValue}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankBands = $"\nValue: {RankBands}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankUnk = $"\nValue: {RankUnk}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
        }

        static void LoadMonsterVariant(byte[] FileData) 
        {
            string Variant1A = FileData[Variant1AIndex].ToString("X2");
            string Variant2A = FileData[Variant2AIndex].ToString("X2");
            string Variant1B = FileData[Variant1BIndex].ToString("X2");
            string Variant2B = FileData[Variant2BIndex].ToString("X2");

            ReturnMonsterVariant1AInfo = $"\nValue: {Variant1A}\nData: {FileData[Variant1AIndex]}\nBy Index: {FileData[Variant1AIndex]}\nBy Index (Hex): 0x{FileData[Variant1AIndex].ToString("X2")}\nIndex: {Variant1AIndex}\nString: X2\n";
            ReturnMonsterVariant2AInfo = $"\nValue: {Variant2A}\nData: {FileData[Variant2AIndex]}\nBy Index: {FileData[Variant2AIndex]}\nBy Index (Hex): 0x{FileData[Variant2AIndex].ToString("X2")}\nIndex: {Variant2AIndex}\nString: X2\n";;
            ReturnMonsterVariant1BInfo = $"\nValue: {Variant1B}\nData: {FileData[Variant1BIndex]}\nBy Index: {FileData[Variant1BIndex]}\nBy Index (Hex): 0x{FileData[Variant1BIndex].ToString("X2")}\nIndex: {Variant1BIndex}\nString: X2\n";;
            ReturnMonsterVariant2BInfo = $"\nValue: {Variant2B}\nData: {FileData[Variant2BIndex]}\nBy Index: {FileData[Variant2BIndex]}\nBy Index (Hex): 0x{FileData[Variant2BIndex].ToString("X2")}\nIndex: {Variant2BIndex}\nString: X2\n";;
        }

        static void LoadMonsterCoords(byte[] FileData) 
        {
            int monsterStart = BitConverter.ToInt32(FileData, MonsterCoordStartIndex);
            int monsterTypePointer = BitConverter.ToInt32(FileData, monsterStart + MonsterCoordTypePointer);
            int monsterSpawns = 0;
            while (BitConverter.ToInt32(FileData, monsterTypePointer) > MonsterCoordCheckIndex) 
            {
                monsterTypePointer += MonsterCoordTypePointerIncIndex;
                monsterSpawns += MonsterCoordSpawnIncIndex;
            }

            int monsterStatPointer = BitConverter.ToInt32(FileData, monsterStart + MonsterCoordStatPointer);
            Console.WriteLine($"{monsterStatPointer}");
            if (monsterSpawns > MonsterCoordCheckIndex) 
            {
                var MonsterData = new Structs.MonsterSpawn[monsterSpawns];
                brInput.BaseStream.Seek(monsterStatPointer, SeekOrigin.Begin);
                for (int i = 0, loopTo = monsterSpawns - MonsterCoordLoopIndexSub; i <= loopTo; i++) 
                {
                    var cMon = new Structs.MonsterSpawn();
                    cMon.Monster = ReturnMonster(brInput.ReadInt32());
                    cMon.Unk1 = brInput.ReadInt32();
                    cMon.Unk2 = brInput.ReadInt32();
                    cMon.Unk3 = brInput.ReadInt32();
                    cMon.Unk4 = brInput.ReadInt32();
                    cMon.Unk5 = brInput.ReadInt32();
                    cMon.Unk6 = brInput.ReadInt32();
                    cMon.Unk7 = brInput.ReadInt32();
                    cMon.XPos = brInput.ReadSingle();
                    cMon.ZPos = brInput.ReadSingle();
                    cMon.YPos = brInput.ReadSingle();
                    cMon.Unk8 = brInput.ReadInt32();
                    cMon.Unk9 = brInput.ReadInt32();
                    cMon.Unk10 = brInput.ReadInt32();
                    cMon.Unk11 = brInput.ReadInt32();
                    MonsterData[i] = cMon;
                }

                brInput.Close();
            }
        }

        static void LoadMainObjective(byte[] FileData)
        {
            string objectiveMainHex = ReturnObjectiveHex(FileData, MainObjHexIndex);
            string objectiveMainType = null;
            object ObjectiveMainQuant;
            string MainObj = null;

            if (QuestObjectives.Objectives.TryGetValue(objectiveMainHex, out objectiveMainType))
            {
                //
            }
            else
            {
                objectiveMainType = objectiveMainHex;
            }

            if (objectiveMainType == "Hunt" | objectiveMainType == "Slay" | objectiveMainType == "Damage" | objectiveMainType == "Slay or Damage" | objectiveMainType == "Capture")
            {
                MainObj = ReturnMonster(FileData, MainObjIndex);
            }
            else if (objectiveMainType == "Break Part")
            {
                MainObj = BitConverter.ToInt16(FileData, MainObjIndex).ToString();
            }
            else if (objectiveMainType == "Slay All")
            {
                MainObj = ReturnInterception(FileData);
            }
            else
            {
                MainObj = ReturnItem(FileData, MainObjIndex);
            }

            ObjectiveMainQuant = BitConverter.ToInt16(FileData, MainObjQuantIndex);
            if (objectiveMainType == "Damage" || objectiveMainType == "Slay or Damage")
            {
                ObjectiveMainQuant = (int)ObjectiveMainQuant * MainObjQuantMult;
            }
            
            ReturnObjectiveMainHex = $"\nValue: {objectiveMainHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, MainObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[MainObjHexIndex]} | INDEX+1: {FileData[MainObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[MainObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[MainObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[MainObjHexIndex], FileData[MainObjHexIndex + ObjectiveHexIncIndexA], FileData[MainObjHexIndex + ObjectiveHexIncIndexB], FileData[MainObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[MainObjHexIndex]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexA]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexB]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {MainObjHexIndex}\n";
            ReturnObjectiveMainType = $"\nValue: {objectiveMainType}\nIndex: N/A\n";
            ReturnObjectiveMainQuant = $"\nValue: {ObjectiveMainQuant} (* {MainObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, MainObjQuantIndex)}\nIndex: {MainObjQuantIndex}\n";
            ReturnMainObj = $"\nValue: {MainObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, MainObjIndex).ToString()} / {BitConverter.ToInt16(FileData, MainObjIndex)}\nBy Index: {FileData[MainObjIndex]}\nBy Index (Hex): 0x{FileData[MainObjIndex].ToString("X2")}\nIndex: {MainObjIndex}\n";
        }

        static void LoadSubAObjective(byte[] FileData)
        {
            string objectiveSubAHex = ReturnObjectiveHex(FileData, SubAObjHexIndex);
            string objectiveSubAType = null;
            object ObjectiveSubAQuant;
            string SubAObj  = null;

            if (QuestObjectives.Objectives.TryGetValue(objectiveSubAHex, out objectiveSubAHex))
            {
                //
            }
            else
            {
                objectiveSubAType = objectiveSubAHex;
            }

            if (objectiveSubAType == "Hunt" | objectiveSubAType == "Slay" | objectiveSubAType == "Damage" | objectiveSubAType == "Slay or Damage" | objectiveSubAType == "Capture")
            {
                SubAObj = ReturnMonster(FileData, SubAObjIndex);
            }
            else if (objectiveSubAType == "Break Part")
            {
                SubAObj = BitConverter.ToInt16(FileData, SubAObjIndex).ToString();
            }
            else if (objectiveSubAType == "Slay All")
            {
                SubAObj = ReturnInterception(FileData);
            }
            else
            {
                SubAObj = ReturnItem(FileData, SubAObjIndex);
            }

            if (SubAObj == "0" | string.IsNullOrEmpty(SubAObj))
            {
                SubAObj = "None";
            }

            ObjectiveSubAQuant = BitConverter.ToInt16(FileData, SubAObjQuantIndex);
            if (objectiveSubAType == "Damage" || objectiveSubAType == "Slay or Damage")
            {
                ObjectiveSubAQuant = (int)ObjectiveSubAQuant * SubAObjQuantMult;
            }

            ReturnObjectiveSubAHex = $"\nValue: {objectiveSubAHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, SubAObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[SubAObjHexIndex]} | INDEX+1: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[SubAObjHexIndex], FileData[SubAObjHexIndex + ObjectiveHexIncIndexA], FileData[SubAObjHexIndex + ObjectiveHexIncIndexB], FileData[SubAObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[SubAObjHexIndex]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {SubAObjHexIndex}\n";
            ReturnObjectiveSubAType = $"\nValue: {objectiveSubAType}\nIndex: N/A\n";
            ReturnObjectiveSubAQuant = $"\nValue: {ObjectiveSubAQuant} (* {SubAObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubAObjQuantIndex)}\nIndex: {SubAObjQuantIndex}\n";
            ReturnSubAObj = $"\nValue: {SubAObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubAObjIndex).ToString()} / {BitConverter.ToInt16(FileData, SubAObjIndex)}\nBy Index: {FileData[SubAObjIndex]}\nBy Index (Hex): 0x{FileData[SubAObjIndex].ToString("X2")}\nIndex: {SubAObjIndex}\n";
        }

        static void LoadSubBObjective(byte[] FileData)
        {
            string objectiveSubBHex = ReturnObjectiveHex(FileData, SubBObjHexIndex);
            string objectiveSubBType = null;
            object ObjectiveSubBQuant;
            string SubBObj  = null;

            if (QuestObjectives.Objectives.TryGetValue(objectiveSubBHex, out objectiveSubBHex))
            {
                //
            }
            else
            {
                objectiveSubBType = objectiveSubBHex;
            }

            if (objectiveSubBType == "Hunt" | objectiveSubBType == "Slay" | objectiveSubBType == "Damage" | objectiveSubBType == "Slay or Damage" | objectiveSubBType == "Capture")
            {
                SubBObj = ReturnMonster(FileData, SubBObjIndex);
            }
            else if (objectiveSubBType == "Break Part")
            {
                SubBObj = BitConverter.ToInt16(FileData, SubBObjIndex).ToString();
            }
            else if (objectiveSubBType == "Slay All")
            {
                SubBObj = ReturnInterception(FileData);
            }
            else
            {
                SubBObj = ReturnItem(FileData, SubBObjIndex);
            }

            if (SubBObj == "0" | string.IsNullOrEmpty(SubBObj))
            {
                SubBObj = "None";
            }

            ObjectiveSubBQuant = BitConverter.ToInt16(FileData, SubBObjQuantIndex);
            if (objectiveSubBType == "Damage" || objectiveSubBType == "Slay or Damage")
            {
                ObjectiveSubBQuant = (int)ObjectiveSubBQuant * SubBObjQuantMult;
            }

            ReturnObjectiveSubBHex = $"\nValue: {objectiveSubBHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, SubBObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[SubBObjHexIndex]} | INDEX+1: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[SubBObjHexIndex], FileData[SubBObjHexIndex + ObjectiveHexIncIndexA], FileData[SubBObjHexIndex + ObjectiveHexIncIndexB], FileData[SubBObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[SubBObjHexIndex]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {SubBObjHexIndex}\n";
            ReturnObjectiveSubBType = $"\nValue: {objectiveSubBType}\nIndex: N/A\n";
            ReturnObjectiveSubBQuant = $"\nValue: {ObjectiveSubBQuant} (* {SubBObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubBObjQuantIndex)}\nIndex: {SubBObjQuantIndex}\n";
            ReturnSubBObj = $"\nValue: {SubBObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubBObjIndex).ToString()} / {BitConverter.ToInt16(FileData, SubBObjIndex)}\nBy Index: {FileData[SubBObjIndex]}\nBy Index (Hex): 0x{FileData[SubBObjIndex].ToString("X2")}\nIndex: {SubBObjIndex}\n";
        }
    }
}
