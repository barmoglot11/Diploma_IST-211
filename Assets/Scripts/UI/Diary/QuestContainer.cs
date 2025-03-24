using QUEST;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DIARY
{
    public class QuestContainer : MonoBehaviour
    {
        public Quest quest;
        public TextMeshProUGUI title;
        public Image trackingIndicator;

        public void SetContainer(Quest quest)
        {
            this.quest = quest;
            title.text = this.quest.QuestName;
        }

        public void OpenQuest()
        {
            DiaryInterface.Instance.OnClickReload(quest);
        }
    }
}