
namespace FrontierTools
{
    public class Objects
    {
        public class QuestInfo
        {
            public class ReturnLocationDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRankInfoDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRankValueDict
            {
                public int Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRankBandsDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRankUnkDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnQuestFeeDict
            {
                public int Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnPrimaryRewardDict
            {
                public int Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRewardADict
            {
                public int Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnRewardBDict
            {
                public int Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnMonsterVariant1ADict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnMonsterVariant2ADict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnMonsterVariant1BDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnMonsterVariant2BDict
            {
                public string Value { get; set; }
                public int Int32LE { get; set; }
                public byte ByIndex { get; set; }
                public string ByIndexHex { get; set; }
                public int Index { get; set; }
            }

            public class ReturnDeliverStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; }
            }

            public class ReturnQuestTypeNameDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; }
            }

            public class ReturnObjMainStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; }
            }

            public class ReturnObjAStringDict
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; }
            }

            public class ReturnObjBStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; }
            }

            public class ReturnClearReqStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; } 
            }

            public class ReturnFailReqStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; } 
            }

            public class ReturnHirerStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; } 
            }

            public class ReturnDescriptionStringDict 
            {
                public int QuestStringsStart { get; set; }
                public int ReadPointer { get; set; }
                public long brInputSeek { get; set; }
                public string ValueA { get; set; }
                public string ValueB { get; set; }
                public string Bytes { get; set; }
                public string Hex { get; set; } 
            }

            public class ReturnObjectiveMainDict
            {
                public string ObjHexValue { get; set; }
                public string ObjHexHexA { get; set; }
                public string ObjHexHexB { get; set; }
                public string ObjHexHexC { get; set; }
                public byte ObjHexByIndex { get; set; }
                public string ObjHexByIndexHex { get; set; }
                public int ObjHexIndex { get; set; }

                public string ObjTypeValue { get; set; }

                public object ObjQuantValue { get; set; }
                public int ObjQuantMult { get; set; }
                public int ObjQuantInt16LE { get; set; }
                public int ObjQuantIndex { get; set; }

                public string ObjValue { get; set; }
                public string ObjInt16LEA { get; set; }
                public int ObjInt16LEB { get; set; }
                public byte ObjByIndex { get; set; }
                public string ObjByIndexHex { get; set; }
                public int ObjIndex { get; set; }
            }

            public class ReturnObjectiveSubADict
            {
                public string ObjHexValue { get; set; }
                public string ObjHexHexA { get; set; }
                public string ObjHexHexB { get; set; }
                public string ObjHexHexC { get; set; }
                public byte ObjHexByIndex { get; set; }
                public string ObjHexByIndexHex { get; set; }
                public int ObjHexIndex { get; set; }

                public string ObjTypeValue { get; set; }

                public object ObjQuantValue { get; set; }
                public int ObjQuantMult { get; set; }
                public int ObjQuantInt16LE { get; set; }
                public int ObjQuantIndex { get; set; }

                public string ObjValue { get; set; }
                public string ObjInt16LEA { get; set; }
                public int ObjInt16LEB { get; set; }
                public byte ObjByIndex { get; set; }
                public string ObjByIndexHex { get; set; }
                public int ObjIndex { get; set; }
            }

            public class ReturnObjectiveSubBDict
            {
                public string ObjHexValue { get; set; }
                public string ObjHexHexA { get; set; }
                public string ObjHexHexB { get; set; }
                public string ObjHexHexC { get; set; }
                public byte ObjHexByIndex { get; set; }
                public string ObjHexByIndexHex { get; set; }
                public int ObjHexIndex { get; set; }

                public string ObjTypeValue { get; set; }

                public object ObjQuantValue { get; set; }
                public int ObjQuantMult { get; set; }
                public int ObjQuantInt16LE { get; set; }
                public int ObjQuantIndex { get; set; }

                public string ObjValue { get; set; }
                public string ObjInt16LEA { get; set; }
                public int ObjInt16LEB { get; set; }
                public byte ObjByIndex { get; set; }
                public string ObjByIndexHex { get; set; }
                public int ObjIndex { get; set; }
            }
        }
    }
}