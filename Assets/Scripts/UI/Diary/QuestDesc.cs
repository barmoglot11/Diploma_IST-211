using System.Linq;
using QUEST;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DIARY
{
    public class QuestDesc : MonoBehaviour
    {
        public Quest quest;
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public GameObject taskContainer;
        public Button trackButton;

        
        public GameObject taskPrefab;
        public void SetQuest(Quest quest) => this.quest = quest;
        public void SetUI()
        {
            title.text = quest.QuestName;
            description.text = quest.QuestDescription.FirstOrDefault(stage => stage.Key == quest.QuestStage).Value;
            SetTasks();
        }

        public void SetTasks()
        {
            foreach (var stage in quest.StagesDescription)
            {
                var task = Instantiate(taskPrefab, taskContainer.transform);
                task.GetComponent<TaskContainer>();
            }
        }
    }
}