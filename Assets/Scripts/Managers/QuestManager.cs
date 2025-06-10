using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace QUEST
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager _instance;
        [SerializeField] private QuestsConfigData questsConfig;
        
        [SerializedDictionary("Quest ID", "Quest Data")]
        [SerializeField] private SerializedDictionary<string, Quest> currentQuests = new SerializedDictionary<string, Quest>();
        
        [SerializeField] private Quest trackedQuest;
        
        [SerializedDictionary("Quest ID", "Completed Quest")]
        [SerializeField] private SerializedDictionary<string, Quest> completedQuests = new SerializedDictionary<string, Quest>();

        public static QuestManager Instance => _instance;
        public IReadOnlyDictionary<string, Quest> CurrentQuests => currentQuests;
        public Quest TrackedQuest => trackedQuest;
        public IReadOnlyDictionary<string, Quest> CompletedQuests => completedQuests;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetQuestStage(string questID, int stage)
        {
            if (!currentQuests.TryGetValue(questID, out Quest quest))
            {
                Debug.LogError($"Quest with ID {questID} not found.");
                return;
            }

            if (quest.QuestStage == stage) return;

            quest.PreviousTaskStage = quest.QuestStage;
            quest.QuestStage = stage;

            int maxStage = quest.StagesDescription.Keys.Max();
            if (maxStage == stage)
            {
                CompleteQuest(quest);
            }
            else
            {
                OnQuestStageUpdated?.Invoke(quest);
            }
        }

        public Quest GetQuestInfo(string questID)
        {
            return currentQuests.TryGetValue(questID, out Quest quest) ? quest : null;
        }

        public void CompleteQuest(Quest quest)
        {
            if (!currentQuests.ContainsKey(quest.QuestID)) return;

            currentQuests.Remove(quest.QuestID);
            completedQuests.Add(quest.QuestID, quest);

            // Update tracked quest
            trackedQuest = currentQuests.Values.FirstOrDefault();
            OnQuestCompleted?.Invoke(quest);
        }

        public Quest GetCompletedQuestInfo(string questID)
        {
            return completedQuests.TryGetValue(questID, out Quest quest) ? quest : null;
        }

        public void TrackQuest(string questID)
        {
            if (currentQuests.TryGetValue(questID, out Quest quest))
            {
                trackedQuest = quest;
                OnQuestTracked?.Invoke(quest);
            }
        }

        public bool IsQuestTracked(Quest quest)
        {
            return trackedQuest != null && trackedQuest.QuestID == quest.QuestID;
        }

        public void StartQuest(string questID)
        {
            if (currentQuests.ContainsKey(questID)) return;

            Quest template = questsConfig.GetConfig(questID);
            if (template == null)
            {
                Debug.LogError($"Quest with ID {questID} not found in config.");
                return;
            }

            Quest newQuest = template.Copy();
            currentQuests.Add(questID, newQuest);
            SetQuestStage(questID, 0);

            if (currentQuests.Count == 1)
            {
                TrackQuest(questID);
            }
        }

        // Event system
        public delegate void QuestEventHandler(Quest quest);
        public event QuestEventHandler OnQuestCompleted;
        public event QuestEventHandler OnQuestTracked;
        public event QuestEventHandler OnQuestStageUpdated;
    }
}