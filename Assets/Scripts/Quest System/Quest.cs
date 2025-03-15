using System.Collections.Generic;

namespace QUEST
{
    public class Quest
    {
        public string QuestName;
        public string QuestID;
        public int QuestStage;
        
        public Dictionary<int, string> StagesDescription; 
        public Dictionary<int, string> QuestDescription; 
    }
}
