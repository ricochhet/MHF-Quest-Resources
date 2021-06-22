using System;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.Text.RegularExpressions;
using ReFrontier;

namespace QuestEditor
{
    class Program
    {
        public static bool ReFrontierToolCheck = false;
        public static bool FrontierDataToolCheck = false;
        public static bool FrontierTextToolCheck = false;
        public static bool QuestToolCheck = false;

        static void Main(string[] args)
        {
            /* We have to specify an extra coding provider for more encoding options. 
            https://docs.microsoft.com/en-us/dotnet/api/system.text.codepagesencodingprovider?view=net-5.0
            */
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // QuestReader.LoadQuestData(args);
            if (args.Length < 1) { Console.WriteLine("Too few arguments."); return; }
            string input = args[0];
            string[] modifiedArgs = args.Skip(1).ToArray();

            if (input == "QuestTool") { QuestToolCheck = true; ReFrontierToolCheck = false; FrontierDataToolCheck = false; FrontierTextToolCheck = false; }
            if (input == "ReFrontier") { QuestToolCheck = false; ReFrontierToolCheck = true; FrontierDataToolCheck = false; FrontierTextToolCheck = false; }
            if (input == "FrontierDataTool") { QuestToolCheck = false; ReFrontierToolCheck = false; FrontierDataToolCheck = true; FrontierTextToolCheck = false; }
            if (input == "FrontierTextTool") { QuestToolCheck = false; ReFrontierToolCheck = false; FrontierDataToolCheck = false; FrontierTextToolCheck = true; }

            if (QuestToolCheck) { Console.WriteLine("Integration: Quest Tool"); InitQuestToolIntegration(modifiedArgs); }
            if (ReFrontierToolCheck) { Console.WriteLine("Integration: ReFrontier"); InitReFrontierIntegration(modifiedArgs); }
            if (FrontierDataToolCheck) { Console.WriteLine("Integration: FrontierDataTool"); InitFrontierDataToolIntegration(modifiedArgs); }
            if (FrontierTextToolCheck) { Console.WriteLine("Integration: FrontierTextTool"); InitFrontierTextToolIntegration(modifiedArgs); }

            if (!QuestToolCheck && !ReFrontierToolCheck && !FrontierDataToolCheck && !FrontierTextToolCheck) { Console.WriteLine("No integrations specified"); }
        }

        public static void InitReFrontierIntegration(string[] args)
        {
            ReFrontier.ReFrontier.Initialize(args);
        }

        public static void InitFrontierDataToolIntegration(string[] args)
        {
            FrontierDataTool.FrontierDataTool.Initialize(args);
        }

        public static void InitFrontierTextToolIntegration(string[] args)
        {
            FrontierTextTool.FrontierTextTool.Initialize(args);
        }

        public static void InitQuestToolIntegration(string[] args)
        {
            QuestWriter.Initialize(args);
        }
    }
}
