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
    class QuestReader
    {
        public static byte[] BaseFile;
        public static BinaryReader brInput;

        public static Objects.QuestInfo.ReturnLocationDict ReturnLocationDict = new Objects.QuestInfo.ReturnLocationDict();
        
        public static Objects.QuestInfo.ReturnRankInfoDict ReturnRankInfoDict = new Objects.QuestInfo.ReturnRankInfoDict();
        public static Objects.QuestInfo.ReturnRankValueDict ReturnRankValueDict = new Objects.QuestInfo.ReturnRankValueDict();
        public static Objects.QuestInfo.ReturnRankBandsDict ReturnRankBandsDict = new Objects.QuestInfo.ReturnRankBandsDict();
        public static Objects.QuestInfo.ReturnRankUnkDict ReturnRankUnkDict = new Objects.QuestInfo.ReturnRankUnkDict();

        public static Objects.QuestInfo.ReturnQuestFeeDict ReturnQuestFeeDict = new Objects.QuestInfo.ReturnQuestFeeDict();
        public static Objects.QuestInfo.ReturnPrimaryRewardDict ReturnPrimaryRewardDict = new Objects.QuestInfo.ReturnPrimaryRewardDict();
        public static Objects.QuestInfo.ReturnRewardADict ReturnRewardADict = new Objects.QuestInfo.ReturnRewardADict();
        public static Objects.QuestInfo.ReturnRewardBDict ReturnRewardBDict = new Objects.QuestInfo.ReturnRewardBDict();

        public static Objects.QuestInfo.ReturnMonsterVariant1ADict ReturnMonsterVariant1ADict = new Objects.QuestInfo.ReturnMonsterVariant1ADict();
        public static Objects.QuestInfo.ReturnMonsterVariant2ADict ReturnMonsterVariant2ADict = new Objects.QuestInfo.ReturnMonsterVariant2ADict();
        public static Objects.QuestInfo.ReturnMonsterVariant1BDict ReturnMonsterVariant1BDict = new Objects.QuestInfo.ReturnMonsterVariant1BDict();
        public static Objects.QuestInfo.ReturnMonsterVariant2BDict ReturnMonsterVariant2BDict = new Objects.QuestInfo.ReturnMonsterVariant2BDict();

        public static Objects.QuestInfo.ReturnDeliverStringDict ReturnDeliverStringDict = new Objects.QuestInfo.ReturnDeliverStringDict();
        public static Objects.QuestInfo.ReturnQuestTypeNameDict ReturnQuestTypeNameDict = new Objects.QuestInfo.ReturnQuestTypeNameDict();
        public static Objects.QuestInfo.ReturnObjMainStringDict ReturnObjMainStringDict = new Objects.QuestInfo.ReturnObjMainStringDict();
        public static Objects.QuestInfo.ReturnObjAStringDict ReturnObjAStringDict = new Objects.QuestInfo.ReturnObjAStringDict();
        public static Objects.QuestInfo.ReturnObjBStringDict ReturnObjBStringDict = new Objects.QuestInfo.ReturnObjBStringDict();
        public static Objects.QuestInfo.ReturnClearReqStringDict ReturnClearReqStringDict = new Objects.QuestInfo.ReturnClearReqStringDict();
        public static Objects.QuestInfo.ReturnFailReqStringDict ReturnFailReqStringDict = new Objects.QuestInfo.ReturnFailReqStringDict();
        public static Objects.QuestInfo.ReturnHirerStringDict ReturnHirerStringDict = new Objects.QuestInfo.ReturnHirerStringDict();
        public static Objects.QuestInfo.ReturnDescriptionStringDict ReturnDescriptionStringDict = new Objects.QuestInfo.ReturnDescriptionStringDict();

        public static Objects.QuestInfo.ReturnObjectiveMainDict ReturnObjectiveMainDict = new Objects.QuestInfo.ReturnObjectiveMainDict();
        public static Objects.QuestInfo.ReturnObjectiveSubADict ReturnObjectiveSubADict = new Objects.QuestInfo.ReturnObjectiveSubADict();
        public static Objects.QuestInfo.ReturnObjectiveSubBDict ReturnObjectiveSubBDict = new Objects.QuestInfo.ReturnObjectiveSubBDict();

        public static string ReturnLocationInfo;

        public static string ReturnRankInfo;
        public static string ReturnRankValue;
        public static string ReturnRankBands;
        public static string ReturnRankUnk;

        public static string ReturnQuestFee;
        public static string ReturnPrimaryReward;
        public static string ReturnRewardA;
        public static string ReturnRewardB;

        public static string ReturnMonsterVariant1AInfo;
        public static string ReturnMonsterVariant2AInfo;
        public static string ReturnMonsterVariant1BInfo;
        public static string ReturnMonsterVariant2BInfo;

        public static string ReturnObjectiveMainHex;
        public static string ReturnObjectiveMainType;
        public static string ReturnObjectiveMainQuant;
        public static string ReturnMainObj;

        public static string ReturnObjectiveSubAHex;
        public static string ReturnObjectiveSubAType;
        public static string ReturnObjectiveSubAQuant;
        public static string ReturnSubAObj;

        public static string ReturnObjectiveSubBHex;
        public static string ReturnObjectiveSubBType;
        public static string ReturnObjectiveSubBQuant;
        public static string ReturnSubBObj;

        public static string[] ReturnDeliverString;
        public static string[] ReturnQuestTypeName;
        public static string[] ReturnObjMainString;
        public static string[] ReturnObjAString;
        public static string[] ReturnObjBString;
        public static string[] ReturnClearReqString;
        public static string[] ReturnFailReqString;
        public static string[] ReturnHirerString;
        public static string[] ReturnDescriptionString;

        public static int QuestStringsStartIndexA = 48;
        public static int QuestStringsStartIndexB = 232;
        public static int QuestStringsStartPointer = 4;

        public static int QuestFeeIndex = 204;
        public static int PrimaryRewardIndex = 208;
        public static int RewardAIndex = 216;
        public static int RewardBIndex = 220;

        public static int LocationIndex = 228;
        
        public static int RankIndex = 72;

        public static int Variant1AIndex = 337;
        public static int Variant2AIndex = 338;
        public static int Variant1BIndex = 345;
        public static int Variant2BIndex = 346;

        public static int ObjectiveHexIncIndexA = 1;
        public static int ObjectiveHexIncIndexB = 2;
        public static int ObjectiveHexIncIndexC = 3;

        public static int InterceptionLoopIndexA = 377;
        public static int InterceptionLoopIndexB = 382;
        public static int InterceptionLoopIndexSub = 1;

        public static int MonsterCoordStartIndex = 24;
        public static int MonsterCoordTypePointer = 8;
        public static int MonsterCoordTypePointerIncIndex = 4;
        public static int MonsterCoordSpawnIncIndex = 1;
        public static int MonsterCoordStatPointer = 12;
        public static int MonsterCoordCheckIndex = 0;
        public static int MonsterCoordLoopIndexSub = 1;

        public static int MainObjHexIndex = 240;
        public static int MainObjIndex = 244;
        public static int MainObjQuantIndex = 246;
        public static int MainObjQuantMult = 100;

        public static int SubAObjHexIndex = 248;
        public static int SubAObjIndex = 252;
        public static int SubAObjQuantIndex = 254;
        public static int SubAObjQuantMult = 100;

        public static int SubBObjHexIndex = 256;
        public static int SubBObjIndex = 260;
        public static int SubBObjQuantIndex = 262;
        public static int SubBObjQuantMult = 100;

        public static string[] ReadNullTerminatedStringAsArray(BinaryReader brInput, Encoding encoding)
        {
            var charByteList = new List<byte>();
            string str = "";
            
            if (brInput.BaseStream.Position == brInput.BaseStream.Length)
            {
                var charByteArray = charByteList.ToArray();
                str = encoding.GetString(charByteArray);
                return new string[] { str, str.Replace("\n", "<NLINE>"), StringUtils.ReturnByteArrayString(charByteArray), StringUtils.ReturnByteArrayHexString(charByteArray) };
            }

            byte b = brInput.ReadByte();

            while (b != 0x0 && brInput.BaseStream.Position != brInput.BaseStream.Length)
            {
                charByteList.Add(b);
                b = brInput.ReadByte();
            }

            var char_bytes = charByteList.ToArray();
            str = encoding.GetString(char_bytes);

            StringUtils.WriteLine("\n===== NULL TERMINATED STRING ====={0}",
                $"\nValue: {str.Replace("\n", "<NLINE>")}\n\nBytes: {StringUtils.ReturnByteArrayString(char_bytes)}\n\nHex: {StringUtils.ReturnByteArrayHexString(char_bytes)}\n");

            return new string[] { str, str.Replace("\n", "<NLINE>"), StringUtils.ReturnByteArrayString(char_bytes), StringUtils.ReturnByteArrayHexString(char_bytes) };
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


            StringUtils.WriteLine("\n===== NULL TERMINATED STRING ====={0}",
                $"\nValue: {str.Replace("\n", "<NLINE>")}\n\nBytes: {StringUtils.ReturnByteArrayString(char_bytes)}\n\nHex: {StringUtils.ReturnByteArrayHexString(char_bytes)}\n");

            return str;
        }

        public static void InitQuestDataLoaders()
        {
            BaseFile = FileLoader.BaseFile;
            brInput = FileLoader.brInput;

            Items.initiate();
            LoadRank(BaseFile);
            LoadRewardInfo(BaseFile);
            LoadLocations(BaseFile);
            LoadMonsterVariant(BaseFile);
            LoadMainObjective(BaseFile);
            LoadSubAObjective(BaseFile);
            LoadSubBObjective(BaseFile);
            LoadQuestTextStrings(BaseFile);
        }

        public static void WriteQuestLogFile(string input)
        {
            BaseFile = FileLoader.BaseFile;
            brInput = FileLoader.brInput;

            Items.initiate();
            StringUtils.WriteLine($"{Path.GetFileName(input)}");
            StringUtils.WriteLine("======================\n===== QUEST DATA =====\n======================\n\n");

            StringUtils.WriteLine("===============================\n========== RANK DATA ==========\n===============================");
            LoadRank(BaseFile);
            StringUtils.WriteLine("\n\n===== RANK INFO: ====={0}\n===== RANK VALUE: ====={1}\n===== RANK BANDS: ====={2}\n==== RANK UNK: ====={3}\n", 
                ReturnRankInfo, ReturnRankValue, ReturnRankBands, ReturnRankUnk);
            
            LoadRewardInfo(BaseFile);
            StringUtils.WriteLine("===== QUEST FEE: ====={0}\n===== PRIMARY REWARD: ====={1}\n===== REWARD A ====={2}\n===== REWARD B ====={3}\n",
                ReturnQuestFee, ReturnPrimaryReward, ReturnRewardA, ReturnRewardB);

            StringUtils.WriteLine("===================================\n========== LOCATION DATA ==========\n===================================");
            LoadLocations(BaseFile);
            StringUtils.WriteLine("\n\n===== LOCATION: ====={0}\n", 
                ReturnLocationInfo);

            StringUtils.WriteLine("==========================================\n========== MONSTER VARIANT DATA ==========\n==========================================");
            LoadMonsterVariant(BaseFile);
            StringUtils.WriteLine("\n\n===== MONSTER VARIANT 1A: ====={0}\n===== MONSTER VARIANT 1B: ====={1}\n===== MONSTER VARIANT 2A: ====={2}\n===== MONSTER VARIANT 2B: ====={3}\n", 
                ReturnMonsterVariant1AInfo, ReturnMonsterVariant2AInfo, ReturnMonsterVariant1BInfo, ReturnMonsterVariant2BInfo);

            StringUtils.WriteLine("===================================\n========== MAIN OBJ DATA ==========\n===================================");
            LoadMainObjective(BaseFile);
            StringUtils.WriteLine("\n\n===== MAIN OBJECTIVE HEX: ====={0}\n===== MAIN OBJECTIVE TYPE: ====={1}\n===== MAIN OBJECTIVE QUANT: ====={2}\n===== MAIN OBJECTIVE: ====={3}\n",
                ReturnObjectiveMainHex, ReturnObjectiveMainType, ReturnObjectiveMainQuant, ReturnMainObj);

            StringUtils.WriteLine("====================================\n========== SUB A OBJ DATA ==========\n====================================");
            LoadSubAObjective(BaseFile);
            StringUtils.WriteLine("\n\n===== SUB A OBJECTIVE HEX: ====={0}\n===== SUB A OBJECTIVE TYPE: ====={1}\n===== SUB A OBJECTIVE QUANT: ====={2}\n===== SUB A OBJECTIVE: ====={3}\n",
                ReturnObjectiveSubAHex, ReturnObjectiveSubAType, ReturnObjectiveSubAQuant, ReturnSubAObj); 

            StringUtils.WriteLine("====================================\n========== SUB B OBJ DATA ==========\n====================================");
            LoadSubBObjective(BaseFile);
            StringUtils.WriteLine("\n\n===== SUB B OBJECTIVE HEX: ====={0}\n===== SUB B OBJECTIVE TYPE: ====={1}\n===== SUB B OBJECTIVE QUANT: ====={2}\n===== SUB B OBJECTIVE: ====={3}\n",
                ReturnObjectiveSubBHex, ReturnObjectiveSubBType, ReturnObjectiveSubBQuant, ReturnSubBObj);

            StringUtils.WriteLine("========================================\n========== QUEST TEXT STRINGS ==========\n========================================\n");
            LoadQuestTextStrings(BaseFile);
        }

        public static void LoadQuestTextStrings(byte[] FileData) {
            int QuestStringsStart = BitConverter.ToInt32(FileData, QuestStringsStartIndexA);
            int ReadPointer = BitConverter.ToInt32(FileData, QuestStringsStart);
            brInput.BaseStream.Seek(ReadPointer, SeekOrigin.Begin);

            ReturnDeliverString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnDeliverStringDict.QuestStringsStart = QuestStringsStart;
            ReturnDeliverStringDict.ReadPointer = ReadPointer;
            ReturnDeliverStringDict.brInputSeek = brInput.BaseStream.Seek(ReadPointer, SeekOrigin.Begin);
            ReturnDeliverStringDict.ValueA = ReturnDeliverString[0];
            ReturnDeliverStringDict.ValueB = ReturnDeliverString[1];
            ReturnDeliverStringDict.Bytes = ReturnDeliverString[2];
            ReturnDeliverStringDict.Hex = ReturnDeliverString[3];

            StringUtils.WriteLine("===== DELIVER STRING =====\n{0}",
                $"Value: {ReturnDeliverString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart = BitConverter.ToInt32(FileData, QuestStringsStartIndexB);
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnQuestTypeName = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnQuestTypeNameDict.QuestStringsStart = QuestStringsStart;
            ReturnQuestTypeNameDict.ReadPointer = ReadPointer;
            ReturnQuestTypeNameDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnQuestTypeNameDict.ValueA = ReturnQuestTypeName[0];
            ReturnQuestTypeNameDict.ValueB = ReturnQuestTypeName[1];
            ReturnQuestTypeNameDict.Bytes = ReturnQuestTypeName[2];
            ReturnQuestTypeNameDict.Hex = ReturnQuestTypeName[3];

            StringUtils.WriteLine("===== QUEST TYPE NAME =====\n{0}",
                $"Value: {ReturnQuestTypeName[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);

            ReturnObjMainString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnObjMainStringDict.QuestStringsStart = QuestStringsStart;
            ReturnObjMainStringDict.ReadPointer = ReadPointer;
            ReturnObjMainStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnObjMainStringDict.ValueA = ReturnObjMainString[0];
            ReturnObjMainStringDict.ValueB = ReturnObjMainString[1];
            ReturnObjMainStringDict.Bytes = ReturnObjMainString[2];
            ReturnObjMainStringDict.Hex = ReturnObjMainString[3];

            StringUtils.WriteLine("===== OBJ MAIN STRING =====\n{0}",
                $"Value: {ReturnObjMainString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjAString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnObjAStringDict.QuestStringsStart = QuestStringsStart;
            ReturnObjAStringDict.ReadPointer = ReadPointer;
            ReturnObjAStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnObjAStringDict.ValueA = ReturnObjAString[0];
            ReturnObjAStringDict.ValueB = ReturnObjAString[1];
            ReturnObjAStringDict.Bytes = ReturnObjAString[2];
            ReturnObjAStringDict.Hex = ReturnObjAString[3];

            StringUtils.WriteLine("===== OBJ A STRING =====\n{0}",
                $"Value: {ReturnObjAString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnObjBString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnObjBStringDict.QuestStringsStart = QuestStringsStart;
            ReturnObjBStringDict.ReadPointer = ReadPointer;
            ReturnObjBStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnObjBStringDict.ValueA = ReturnObjBString[0];
            ReturnObjBStringDict.ValueB = ReturnObjBString[1];
            ReturnObjBStringDict.Bytes = ReturnObjBString[2];
            ReturnObjBStringDict.Hex = ReturnObjBString[3];

            StringUtils.WriteLine("===== OBJ B STRING =====\n{0}",
                $"Value: {ReturnObjBString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnClearReqString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnClearReqStringDict.QuestStringsStart = QuestStringsStart;
            ReturnClearReqStringDict.ReadPointer = ReadPointer;
            ReturnClearReqStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnClearReqStringDict.ValueA = ReturnClearReqString[0];
            ReturnClearReqStringDict.ValueB = ReturnClearReqString[1];
            ReturnClearReqStringDict.Bytes = ReturnClearReqString[2];
            ReturnClearReqStringDict.Hex = ReturnClearReqString[3];

            StringUtils.WriteLine("===== CLEAR REQ STRING =====\n{0}",
                $"Value: {ReturnClearReqString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnFailReqString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnFailReqStringDict.QuestStringsStart = QuestStringsStart;
            ReturnFailReqStringDict.ReadPointer = ReadPointer;
            ReturnFailReqStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnFailReqStringDict.ValueA = ReturnFailReqString[0];
            ReturnFailReqStringDict.ValueB = ReturnFailReqString[1];
            ReturnFailReqStringDict.Bytes = ReturnFailReqString[2];
            ReturnFailReqStringDict.Hex = ReturnFailReqString[3];

            StringUtils.WriteLine("===== FAIL REQ STRING =====\n{0}",
                $"Value: {ReturnFailReqString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnHirerString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));

            ReturnHirerStringDict.QuestStringsStart = QuestStringsStart;
            ReturnHirerStringDict.ReadPointer = ReadPointer;
            ReturnHirerStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnHirerStringDict.ValueA = ReturnHirerString[0];
            ReturnHirerStringDict.ValueB = ReturnHirerString[1];
            ReturnHirerStringDict.Bytes = ReturnHirerString[2];
            ReturnHirerStringDict.Hex = ReturnHirerString[3];

            StringUtils.WriteLine("===== HIRER STRING =====\n{0}",
                $"Value: {ReturnHirerString[0].Replace("\n", "<NLINE>")}\n\n\n");



            QuestStringsStart += QuestStringsStartPointer;
            brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            
            ReturnDescriptionString = ReadNullTerminatedStringAsArray(brInput, Encoding.GetEncoding("Shift-JIS"));
            
            ReturnDescriptionStringDict.QuestStringsStart = QuestStringsStart;
            ReturnDescriptionStringDict.ReadPointer = ReadPointer;
            ReturnDescriptionStringDict.brInputSeek = brInput.BaseStream.Seek(BitConverter.ToInt32(FileData, QuestStringsStart), SeekOrigin.Begin);
            ReturnDescriptionStringDict.ValueA = ReturnDescriptionString[0];
            ReturnDescriptionStringDict.ValueB = ReturnDescriptionString[1];
            ReturnDescriptionStringDict.Bytes = ReturnDescriptionString[2];
            ReturnDescriptionStringDict.Hex = ReturnDescriptionString[3];

            StringUtils.WriteLine("===== DESCRIPTION STRING =====\n{0}",
                $"Value: {ReturnDescriptionString[0].Replace("\n", "<NLINE>")}\n\n\n");
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
            
            StringUtils.WriteLine("===== ITEM INFO: ====={0}\n", 
                $"\nValue: {item}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, index)}\nBy Index: {FileData[index]}\nBy Index (Hex): 0x{FileData[index].ToString("X2")}\nIndex: {index}\n");
            
            return item;
        }

        public static string ReturnObjectiveHex(byte[] FileData, int index)
        {
            StringUtils.WriteLine("\n\n===== OBJECTIVE HEX: ====={0}",
                $"\nValue: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] }).Replace("-", "")}\nIndexes 1: INDEX: {FileData[index]} | INDEX+1: {FileData[index + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[index + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[index + ObjectiveHexIncIndexC]}\nHex 1: {BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] })}\nHex 2: {FileData[index]} {FileData[index + ObjectiveHexIncIndexA]} {FileData[index + ObjectiveHexIncIndexB]} {FileData[index + ObjectiveHexIncIndexC]}\nIndex: {index}\n");

            return BitConverter.ToString(new byte[] { FileData[index], FileData[index + ObjectiveHexIncIndexA], FileData[index + ObjectiveHexIncIndexB], FileData[index + ObjectiveHexIncIndexC] }).Replace("-", "");
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
            
            StringUtils.WriteLine("===== MONSTER INFO A: ====={0}\n", 
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

            StringUtils.WriteLine("===== MONSTER INFO B: ====={0}\n", 
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
                    StringUtils.WriteLine("\n\n===== INTERCEPTION DATA SECT 1 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                }
                else
                {                    
                    StringUtils.WriteLine("\n\n===== INTERCEPTION DATA SECT 2 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nSingle {BitConverter.ToSingle(FileData, i).ToString()} / {BitConverter.ToSingle(FileData, i)}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                        
                    monsterAdd = BitConverter.ToSingle(FileData, i).ToString();
                }

                if (i == InterceptionLoopIndexA)
                {                    
                    StringUtils.WriteLine("\n\n===== INTERCEPTION DATA SECT 3 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");
                    
                    monster += monsterAdd;
                }
                else
                {
                    StringUtils.WriteLine("\n\n===== INTERCEPTION DATA SECT 4 ====={0}",
                        $"\nValue: {monster}\nAdd: {monsterAdd}\nBy Index: {FileData[i]}\nBy Index (Hex): 0x{FileData[i].ToString("X2")}\nIndex: {i}\n");

                    monster += $", {monsterAdd}";
                }
            }

            StringUtils.WriteLine("\n\n===== INTERCEPTION SECT 5 ====={0}",
                $"\nValue: {monster}\nAdd: {monsterAdd}\n");

            return monster;
        }

        public static void LoadRewardInfo(byte[] FileData)
        {
            int QuestFee = BitConverter.ToInt32(FileData, QuestFeeIndex);
            int PrimaryReward = BitConverter.ToInt32(FileData, PrimaryRewardIndex);
            int RewardA = BitConverter.ToInt32(FileData, RewardAIndex);
            int RewardB = BitConverter.ToInt32(FileData, RewardBIndex);

            ReturnQuestFeeDict.Value = QuestFee;
            ReturnQuestFeeDict.Int32LE = BitConverter.ToInt32(FileData, QuestFeeIndex);
            ReturnQuestFeeDict.ByIndex = FileData[QuestFeeIndex];
            ReturnQuestFeeDict.ByIndexHex = FileData[QuestFeeIndex].ToString("X2");
            ReturnQuestFeeDict.Index = QuestFeeIndex;

            ReturnPrimaryRewardDict.Value = PrimaryReward;
            ReturnPrimaryRewardDict.Int32LE = BitConverter.ToInt32(FileData, PrimaryRewardIndex);
            ReturnPrimaryRewardDict.ByIndex = FileData[PrimaryRewardIndex];
            ReturnPrimaryRewardDict.ByIndexHex = FileData[PrimaryRewardIndex].ToString("X2");
            ReturnPrimaryRewardDict.Index = PrimaryRewardIndex;

            ReturnRewardADict.Value = RewardA;
            ReturnRewardADict.Int32LE = BitConverter.ToInt32(FileData, RewardAIndex);
            ReturnRewardADict.ByIndex = FileData[RewardAIndex];
            ReturnRewardADict.ByIndexHex = FileData[RewardAIndex].ToString("X2");
            ReturnRewardADict.Index = RewardAIndex;

            ReturnRewardBDict.Value = RewardB;
            ReturnRewardBDict.Int32LE = BitConverter.ToInt32(FileData, RewardBIndex);
            ReturnRewardBDict.ByIndex = FileData[RewardBIndex];
            ReturnRewardBDict.ByIndexHex = FileData[RewardBIndex].ToString("X2");
            ReturnRewardBDict.Index = RewardBIndex;


            ReturnQuestFee = $"\nValue: {QuestFee}\nInt32 (LE): {BitConverter.ToInt32(FileData, QuestFeeIndex)}\nBy Index: {FileData[QuestFeeIndex]}\nBy Index (Hex): 0x{FileData[QuestFeeIndex].ToString("X2")}\nIndex: {QuestFeeIndex}\n";
            ReturnPrimaryReward = $"\nValue: {PrimaryReward}\nInt32 (LE): {BitConverter.ToInt32(FileData, PrimaryRewardIndex)}\nBy Index: {FileData[PrimaryRewardIndex]}\nBy Index (Hex): 0x{FileData[PrimaryRewardIndex].ToString("X2")}\nIndex: {PrimaryRewardIndex}\n";
            ReturnRewardA = $"\nValue: {RewardA}\nInt32 (LE): {BitConverter.ToInt32(FileData, RewardAIndex)}\nBy Index: {FileData[RewardAIndex]}\nBy Index (Hex): 0x{FileData[RewardAIndex].ToString("X2")}\nIndex: {RewardAIndex}\n";
            ReturnRewardB = $"\nValue: {RewardB}\nInt32 (LE): {BitConverter.ToInt32(FileData, RewardBIndex)}\nBy Index: {FileData[RewardBIndex]}\nBy Index (Hex): 0x{FileData[RewardBIndex].ToString("X2")}\nIndex: {RewardBIndex}\n";
        }

        public static void LoadLocations(byte[] FileData)
        {
            string location = null;
            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(FileData, LocationIndex), out location))
            {
                //
            }

            ReturnLocationDict.Value = location;
            ReturnLocationDict.Int32LE = BitConverter.ToInt32(FileData, LocationIndex);
            ReturnLocationDict.ByIndex = FileData[LocationIndex];
            ReturnLocationDict.ByIndexHex = FileData[LocationIndex].ToString("X2");
            ReturnLocationDict.Index = LocationIndex;
            

            ReturnLocationInfo = $"\nValue: {location}\nInt32 (LE): {BitConverter.ToInt32(FileData, LocationIndex)}\nBy Index: {FileData[LocationIndex]}\nBy Index (Hex): 0x{FileData[LocationIndex].ToString("X2")}\nIndex: {LocationIndex}\n";
        }

        public static void LoadRank(byte[] FileData) 
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

            ReturnRankInfoDict.Value = StatTable;
            ReturnRankInfoDict.Int32LE = BitConverter.ToInt32(FileData, RankIndex);
            ReturnRankInfoDict.ByIndex = FileData[RankIndex];
            ReturnRankInfoDict.ByIndexHex = FileData[RankIndex].ToString("X2");
            ReturnRankInfoDict.Index = RankIndex;

            ReturnRankValueDict.Value = RankValue;
            ReturnRankValueDict.Int32LE = BitConverter.ToInt32(FileData, RankIndex);
            ReturnRankValueDict.ByIndex = FileData[RankIndex];
            ReturnRankValueDict.ByIndexHex = FileData[RankIndex].ToString("X2");
            ReturnRankValueDict.Index = RankIndex;

            ReturnRankBandsDict.Value = RankBands;
            ReturnRankBandsDict.Int32LE = BitConverter.ToInt32(FileData, RankIndex);
            ReturnRankBandsDict.ByIndex = FileData[RankIndex];
            ReturnRankBandsDict.ByIndexHex = FileData[RankIndex].ToString("X2");
            ReturnRankBandsDict.Index = RankIndex;

            ReturnRankUnkDict.Value = RankUnk;
            ReturnRankUnkDict.Int32LE = BitConverter.ToInt32(FileData, RankIndex);
            ReturnRankUnkDict.ByIndex = FileData[RankIndex];
            ReturnRankUnkDict.ByIndexHex = FileData[RankIndex].ToString("X2");
            ReturnRankUnkDict.Index = RankIndex;


            ReturnRankInfo = $"\nValue: {StatTable}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankValue = $"\nValue: {RankValue}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankBands = $"\nValue: {RankBands}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
            ReturnRankUnk = $"\nValue: {RankUnk}\nInt32 (LE): {BitConverter.ToInt32(FileData, RankIndex)}\nBy Index: {FileData[RankIndex]}\nBy Index (Hex): 0x{FileData[RankIndex].ToString("X2")}\nIndex: {RankIndex}\n";
        }

        public static void LoadMonsterVariant(byte[] FileData) 
        {
            string Variant1A = FileData[Variant1AIndex].ToString("X2");
            string Variant2A = FileData[Variant2AIndex].ToString("X2");
            string Variant1B = FileData[Variant1BIndex].ToString("X2");
            string Variant2B = FileData[Variant2BIndex].ToString("X2");

            ReturnMonsterVariant1ADict.Value = Variant1A;
            ReturnMonsterVariant1ADict.Int32LE = BitConverter.ToInt32(FileData, Variant1AIndex);
            ReturnMonsterVariant1ADict.ByIndex = FileData[Variant1AIndex];
            ReturnMonsterVariant1ADict.ByIndexHex = FileData[Variant1AIndex].ToString("X2");
            ReturnMonsterVariant1ADict.Index = Variant1AIndex;

            ReturnMonsterVariant2ADict.Value = Variant2A;
            ReturnMonsterVariant2ADict.Int32LE = BitConverter.ToInt32(FileData, Variant2AIndex);
            ReturnMonsterVariant2ADict.ByIndex = FileData[Variant2AIndex];
            ReturnMonsterVariant2ADict.ByIndexHex = FileData[Variant2AIndex].ToString("X2");
            ReturnMonsterVariant2ADict.Index = Variant2AIndex;

            ReturnMonsterVariant1BDict.Value = Variant1B;
            ReturnMonsterVariant1BDict.Int32LE = BitConverter.ToInt32(FileData, Variant1BIndex);
            ReturnMonsterVariant1BDict.ByIndex = FileData[Variant1BIndex];
            ReturnMonsterVariant1BDict.ByIndexHex = FileData[Variant1BIndex].ToString("X2");
            ReturnMonsterVariant1BDict.Index = Variant1BIndex;

            ReturnMonsterVariant2BDict.Value = Variant2B;
            ReturnMonsterVariant2BDict.Int32LE = BitConverter.ToInt32(FileData, Variant2BIndex);
            ReturnMonsterVariant2BDict.ByIndex = FileData[Variant2BIndex];
            ReturnMonsterVariant2BDict.ByIndexHex = FileData[Variant2BIndex].ToString("X2");
            ReturnMonsterVariant2BDict.Index = Variant2BIndex;


            ReturnMonsterVariant1AInfo = $"\nValue: {Variant1A}\nData: {FileData[Variant1AIndex]}\nBy Index: {FileData[Variant1AIndex]}\nBy Index (Hex): 0x{FileData[Variant1AIndex].ToString("X2")}\nIndex: {Variant1AIndex}\nString: X2\n";
            ReturnMonsterVariant2AInfo = $"\nValue: {Variant2A}\nData: {FileData[Variant2AIndex]}\nBy Index: {FileData[Variant2AIndex]}\nBy Index (Hex): 0x{FileData[Variant2AIndex].ToString("X2")}\nIndex: {Variant2AIndex}\nString: X2\n";;
            ReturnMonsterVariant1BInfo = $"\nValue: {Variant1B}\nData: {FileData[Variant1BIndex]}\nBy Index: {FileData[Variant1BIndex]}\nBy Index (Hex): 0x{FileData[Variant1BIndex].ToString("X2")}\nIndex: {Variant1BIndex}\nString: X2\n";;
            ReturnMonsterVariant2BInfo = $"\nValue: {Variant2B}\nData: {FileData[Variant2BIndex]}\nBy Index: {FileData[Variant2BIndex]}\nBy Index (Hex): 0x{FileData[Variant2BIndex].ToString("X2")}\nIndex: {Variant2BIndex}\nString: X2\n";;
        }

        public static void LoadMonsterCoords(byte[] FileData) 
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

        public static void LoadMainObjective(byte[] FileData)
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

            ReturnObjectiveMainDict.ObjHexValue = objectiveMainHex;
            ReturnObjectiveMainDict.ObjHexHexA = ReturnObjectiveHex(FileData, MainObjHexIndex);
            ReturnObjectiveMainDict.ObjHexHexB = $"{BitConverter.ToString(new byte[] { FileData[MainObjHexIndex], FileData[MainObjHexIndex + ObjectiveHexIncIndexA], FileData[MainObjHexIndex + ObjectiveHexIncIndexB], FileData[MainObjHexIndex + ObjectiveHexIncIndexC] })}";
            ReturnObjectiveMainDict.ObjHexHexC = $"{FileData[MainObjHexIndex]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexA]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexB]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexC]}";
            ReturnObjectiveMainDict.ObjHexByIndex = FileData[MainObjHexIndex];
            ReturnObjectiveMainDict.ObjHexByIndexHex = FileData[MainObjHexIndex].ToString("X2");
            ReturnObjectiveMainDict.ObjHexIndex = MainObjHexIndex;
            ReturnObjectiveMainDict.ObjTypeValue = objectiveMainType;
            ReturnObjectiveMainDict.ObjQuantValue = ObjectiveMainQuant;
            ReturnObjectiveMainDict.ObjQuantMult = MainObjQuantMult;
            ReturnObjectiveMainDict.ObjQuantInt16LE = BitConverter.ToInt16(FileData, MainObjQuantIndex);
            ReturnObjectiveMainDict.ObjQuantIndex = MainObjQuantIndex;
            ReturnObjectiveMainDict.ObjValue = MainObj;
            ReturnObjectiveMainDict.ObjInt16LEA = BitConverter.ToInt16(FileData, MainObjIndex).ToString();
            ReturnObjectiveMainDict.ObjInt16LEB = BitConverter.ToInt16(FileData, MainObjIndex);
            ReturnObjectiveMainDict.ObjByIndex = FileData[MainObjIndex];
            ReturnObjectiveMainDict.ObjByIndexHex = FileData[MainObjIndex].ToString("X2");
            ReturnObjectiveMainDict.ObjIndex = MainObjIndex;
            
            ReturnObjectiveMainHex = $"\nValue: {objectiveMainHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, MainObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[MainObjHexIndex]} | INDEX+1: {FileData[MainObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[MainObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[MainObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[MainObjHexIndex], FileData[MainObjHexIndex + ObjectiveHexIncIndexA], FileData[MainObjHexIndex + ObjectiveHexIncIndexB], FileData[MainObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[MainObjHexIndex]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexA]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexB]} {FileData[MainObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {MainObjHexIndex}\n";
            ReturnObjectiveMainType = $"\nValue: {objectiveMainType}\nIndex: N/A\n";
            ReturnObjectiveMainQuant = $"\nValue: {ObjectiveMainQuant} (* {MainObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, MainObjQuantIndex)}\nIndex: {MainObjQuantIndex}\n";
            ReturnMainObj = $"\nValue: {MainObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, MainObjIndex).ToString()} / {BitConverter.ToInt16(FileData, MainObjIndex)}\nBy Index: {FileData[MainObjIndex]}\nBy Index (Hex): 0x{FileData[MainObjIndex].ToString("X2")}\nIndex: {MainObjIndex}\n";
        }

        public static void LoadSubAObjective(byte[] FileData)
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

            ReturnObjectiveSubADict.ObjHexValue = objectiveSubAHex;
            ReturnObjectiveSubADict.ObjHexHexA = ReturnObjectiveHex(FileData, SubAObjHexIndex);
            ReturnObjectiveSubADict.ObjHexHexB = $"{BitConverter.ToString(new byte[] { FileData[SubAObjHexIndex], FileData[SubAObjHexIndex + ObjectiveHexIncIndexA], FileData[SubAObjHexIndex + ObjectiveHexIncIndexB], FileData[SubAObjHexIndex + ObjectiveHexIncIndexC] })}";
            ReturnObjectiveSubADict.ObjHexHexC = $"{FileData[SubAObjHexIndex]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexC]}";
            ReturnObjectiveSubADict.ObjHexByIndex = FileData[SubAObjHexIndex];
            ReturnObjectiveSubADict.ObjHexByIndexHex = FileData[SubAObjHexIndex].ToString("X2");
            ReturnObjectiveSubADict.ObjHexIndex = SubAObjHexIndex;
            ReturnObjectiveSubADict.ObjTypeValue = objectiveSubAType;
            ReturnObjectiveSubADict.ObjQuantValue = ObjectiveSubAQuant;
            ReturnObjectiveSubADict.ObjQuantMult = SubAObjQuantMult;
            ReturnObjectiveSubADict.ObjQuantInt16LE = BitConverter.ToInt16(FileData, SubAObjQuantIndex);
            ReturnObjectiveSubADict.ObjQuantIndex = SubAObjQuantIndex;
            ReturnObjectiveSubADict.ObjValue = SubAObj;
            ReturnObjectiveSubADict.ObjInt16LEA = BitConverter.ToInt16(FileData, SubAObjIndex).ToString();
            ReturnObjectiveSubADict.ObjInt16LEB = BitConverter.ToInt16(FileData, SubAObjIndex);
            ReturnObjectiveSubADict.ObjByIndex = FileData[SubAObjIndex];
            ReturnObjectiveSubADict.ObjByIndexHex = FileData[SubAObjIndex].ToString("X2");
            ReturnObjectiveSubADict.ObjIndex = SubAObjIndex;

            ReturnObjectiveSubAHex = $"\nValue: {objectiveSubAHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, SubAObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[SubAObjHexIndex]} | INDEX+1: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[SubAObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[SubAObjHexIndex], FileData[SubAObjHexIndex + ObjectiveHexIncIndexA], FileData[SubAObjHexIndex + ObjectiveHexIncIndexB], FileData[SubAObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[SubAObjHexIndex]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubAObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {SubAObjHexIndex}\n";
            ReturnObjectiveSubAType = $"\nValue: {objectiveSubAType}\nIndex: N/A\n";
            ReturnObjectiveSubAQuant = $"\nValue: {ObjectiveSubAQuant} (* {SubAObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubAObjQuantIndex)}\nIndex: {SubAObjQuantIndex}\n";
            ReturnSubAObj = $"\nValue: {SubAObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubAObjIndex).ToString()} / {BitConverter.ToInt16(FileData, SubAObjIndex)}\nBy Index: {FileData[SubAObjIndex]}\nBy Index (Hex): 0x{FileData[SubAObjIndex].ToString("X2")}\nIndex: {SubAObjIndex}\n";
        }

        public static void LoadSubBObjective(byte[] FileData)
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

            ReturnObjectiveSubBDict.ObjHexValue = objectiveSubBHex;
            ReturnObjectiveSubBDict.ObjHexHexA = ReturnObjectiveHex(FileData, SubBObjHexIndex);
            ReturnObjectiveSubBDict.ObjHexHexB = $"{BitConverter.ToString(new byte[] { FileData[SubBObjHexIndex], FileData[SubBObjHexIndex + ObjectiveHexIncIndexA], FileData[SubBObjHexIndex + ObjectiveHexIncIndexB], FileData[SubBObjHexIndex + ObjectiveHexIncIndexC] })}";
            ReturnObjectiveSubBDict.ObjHexHexC = $"{FileData[SubBObjHexIndex]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexC]}";
            ReturnObjectiveSubBDict.ObjHexByIndex = FileData[SubBObjHexIndex];
            ReturnObjectiveSubBDict.ObjHexByIndexHex = FileData[SubBObjHexIndex].ToString("X2");
            ReturnObjectiveSubBDict.ObjHexIndex = SubBObjHexIndex;
            ReturnObjectiveSubBDict.ObjTypeValue = objectiveSubBType;
            ReturnObjectiveSubBDict.ObjQuantValue = ObjectiveSubBQuant;
            ReturnObjectiveSubBDict.ObjQuantMult = SubBObjQuantMult;
            ReturnObjectiveSubBDict.ObjQuantInt16LE = BitConverter.ToInt16(FileData, SubBObjQuantIndex);
            ReturnObjectiveSubBDict.ObjQuantIndex = SubBObjQuantIndex;
            ReturnObjectiveSubBDict.ObjValue = SubBObj;
            ReturnObjectiveSubBDict.ObjInt16LEA = BitConverter.ToInt16(FileData, SubBObjIndex).ToString();
            ReturnObjectiveSubBDict.ObjInt16LEB = BitConverter.ToInt16(FileData, SubBObjIndex);
            ReturnObjectiveSubBDict.ObjByIndex = FileData[SubBObjIndex];
            ReturnObjectiveSubBDict.ObjByIndexHex = FileData[SubBObjIndex].ToString("X2");
            ReturnObjectiveSubBDict.ObjIndex = SubBObjIndex;

            ReturnObjectiveSubBHex = $"\nValue: {objectiveSubBHex}\nReturnObjHex: {ReturnObjectiveHex(FileData, SubBObjHexIndex)}\nHex 1: (IndexInfo): (BCtoSTR): INDEX: {FileData[SubBObjHexIndex]} | INDEX+1: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexA]} | INDEX+2: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexB]} | INDEX+3: {FileData[SubBObjHexIndex + ObjectiveHexIncIndexC]}\nHex 2: {BitConverter.ToString(new byte[] { FileData[SubBObjHexIndex], FileData[SubBObjHexIndex + ObjectiveHexIncIndexA], FileData[SubBObjHexIndex + ObjectiveHexIncIndexB], FileData[SubBObjHexIndex + ObjectiveHexIncIndexC] })}\nHex 3: {FileData[SubBObjHexIndex]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexA]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexB]} {FileData[SubBObjHexIndex + ObjectiveHexIncIndexC]}\nIndex: {SubBObjHexIndex}\n";
            ReturnObjectiveSubBType = $"\nValue: {objectiveSubBType}\nIndex: N/A\n";
            ReturnObjectiveSubBQuant = $"\nValue: {ObjectiveSubBQuant} (* {SubBObjQuantMult})\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubBObjQuantIndex)}\nIndex: {SubBObjQuantIndex}\n";
            ReturnSubBObj = $"\nValue: {SubBObj}\nInt16 (LE ?): {BitConverter.ToInt16(FileData, SubBObjIndex).ToString()} / {BitConverter.ToInt16(FileData, SubBObjIndex)}\nBy Index: {FileData[SubBObjIndex]}\nBy Index (Hex): 0x{FileData[SubBObjIndex].ToString("X2")}\nIndex: {SubBObjIndex}\n";
        }
    }
}
