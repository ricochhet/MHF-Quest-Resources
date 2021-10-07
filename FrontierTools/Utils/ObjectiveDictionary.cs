using System;
using System.IO;
using System.Collections.Generic;

namespace FrontierTools
{
    public class QuestObjectives
    {
        public static Dictionary<string, string> Objectives = new Dictionary<string, string>()
        {
            {"00000000", "None"},
            {"01000000", "Hunt"},
            {"01010000", "Capture"},
            {"01020000", "Slay"},
            {"04800000", "Damage"},
            {"04800100", "Slay or Damage"},
            {"00000400", "Slay All"},
            {"00000200", "Slay Total"},
            {"02000000", "Deliver"},
            {"04400000", "Break Part"},
            {"02100000", "Deliver Flag"},
            {"10000000", "Esoteric Action"}
        };
    }
}