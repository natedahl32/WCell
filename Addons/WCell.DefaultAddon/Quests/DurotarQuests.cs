using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.Core.Initialization;
using WCell.RealmServer.Quests;

namespace WCell.Addons.Default.Quests
{
    public static class DurotarQuests
    {
        static uint CUTTING_TEETH_QUEST_ID = 788;

        [Initialization]
        [DependentInitialization(typeof(QuestMgr))]
        public static void FixIt()
        {
            // Remove bad requirement
            var cuttingTeeth = QuestMgr.GetTemplate(CUTTING_TEETH_QUEST_ID);
            cuttingTeeth.ReqAllFinishedQuests.Remove(787);
        }
    }
}
