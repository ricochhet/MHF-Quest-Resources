using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using DamienG.Security.Cryptography;

namespace QuestBinTools
{
    class Program
    {
        private static byte[] BaseFile;
        private static BinaryReader brInput;

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
            return str;
        }

        static void Main(string[] args)
        {
            /* Input a quest path */
            FileLoader("./QuestBinTools/QuestBins/21978d0.bin");
            Items.initiate();
            LoadMonsterVariant(BaseFile);
            // LoadRewardInfo(BaseFile);
            // LoadLocations(BaseFile);
            // LoadMainObjective(BaseFile);
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

            return item;
        }

        public static string ReturnObjectiveHex(byte[] FileData, int index)
        {
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

            return monster;
        }

        public static string ReturnInterception(byte[] inputArray)
        {
            string monster = "";
            string monsterAdd = null;
            for (int i = 377; i <= 382 - 1; i++)
            {
                if (inputArray[i] == 0)
                    continue;
                if (Monsters.MonsterNames.TryGetValue(inputArray[i], out monsterAdd))
                {
                    // 
                }
                else
                {
                    monsterAdd = BitConverter.ToSingle(inputArray, i).ToString();
                }

                if (i == 377)
                {
                    monster += monsterAdd;
                }
                else
                {
                    monster += $", {monsterAdd}";
                }
            }

            return monster;
        }

        static void LoadRewardInfo(byte[] FileData)
        {
            int QuestFee = BitConverter.ToInt32(FileData, 204);
            int PrimaryReward = BitConverter.ToInt32(FileData, 208);
            int RewardA = BitConverter.ToInt32(FileData, 216);
            int RewardB = BitConverter.ToInt32(FileData, 220);
        }

        static void LoadLocations(byte[] FileData)
        {
            string location = null;
            if (LocationList.Locations.TryGetValue(BitConverter.ToInt32(FileData, 228), out location))
            {
                //
            }
        }

        static void LoadRank(byte[] FileData) 
        {
            string StatTable = null;
            if (Ranks.RankBands.TryGetValue(BitConverter.ToInt32(FileData, 72), out StatTable))
            {
                StatTable = $"{BitConverter.ToInt32(FileData, 72)}   |   {StatTable}";
            }
            else
            {
                StatTable = BitConverter.ToInt32(FileData, 72).ToString();
            }
        }

        static void LoadMonsterVariant(byte[] FileData) 
        {
            string Variant1A = FileData[337].ToString("X2");
            string Variant2A = FileData[338].ToString("X2");
            string Variant1B = FileData[345].ToString("X2");
            string Variant2B = FileData[346].ToString("X2");
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
                SubAObj = ReturnMonster(FileData, 244);
            }
            else if (objectiveSubAType == "Break Part")
            {
                SubAObj = BitConverter.ToInt16(FileData, 244).ToString();
            }
            else if (objectiveSubAType == "Slay All")
            {
                SubAObj = ReturnInterception(FileData);
            }
            else
            {
                SubAObj = ReturnItem(FileData, 244);
            }

            ObjectiveSubAQuant = BitConverter.ToInt16(FileData, 246);
            if (objectiveSubAType == "Damage" || objectiveSubAType == "Slay or Damage")
            {
                ObjectiveSubAQuant = (int)ObjectiveSubAQuant * 100;
            }
        }

        static void LoadSubBObjective(byte[] FileData)
        {
            string objectiveSubBHex = ReturnObjectiveHex(FileData, 248);
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
                SubBObj = ReturnMonster(FileData, 244);
            }
            else if (objectiveSubBType == "Break Part")
            {
                SubBObj = BitConverter.ToInt16(FileData, 244).ToString();
            }
            else if (objectiveSubBType == "Slay All")
            {
                SubBObj = ReturnInterception(FileData);
            }
            else
            {
                SubBObj = ReturnItem(FileData, 244);
            }

            ObjectiveSubBQuant = BitConverter.ToInt16(FileData, 246);
            if (objectiveSubBType == "Damage" || objectiveSubBType == "Slay or Damage")
            {
                ObjectiveSubBQuant = (int)ObjectiveSubBQuant * 100;
            }
        }
    }
}
