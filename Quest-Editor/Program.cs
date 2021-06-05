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

        /*private static string ReturnItemInfo;
        private static string ReturnMonsterInfoA;
        private static string ReturnMonsterInfoB;
        private static string ReturnInterceptionInfo;*/

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

                    // StringToHex("≪樹海探索≫\n樹海の特産【下位】");
                }
            }
            else Console.WriteLine("ERROR: Input file does not exist.");

            /* Input a quest path */
            // FileLoader("./QuestBinTools/QuestBins/21978d0.bin");
            // Items.initiate();
            // LoadMonsterVariant(BaseFile);
            // LoadRewardInfo(BaseFile);
            // LoadLocations(BaseFile);
            // LoadMainObjective(BaseFile);
        }

        static void LoadQuestTextStrings(byte[] FileData) {
            int QuestStringsStart = BitConverter.ToInt32(FileData, 48);
            int ReadPointer = BitConverter.ToInt32(FileData, QuestStringsStart);
            brInput.BaseStream.Seek(ReadPointer, SeekOrigin.Begin);

            ReturnDeliverString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("Shift-JIS")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== DELIVER STRING =====\n{0}",
                $"Value: {ReturnDeliverString}\n\n\n");

            WriteLine("===== DELIVER STRING =====\n{0}",
                $"Value: {ReturnDeliverString}\n\n\n");

            QuestStringsStart = BitConverter.ToInt32(FileData, 232);
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnQuestTypeName = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== QUEST TYPE NAME =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            WriteLine("===== QUEST TYPE NAME =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);

            ReturnObjMainString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ MAIN STRING =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            WriteLine("===== OBJ MAIN STRING =====\n{0}",
                $"Value: {ReturnQuestTypeName}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjAString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ A STRING =====\n{0}",
                $"Value: {ReturnObjAString}\n\n\n");

            WriteLine("===== OBJ A STRING =====\n{0}",
                $"Value: {ReturnObjAString}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjBString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== OBJ B STRING =====\n{0}",
                $"Value: {ReturnObjBString}\n\n\n");

            WriteLine("===== OBJ B STRING =====\n{0}",
                $"Value: {ReturnObjBString}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnClearReqString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== CLEAR REQ STRING =====\n{0}",
                $"Value: {ReturnClearReqString}\n\n\n");

            WriteLine("===== CLEAR REQ STRING =====\n{0}",
                $"Value: {ReturnClearReqString}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnFailReqString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== FAIL REQ STRING =====\n{0}",
                $"Value: {ReturnFailReqString}\n\n\n");

            WriteLine("===== FAIL REQ STRING =====\n{0}",
                $"Value: {ReturnFailReqString}\n\n\n");

            QuestStringsStart += 4;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnHirerString = ReadNullTerminatedString(brInput, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NLINE>");
            Console.WriteLine("===== HIRER STRING =====\n{0}",
                $"Value: {ReturnHirerString}\n\n\n");

            WriteLine("===== HIRER STRING =====\n{0}",
                $"Value: {ReturnHirerString}\n\n\n");

            QuestStringsStart += 4;
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
                $"\nValue: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + 1], FileData[index + 2], FileData[index + 3] }).Replace("-", "")}\nIndexes 1: INDEX: {FileData[index]} | INDEX+1: {FileData[index + 1]} | INDEX+2: {FileData[index + 2]} | INDEX+3: {FileData[index + 3]}\nHex 1: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + 1], FileData[index + 2], FileData[index + 3] })}\nHex 2: {FileData[index]} {FileData[index + 1]} {FileData[index + 2]} {FileData[index + 3]}\nIndex: {index}\n");

            WriteLine("\n\n===== OBJECTIVE HEX: ====={0}",
                $"\nValue: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + 1], FileData[index + 2], FileData[index + 3] }).Replace("-", "")}\nIndexes 1: INDEX: {FileData[index]} | INDEX+1: {FileData[index + 1]} | INDEX+2: {FileData[index + 2]} | INDEX+3: {FileData[index + 3]}\nHex 1: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + 1], FileData[index + 2], FileData[index + 3] })}\nHex 2: {FileData[index]} {FileData[index + 1]} {FileData[index + 2]} {FileData[index + 3]}\nIndex: {index}\n");

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
            for (int i = 377; i <= 382 - 1; i++)
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

                if (i == 377)
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
            int QuestFee = BitConverter.ToInt32(FileData, 204);
            int PrimaryReward = BitConverter.ToInt32(FileData, 208);
            int RewardA = BitConverter.ToInt32(FileData, 216);
            int RewardB = BitConverter.ToInt32(FileData, 220);

            ReturnQuestFee = $"\nValue: {QuestFee}\nInt32 (LE): {BitConverter.ToInt32(FileData, 204)}\nBy Index: {FileData[204]}\nBy Index (Hex): 0x{FileData[204].ToString("X2")}\nIndex: 204\n";
            ReturnPrimaryReward = $"\nValue: {PrimaryReward}\nInt32 (LE): {BitConverter.ToInt32(FileData, 208)}\nBy Index: {FileData[208]}\nBy Index (Hex): 0x{FileData[208].ToString("X2")}\nIndex: 208\n";
            ReturnRewardA = $"\nValue: {RewardA}\nInt32 (LE): {BitConverter.ToInt32(FileData, 216)}\nBy Index: {FileData[216]}\nBy Index (Hex): 0x{FileData[216].ToString("X2")}\nIndex: 216\n";
            ReturnRewardB = $"\nValue: {RewardB}\nInt32 (LE): {BitConverter.ToInt32(FileData, 220)}\nBy Index: {FileData[220]}\nBy Index (Hex): 0x{FileData[220].ToString("X2")}\nIndex: 220\n";
        }

        static void LoadLocations(byte[] FileData)
        {
            string location = null;
            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(FileData, 228), out location))
            {
                //
            }

            ReturnLocationInfo = $"\nValue: {location}\nInt32 (LE): {BitConverter.ToInt32(FileData, 228)}\nBy Index: {FileData[228]}\nBy Index (Hex): 0x{FileData[228].ToString("X2")}\nIndex: 228\n";
        }

        static void LoadRank(byte[] FileData) 
        {
            string StatTable = null;
            int RankValue = 0;
            string RankBands = null;
            string RankUnk = null;
            if (Ranks.RankBands.TryGetValue(BitConverter.ToInt32(FileData, 72), out StatTable))
            {
                RankValue = BitConverter.ToInt32(FileData, 72);
                RankBands = StatTable;
                StatTable = $"{BitConverter.ToInt32(FileData, 72)}   |   {StatTable}";
            }
            else
            {
                RankUnk = BitConverter.ToInt32(FileData, 72).ToString();
                StatTable = BitConverter.ToInt32(FileData, 72).ToString();
            }

            ReturnRankInfo = $"\nValue: {StatTable}\nInt32 (LE): {BitConverter.ToInt32(FileData, 72)}\nBy Index: {FileData[72]}\nBy Index (Hex): 0x{FileData[72].ToString("X2")}\nIndex: 72\n";
            ReturnRankValue = $"\nValue: {RankValue}\nInt32 (LE): {BitConverter.ToInt32(FileData, 72)}\nBy Index: {FileData[72]}\nBy Index (Hex): 0x{FileData[72].ToString("X2")}\nIndex: 72\n";
            ReturnRankBands = $"\nValue: {RankBands}\nInt32 (LE): {BitConverter.ToInt32(FileData, 72)}\nBy Index: {FileData[72]}\nBy Index (Hex): 0x{FileData[72].ToString("X2")}\nIndex: 72\n";
            ReturnRankUnk = $"\nValue: {RankUnk}\nInt32 (LE): {BitConverter.ToInt32(FileData, 72)}\nBy Index: {FileData[72]}\nBy Index (Hex): 0x{FileData[72].ToString("X2")}\nIndex: 72\n";
        }

        static void LoadMonsterVariant(byte[] FileData) 
        {
            string Variant1A = FileData[337].ToString("X2");
            string Variant2A = FileData[338].ToString("X2");
            string Variant1B = FileData[345].ToString("X2");
            string Variant2B = FileData[346].ToString("X2");

            ReturnMonsterVariant1AInfo = $"\nValue: {Variant1A}\nData: {FileData[337]}\nBy Index: {FileData[337]}\nBy Index (Hex): 0x{FileData[337].ToString("X2")}\nIndex: 337\nString: X2\n";
            ReturnMonsterVariant2AInfo = $"\nValue: {Variant2A}\nData: {FileData[338]}\nBy Index: {FileData[338]}\nBy Index (Hex): 0x{FileData[338].ToString("X2")}\nIndex: 338\nString: X2\n";;
            ReturnMonsterVariant1BInfo = $"\nValue: {Variant1B}\nData: {FileData[345]}\nBy Index: {FileData[345]}\nBy Index (Hex): 0x{FileData[345].ToString("X2")}\nIndex: 345\nString: X2\n";;
            ReturnMonsterVariant2BInfo = $"\nValue: {Variant2B}\nData: {FileData[346]}\nBy Index: {FileData[346]}\nBy Index (Hex): 0x{FileData[346].ToString("X2")}\nIndex: 346\nString: X2\n";;
        }

        static void LoadMonsterCoords(byte[] FileData) 
        {
            int monsterStart = BitConverter.ToInt32(FileData, 24);
            int monsterTypePointer = BitConverter.ToInt32(FileData, monsterStart + 8);
            int monsterSpawns = 0;
            while (BitConverter.ToInt32(FileData, monsterTypePointer) > 0) 
            {
                monsterTypePointer += 4;
                monsterSpawns += 1;
            }

            int monsterStatPointer = BitConverter.ToInt32(FileData, monsterStart + 12);
            Console.WriteLine($"{monsterStatPointer}");
            if (monsterSpawns > 0) 
            {
                var MonsterData = new Structs.MonsterSpawn[monsterSpawns];
                brInput.BaseStream.Seek(monsterStatPointer, SeekOrigin.Begin);
                for (int i = 0, loopTo = monsterSpawns - 1; i <= loopTo; i++) 
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
            string objectiveMainHex = ReturnObjectiveHex(FileData, 240);
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
                MainObj = ReturnMonster(FileData, 244);
            }
            else if (objectiveMainType == "Break Part")
            {
                MainObj = BitConverter.ToInt16(FileData, 244).ToString();
            }
            else if (objectiveMainType == "Slay All")
            {
                MainObj = ReturnInterception(FileData);
            }
            else
            {
                MainObj = ReturnItem(FileData, 244);
            }

            ObjectiveMainQuant = BitConverter.ToInt16(FileData, 246);
            if (objectiveMainType == "Damage" || objectiveMainType == "Slay or Damage")
            {
                ObjectiveMainQuant = (int)ObjectiveMainQuant * 100;
            }

            ReturnObjectiveMainHex = $"\nValue: {objectiveMainHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, 240)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[240]} | INDEX+1: {FileData[240 + 1]} | INDEX+2: {FileData[240 + 2]} | INDEX+3: {FileData[240 + 3]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[240], FileData[240 + 1], FileData[240 + 2], FileData[240 + 3] })}\nHex 3: {FileData[240]} {FileData[240 + 1]} {FileData[240 + 2]} {FileData[240 + 3]}\nIndex: 240\n";
            ReturnObjectiveMainType = $"\nValue: {objectiveMainType}\nIndex: N/A\n";
            ReturnObjectiveMainQuant = $"\nValue: {ObjectiveMainQuant} (* 100)\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 246)}\nIndex: 246\n";
            ReturnMainObj = $"\nValue: {MainObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 244).ToString()} / {BitConverter.ToInt16(FileData, 244)}\nBy Index: {FileData[244]}\nBy Index (Hex): 0x{FileData[244].ToString("X2")}\nIndex: 244\n";
        }

        static void LoadSubAObjective(byte[] FileData)
        {
            string objectiveSubAHex = ReturnObjectiveHex(FileData, 248);
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
                SubAObj = ReturnMonster(FileData, 252);
            }
            else if (objectiveSubAType == "Break Part")
            {
                SubAObj = BitConverter.ToInt16(FileData, 252).ToString();
            }
            else if (objectiveSubAType == "Slay All")
            {
                SubAObj = ReturnInterception(FileData);
            }
            else
            {
                SubAObj = ReturnItem(FileData, 252);
            }

            if (SubAObj == "0" | string.IsNullOrEmpty(SubAObj))
            {
                SubAObj = "None";
            }

            ObjectiveSubAQuant = BitConverter.ToInt16(FileData, 254);
            if (objectiveSubAType == "Damage" || objectiveSubAType == "Slay or Damage")
            {
                ObjectiveSubAQuant = (int)ObjectiveSubAQuant * 100;
            }

            ReturnObjectiveSubAHex = $"\nValue: {objectiveSubAHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, 248)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[248]} | INDEX+1: {FileData[248 + 1]} | INDEX+2: {FileData[248 + 2]} | INDEX+3: {FileData[248 + 3]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[248], FileData[248 + 1], FileData[248 + 2], FileData[248 + 3] })}\nHex 3: {FileData[248]} {FileData[248 + 1]} {FileData[248 + 2]} {FileData[248 + 3]}\nIndex: 248\n";
            ReturnObjectiveSubAType = $"\nValue: {objectiveSubAType}\nIndex: N/A\n";
            ReturnObjectiveSubAQuant = $"\nValue: {ObjectiveSubAQuant} (* 100)\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 254)}\nIndex: 254\n";
            ReturnSubAObj = $"\nValue: {SubAObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 252).ToString()} / {BitConverter.ToInt16(FileData, 252)}\nBy Index: {FileData[252]}\nBy Index (Hex): 0x{FileData[252].ToString("X2")}\nIndex: 252\n";
        }

        static void LoadSubBObjective(byte[] FileData)
        {
            string objectiveSubBHex = ReturnObjectiveHex(FileData, 256);
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
                SubBObj = ReturnMonster(FileData, 260);
            }
            else if (objectiveSubBType == "Break Part")
            {
                SubBObj = BitConverter.ToInt16(FileData, 260).ToString();
            }
            else if (objectiveSubBType == "Slay All")
            {
                SubBObj = ReturnInterception(FileData);
            }
            else
            {
                SubBObj = ReturnItem(FileData, 260);
            }

            if (SubBObj == "0" | string.IsNullOrEmpty(SubBObj))
            {
                SubBObj = "None";
            }

            ObjectiveSubBQuant = BitConverter.ToInt16(FileData, 262);
            if (objectiveSubBType == "Damage" || objectiveSubBType == "Slay or Damage")
            {
                ObjectiveSubBQuant = (int)ObjectiveSubBQuant * 100;
            }

            ReturnObjectiveSubBHex = $"\nValue: {objectiveSubBHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, 256)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[256]} | INDEX+1: {FileData[256 + 1]} | INDEX+2: {FileData[256 + 2]} | INDEX+3: {FileData[256 + 3]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[256], FileData[256 + 1], FileData[256 + 2], FileData[256 + 3] })}\nHex 3: {FileData[256]} {FileData[256 + 1]} {FileData[256 + 2]} {FileData[256 + 3]}\nIndex: 256\n";
            ReturnObjectiveSubBType = $"\nValue: {objectiveSubBType}\nIndex: N/A\n";
            ReturnObjectiveSubBQuant = $"\nValue: {ObjectiveSubBQuant} (* 100)\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 262)}\nIndex: 262\n";
            ReturnSubBObj = $"\nValue: {SubBObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, 260).ToString()} / {BitConverter.ToInt16(FileData, 260)}\nBy Index: {FileData[260]}\nBy Index (Hex): 0x{FileData[260].ToString("X2")}\nIndex: 260\n";
        }
    }
}
