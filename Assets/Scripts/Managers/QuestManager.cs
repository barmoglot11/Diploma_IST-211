using System.IO;
using System.Linq;
using UnityEngine;

namespace QUEST
{
	public class QuestManager : MonoBehaviour
	{
		public static QuestManager Instance;
		private static string _questsDB = "Assets/Scripts/Quest System/QuestDB.json";
		public QuestList currentQuests;
		private Quest trackedQuest;
		public QuestList completedQuests;

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

		public void SetQuestStage(int questID, int stage)
		{
			var quest = GetQuestInfo(questID);
			
			if (quest == null)
			{
				Debug.LogError($"Квест с ID {questID} не найден.");
				return;
			}
			
			quest.QuestStage = stage;

			var maxStage = quest.StagesDescription.Max(sd => sd.StageID);
			
			if (maxStage == stage)
				CompleteQuest(quest);
		}

		public Quest GetQuestInfo(int questID)
		{
			return currentQuests.quests.Find(x => x.QuestID == questID);
		}

		public void CompleteQuest(Quest quest)
		{
			completedQuests.quests.Add(quest);
			currentQuests.quests.Remove(quest);
		}

		public Quest GetCompletedQuestInfo(int questID)
		{
			return completedQuests.quests.Find(x => x.QuestID == questID);
		}

		public void TrackQuest(Quest quest) => trackedQuest = quest;
		
		public bool IsQuestTracked(Quest quest) => trackedQuest?.Equals(quest) == true;
		
		public void StartQuest(int questID)
		{
			if (currentQuests.quests.Contains(GetQuestInfo(questID)))
				return;
			var json = File.ReadAllText(_questsDB);
			var questsReaded = JsonUtility.FromJson<QuestList>(json);
			
			var quest = questsReaded.quests.FirstOrDefault(x => x.QuestID == questID);
			
			if (quest != null)
			{
				currentQuests.quests.Add(quest);
				SetQuestStage(quest.QuestID, 0);
			}
			else
			{
				Debug.LogError($"Квест с ID {questID} не найден.");
			}
		}
	}
}