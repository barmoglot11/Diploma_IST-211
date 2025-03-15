using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace QUEST
{
	public class QuestManager : MonoBehaviour
	{
		public static QuestManager Instance;
		private static string _questsDB = Application.dataPath + "/QuestDB.json";
		public List<Quest> currentQuests;
		private Quest trackedQuest;
		public List<Quest> completedQuests;

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
			
			quest.QuestStage = stage;

			var maxStage = quest.StagesDescription.Max(stage => stage.Key);
			
			if (maxStage == stage)
				CompleteQuest(quest);
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
		
		public bool IsQuestTracked(Quest quest) => trackedQuest?.Equals(quest) == true;
		
		public void StartQuest(string questID)
		{
			if (currentQuests.Contains(GetQuestInfo(questID)))
				return;
			var json = File.ReadAllText(_questsDB);
			var questsReaded = JsonUtility.FromJson<List<Quest>>(json);
			
			var quest = questsReaded.FirstOrDefault(x => x.QuestID == questID);
			
			if (quest != null)
			{
				currentQuests.Add(quest);
				SetQuestStage(quest.QuestID, 0);
			}
			else
			{
				Debug.LogError($"Квест с ID {questID} не найден.");
			}
		}
	}
}