using System;
using UnityEngine;
using QUEST;

namespace QUEST
{
    public class QuestCharacter : MonoBehaviour
    {
        public GameObject questMarker;
        public GameObject questMarkerMap;

        public string QuestID;
        public int QuestStage;
        private void Update()
        {
            var quest = QuestManager.Instance.GetQuestInfo(QuestID);
            if (QuestManager.Instance.IsQuestTracked(quest) && quest.QuestStage == QuestStage)
            {
                TurnMarkerOn();
            }
            else
            {
                TurnMarkerOff();
            }
        }

        private void TurnMarkerOn()
        {
            questMarker.SetActive(true);
            questMarkerMap.SetActive(true);
        }
        private void TurnMarkerOff()
        {
            questMarker.SetActive(false);
            questMarkerMap.SetActive(false);
        }
    }
}