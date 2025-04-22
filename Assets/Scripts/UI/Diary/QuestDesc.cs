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
        [Header("References")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TaskContainer[] taskContainers;
        [SerializeField] private GameObject trackButton;
        
        public GameObject TrackButton => trackButton;
        
        private ButtonImageToggle _buttonImageToggle;
        private Quest _currentQuest;

        private void Awake()
        {
            CacheComponents();
            InitializeTaskContainers();
        }

        private void CacheComponents()
        {
            _buttonImageToggle = trackButton?.GetComponent<ButtonImageToggle>();
            
        }

        /// <summary>
        /// Инициализирует и деактивирует контейнеры задач
        /// </summary>
        private void InitializeTaskContainers()
        {
            foreach (var container in taskContainers)
            {
                container?.gameObject.SetActive(false);
            }
        }

        public void SetQuest(Quest quest) => _currentQuest = quest;

        public void SetUI()
        {
            if (_currentQuest == null) return;

            UpdateHeader();
            UpdateTasks();
        }

        private void UpdateHeader()
        {
            title.text = _currentQuest.QuestName;
            description.text = GetQuestDescription(_currentQuest.QuestStage);
        }

        private void UpdateTasks()
        {
            if (taskContainers.Length < 2) return;
            
            UpdatePreviousTask();
            UpdateCurrentTask();
        }

        private void UpdatePreviousTask()
        {
            var container = taskContainers[0];
            bool shouldShow = _currentQuest.PreviousTaskStage != _currentQuest.QuestStage;

            container.gameObject.SetActive(shouldShow);
            if (shouldShow) container.Initialize(GetStageDescription(_currentQuest.PreviousTaskStage));
        }

        private void UpdateCurrentTask()
        {
            var container = taskContainers[1];
            container.gameObject.SetActive(true);
            container.Initialize(GetStageDescription(_currentQuest.QuestStage));
        }

        public void SetImages(List<Image> imagesToDisappear, Image imageToAppear)
        {
            if (_buttonImageToggle == null) return;
            _buttonImageToggle.disappearImages = imagesToDisappear;
            _buttonImageToggle.appearImage = imageToAppear;
        }

        private string GetStageDescription(int stage)
        {
            return _currentQuest.StagesDescription.GetValueOrDefault(stage, "Stage description not found");
        }

        private string GetQuestDescription(int stage)
        {
            return _currentQuest.QuestDescription.GetValueOrDefault(stage, "Quest description not found");
        }
        
        public void TrackQuest()
        {
            if (_currentQuest == null) return;
            QuestManager.Instance.TrackQuest(_currentQuest.QuestID);
            UpdateTaskStates();
        }

        /// <summary>
        /// Обновляет состояние задач на основе текущего прогресса
        /// </summary>
        private void UpdateTaskStates()
        {
            string currentStageDesc = GetStageDescription(_currentQuest.QuestStage);

            foreach (var container in taskContainers)
            {
                if (!container.gameObject.activeSelf) continue;

                bool isCompleted = container.GetCurrentDescription() == currentStageDesc;
                if (isCompleted) container.MarkAsDone();
            }
        }
    }
}