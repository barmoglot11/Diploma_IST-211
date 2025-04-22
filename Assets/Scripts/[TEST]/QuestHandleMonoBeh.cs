using QUEST;
using UnityEngine;

namespace _TEST_
{
    public class QuestHandleMonoBeh : MonoBehaviour
    {
        public string questID;
        public int questStage = 0;

        public void OnDisable()
        {
            if (questStage == 0)
            {
                QuestManager.Instance.StartQuest(questID);
            }
            else
            {
                QuestManager.Instance.SetQuestStage(questID, questStage);
            }
            
        }
    }
}