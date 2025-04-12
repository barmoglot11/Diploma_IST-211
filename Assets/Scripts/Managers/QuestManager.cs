using System.Collections.Generic;
using System.IO;
using System.Linq;
using SAVELOAD;
using UnityEngine;

namespace QUEST
{
	public class QuestManager : MonoBehaviour
	{
		public static QuestManager Instance;
		public QuestsConfigData questsConfig;
		public List<Quest> currentQuests;
		public Quest trackedQuest;
		public List<Quest> completedQuests;
		private SaveLoadJsonService slService = new SaveLoadJsonService();


		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void SetQuestStage(string questID, int stage)
		{
			var quest = GetQuestInfo(questID);
			
			if (quest == null)
			{
				Debug.LogError($"Квест с ID {questID} не найден.");
				return;
			}
			
			if(quest.QuestStage == stage)
				return;
			
			quest.PreviousTaskStage = quest.QuestStage;
			quest.QuestStage = stage;
			
			if(trackedQuest == quest)
				trackedQuest.QuestStage = stage;
			
			var maxStage = quest.StagesDescription.Max(stage => stage.Key);

			if (maxStage != stage) return;
			
			CompleteQuest(quest);
			trackedQuest = currentQuests.FirstOrDefault(q => q.QuestID != quest.QuestID);

		}

		public Quest GetQuestInfo(string questID)
		{
			return currentQuests.Find(x => x.QuestID == questID);
		}

		public void CompleteQuest(Quest quest)
		{
			completedQuests.Add(quest);
			currentQuests.Remove(quest);
		}

		public Quest GetCompletedQuestInfo(string questID)
		{
			return completedQuests.Find(x => x.QuestID == questID);
		}

		public void TrackQuest(Quest quest) => trackedQuest = quest;
		
		public bool IsQuestTracked(Quest quest) => trackedQuest.QuestID?.Equals(quest.QuestID) == true;
		
		public void StartQuest(string questID)
		{
			if (currentQuests.Contains(GetQuestInfo(questID)))
				return;
			
			var quest = questsConfig.GetConfig(questID);
			
			if (quest != null)
			{
				currentQuests.Add(quest);
				SetQuestStage(quest.QuestID, 0);
			}
			else
			{
				Debug.LogError($"Квест с ID {questID} не найден.");
			}

			if (currentQuests.Count == 1)
			{
				trackedQuest = currentQuests[0];
			}
		}
	}
}