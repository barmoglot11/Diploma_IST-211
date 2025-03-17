using System.Collections.Generic;
using MessagePack;

namespace QUEST
{
    [System.Serializable]
    [MessagePackObject]
    public class Quest
    {
        [Key(0)]
        public string QuestName;
        [Key(1)]
        public string QuestID;
        [Key(2)]
        public int QuestStage;
        
        [Key(3)]
        public Dictionary<int, string> StagesDescription; 
        [Key(4)]
        public Dictionary<int, string> QuestDescription; 
    }
}
