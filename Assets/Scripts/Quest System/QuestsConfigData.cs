using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace QUEST
{
    [CreateAssetMenu(fileName = "QuestsConfigData", menuName = "Quests", order = 0)]
    [System.Serializable]
    public class QuestsConfigData
    {
        public Quest[] Quests;

        public Quest GetConfig(string questID)
        {
            questID = questID.ToLower();

            foreach (var data in Quests)
            {
                if (string.Equals(questID, data.QuestID.ToLower()))
                    return data.Copy();
            }

            return null;
        }
    }
}