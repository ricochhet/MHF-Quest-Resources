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

            QuestReader.ObjectDumper(QuestReader.ReturnLocationDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnRankInfoDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnRankValueDict);
            QuestReader.NewLine();
            
            QuestReader.ObjectDumper(QuestReader.ReturnRankBandsDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnRankUnkDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnQuestFeeDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnPrimaryRewardDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnRewardADict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnRewardBDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnMonsterVariant1AInfoDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnMonsterVariant2AInfoDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnMonsterVariant1BInfoDict);
            QuestReader.NewLine();

            QuestReader.ObjectDumper(QuestReader.ReturnMonsterVariant2BInfoDict);
            QuestReader.NewLine();
        }
    }
}
