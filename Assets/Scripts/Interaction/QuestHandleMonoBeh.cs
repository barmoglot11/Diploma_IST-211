using QUEST;
using UnityEngine;

namespace Interaction
{
    public class QuestHandleMonoBeh : MonoBehaviour
    {
        public string questID;
        public int questStage = 0;

        public void QuestHandle()
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