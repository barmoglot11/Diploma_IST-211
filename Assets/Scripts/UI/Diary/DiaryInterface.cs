using System.Collections;
using System.Collections.Generic;
using QUEST;
using UnityEngine;
using UnityEngine.UI;

namespace DIARY
{
    public class DiaryInterface : MonoBehaviour
    {
        private const int MIN_QUEST_INDEX = 0;
        
        [Header("Left Panel")]
        [SerializeField] private GameObject questList;
        [SerializeField] private GameObject questListObject;
        [SerializeField] private List<QuestContainer> questContainers = new();
        
        [Header("Right Panel")]
        [SerializeField] private QuestDesc description;
        [SerializeField] private GameObject questPrefab;

        [Header("Visual Settings")]
        [SerializeField] [Range(1f, 10f)] private float fadeSpeed = 5f;

        private Quest openedQuest;
        private CanvasGroup descriptionCanvasGroup;
        [SerializeField]private QuestManager questManager;
        public static DiaryInterface Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (description != null)
                descriptionCanvasGroup = description.GetComponent<CanvasGroup>();
            
            if (questList == null)
                Debug.LogError("Quest List reference is missing in DiaryInterface");
            if(questManager == null)
                questManager = QuestManager.Instance;
        }

        private void Start()
        {
            InitializeQuestContainers();
            SetUI();
        }

        /// <summary>
        /// Инициализирует контейнеры квестов
        /// </summary>
        private void InitializeQuestContainers()
        {
            if (questList == null) return;

            foreach (Transform container in questList.transform)
            {
                if (container.TryGetComponent(out QuestContainer questContainer))
                {
                    questContainers.Add(questContainer);
                }
            }
        }

        /// <summary>
        /// Обновляет весь интерфейс дневника
        /// </summary>
        public void SetUI()
        {
            if (questManager == null)
            {
                Debug.LogError("Quest Manager reference is missing");
                return;
            }

            var currentQuests = questManager.CurrentQuests.Values;
            UpdateQuestContainers(currentQuests);
            OpenDefaultQuest();
        }
        
        /// <summary>
        /// Обновляет контейнеры квестов в соответствии с текущим списком
        /// </summary>
        private void UpdateQuestContainers(IEnumerable<Quest> currentQuests)
        {
            int index = 0;
            foreach (var quest in currentQuests)
            {
                if (index >= questContainers.Count)
                {
                    CreateNewQuestContainer(quest);
                }
                else
                {
                    UpdateExistingContainer(questContainers[index], quest);
                }
                index++;
            }

            DeactivateExcessContainers(index);
        }

        private void CreateNewQuestContainer(Quest quest)
        {
            if (questPrefab == null) return;

            var newContainer = Instantiate(questPrefab, questListObject.transform);
            if (newContainer.TryGetComponent(out QuestContainer container))
            {
                container.SetContainer(quest);
                questContainers.Add(container);
            }
        }

        private void UpdateExistingContainer(QuestContainer container, Quest quest)
        {
            if (container != null)
            {
                container.SetContainer(quest);
                container.gameObject.SetActive(true);
            }
        }

        private void DeactivateExcessContainers(int activeCount)
        {
            for (int i = activeCount; i < questContainers.Count; i++)
            {
                if (questContainers[i] != null)
                    questContainers[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Открывает квест по умолчанию (первый в списке)
        /// </summary>
        private void OpenDefaultQuest()
        {
            if (questContainers.Count > MIN_QUEST_INDEX && questContainers[MIN_QUEST_INDEX] != null)
            {
                openedQuest = questContainers[MIN_QUEST_INDEX].Quest;
                OpenQuest(openedQuest);
            }
        }

        /// <summary>
        /// Обновляет изображения отслеживания для всех квестов
        /// </summary>
        private void GetTrackImage()
        {
            if (questContainers.Count == 0 || description == null) return;

            var imagesToDisappear = new List<Image>();
            Image imageToAppear = null;
            bool foundTrackedQuest = false;

            foreach (var container in questContainers)
            {
                if (container == null) continue;

                if (QuestManager.Instance.IsQuestTracked(container.Quest))
                {
                    imagesToDisappear.Add(container.TrackingIndicator);
                    foundTrackedQuest = true;
                }
                else
                {
                    imageToAppear ??= container.TrackingIndicator;
                }
            }

            if (!foundTrackedQuest && questContainers.Count > 0)
            {
                imageToAppear = questContainers[0].TrackingIndicator;
            }

            var buttonToggle = description.TrackButton?.GetComponent<ButtonImageToggle>();
            if (buttonToggle != null)
            {
                description.SetImages(imagesToDisappear, imageToAppear);
            }
        }

        /// <summary>
        /// Обработчик клика для перезагрузки панели квеста
        /// </summary>
        public void OnClickReload(Quest quest)
        {
            if (quest == null) return;
            StartCoroutine(ReloadPanelCoroutine(quest));
        }

        /// <summary>
        /// Открывает указанный квест в детализированном view
        /// </summary>
        private void OpenQuest(Quest quest)
        {
            if (description == null || quest == null) return;

            description.SetQuest(quest);
            description.SetUI();
            GetTrackImage();
            UpdateTrackingIndicators();
        }

        /// <summary>
        /// Обновляет все индикаторы отслеживания
        /// </summary>
        private void UpdateTrackingIndicators()
        {
            foreach (var container in questContainers)
            {
                if (container != null)
                {
                    bool isTracked = QuestManager.Instance.IsQuestTracked(container.Quest);
                    container.UpdateTrackingState(isTracked);
                }
            }
        }

        /// <summary>
        /// Корутина для плавного переключения панели квеста
        /// </summary>
        private IEnumerator ReloadPanelCoroutine(Quest quest)
        {
            if (descriptionCanvasGroup == null) yield break;

            yield return StartCoroutine(FadeOut(descriptionCanvasGroup));
            OpenQuest(quest);
            yield return StartCoroutine(FadeIn(descriptionCanvasGroup));
        }

        /// <summary>
        /// Плавное появление элемента интерфейса
        /// </summary>
        private IEnumerator FadeIn(CanvasGroup panel)
        {
            if (panel == null) yield break;

            panel.gameObject.SetActive(true);
            float targetAlpha = 1f;
            float currentAlpha = panel.alpha;

            while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
            {
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
                panel.alpha = currentAlpha;
                yield return null;
            }

            panel.alpha = targetAlpha;
        }

        /// <summary>
        /// Плавное исчезновение элемента интерфейса
        /// </summary>
        private IEnumerator FadeOut(CanvasGroup panel)
        {
            if (panel == null) yield break;

            float targetAlpha = 0f;
            float currentAlpha = panel.alpha;

            while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
            {
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
                panel.alpha = currentAlpha;
                yield return null;
            }

            panel.alpha = targetAlpha;
            panel.gameObject.SetActive(false);
        }
    }
}