using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.Text.RegularExpressions;

namespace QuestEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestReader.LoadQuestData(args);
        }
    }
}
