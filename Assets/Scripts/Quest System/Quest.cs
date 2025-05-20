using AYellowpaper.SerializedCollections;
using MessagePack;
using UnityEngine;

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
        public int PreviousTaskStage;

        [Key(4)]
        [SerializedDictionary("Stage", "Text")]

        public SerializedDictionary<int, string> StagesDescription = new();
        [Key(5)]
        [SerializedDictionary("Stage", "Description")]

        public SerializedDictionary<int, string> QuestDescription = new();

        public Quest Copy()
        {
            var result = new Quest
            {
                QuestName = QuestName,
                QuestID = QuestID,
                QuestStage = QuestStage,
                PreviousTaskStage = PreviousTaskStage,
                StagesDescription = new SerializedDictionary<int, string>(StagesDescription),
                QuestDescription = new SerializedDictionary<int, string>(QuestDescription)
            };
            return result;
        }
    }
}
