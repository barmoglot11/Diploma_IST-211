using System.Collections.Generic;
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
        public TaskContainer[] taskContainer;
        public GameObject trackButton;
        
        public void SetQuest(Quest quest) => this.quest = quest;
        public void SetUI()
        {
            title.text = quest.QuestName;
            description.text = quest.QuestDescription.FirstOrDefault(stage => stage.Key == quest.QuestStage).Value;
            SetTasks();
            
        }

        public void SetImages(List<Image> imagesToDissapear, Image imageToAppear)
        {
            trackButton.GetComponent<ButtonImageToggle>().disappearImages = imagesToDissapear;
            trackButton.GetComponent<ButtonImageToggle>().appearImage = imageToAppear;
        }
        
        public void SetTasks()
        {
            foreach (var task in taskContainer)
                task.gameObject.SetActive(true);
            
            if (quest.PreviousTaskStage == quest.QuestStage)
            {
                taskContainer[0].gameObject.SetActive(false);
            }
            else
            {
                taskContainer[0].gameObject.SetActive(true);
                taskContainer[0].taskText.text = quest.StagesDescription.FirstOrDefault(stage => stage.Key == quest.PreviousTaskStage).Value;
            }
            
            taskContainer[1].taskText.text = quest.StagesDescription.FirstOrDefault(stage => stage.Key == quest.QuestStage).Value;
        }

        public void TrackQuest()
        {
            QuestManager.Instance.TrackQuest(quest);
        }
    }
}