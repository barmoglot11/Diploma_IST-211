using System.Collections.Generic;
using QUEST;
using TMPro;
using UnityEngine;

namespace DIARY
{
    public class DiaryInterface : MonoBehaviour
    {
        public QuestManager questManager => QuestManager.Instance;
        [Header("Левая панель")]
        public GameObject QuestList;
        public List<QuestContainer> QuestContainers;
        [Header("Правая панель")]
        public Quest OpenedQuest;
        public QuestDesc Description;

        private void Start()
        {
            foreach (Transform container in QuestList.transform)
            {
                QuestContainers.Add(container.GetComponent<QuestContainer>());
            }
        }

        public void SetUI()
        {
            var questList = questManager.currentQuests;
            for (int i = 0; i < questList.Count; i++)
            {
                QuestContainers[i].GetComponentInChildren<TextMeshProUGUI>().text = questList[i].QuestName;
            }
        }

        public void OpenQuest()
        {
            Description.SetUI();
        }
    }
}