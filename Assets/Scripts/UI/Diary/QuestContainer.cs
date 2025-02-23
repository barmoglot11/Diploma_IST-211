using QUEST;
using TMPro;
using UnityEngine;

namespace DIARY
{
    public class QuestContainer : MonoBehaviour
    {
        public Quest quest;
        public TextMeshProUGUI title;
        public GameObject trackingIndicator;

        public void SetContainer(Quest quest)
        {
            this.quest = quest;
            title.text = this.quest.QuestName;
            if(QuestManager.Instance.IsQuestTracked(quest))
                trackingIndicator.SetActive(true);
        }
    }
}