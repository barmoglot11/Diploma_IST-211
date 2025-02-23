using System.Collections.Generic;

namespace QUEST
{
    [System.Serializable]
    public class Quest
    {
        public string QuestName;
        public int QuestID;
        public int QuestStage;
        
        public StageDescription[] StagesDescription; 
        public QuestDescription[] QuestDescription; 
    }

    [System.Serializable]
    public class StageDescription
    {
        public int StageID;
        public string Description;
    }

    [System.Serializable]
    public class QuestDescription
    {
        public int DescriptionID;
        public string Description;
    }
}
