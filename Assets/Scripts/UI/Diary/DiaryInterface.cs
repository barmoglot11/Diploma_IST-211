using System.Collections;
using System.Collections.Generic;
using QUEST;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DIARY
{
    public class DiaryInterface : MonoBehaviour
    {
        public static DiaryInterface Instance;
        public QuestManager questManager => QuestManager.Instance;
        [Header("Левая панель")]
        public GameObject QuestList;
        public List<QuestContainer> QuestContainers;
        [Header("Правая панель")]
        public Quest OpenedQuest;
        public QuestDesc Description;
        public CanvasGroup DescriptionCanvasGroup => Description.GetComponent<CanvasGroup>();
        public GameObject QuestPrefab;
        public GameObject QuestListObject;
        [Header("Настройки визуала")]
        public float fadeSpeed = 5f;

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

        private void Start()
        {
            foreach (Transform container in QuestList.transform)
            {
                //QuestContainers.Add(container.GetComponent<QuestContainer>());
            }
            SetUI();
        }

        public void SetUI()
        {
            var questList = questManager.currentQuests;
            for (int i = 0; i < questList.Count; i++)
            {
                if(QuestContainers[i] == null)
                {
                    var questCont = Instantiate(QuestPrefab, QuestListObject.transform);
                    QuestContainers.Add(questCont.GetComponent<QuestContainer>());
                }
                QuestContainers[i].SetContainer(questList[i]);
            }
            OpenedQuest = QuestContainers[0].quest;
            OpenQuest(OpenedQuest);
            GetTrackImage();
        }

        private void GetTrackImage()
        {
            var images = new List<Image>();
            var imageToTrack = QuestContainers.Find(q => q.quest == Description.quest).trackingIndicator;

            for (var i = 0; i < QuestContainers.Count; i++)
            {
                if(QuestManager.Instance.IsQuestTracked(QuestContainers[i].quest))
                    images.Add(QuestContainers[i].trackingIndicator);
                else
                    imageToTrack = QuestContainers[i].trackingIndicator;
            }
            Description.SetImages(images, imageToTrack);
        }
        
        /// <summary>
        /// Переключение выбранного квеста для кнопки
        /// </summary>
        /// <param name="quest">Квест, который храниться в кнопке</param>
        public void OnClickReload(Quest quest)
        {
            StartCoroutine(ReloadPanel(quest));
        }
        private void OpenQuest(Quest quest)
        {
            Description.SetQuest(quest);
            Description.SetUI();
            GetTrackImage();
            
        }
        
        /// <summary>
        /// Переключение выбранного квеста для кнопки с помощью корутин
        /// </summary>
        /// <param name="quest">Квест, который храниться в кнопке</param>
        /// <returns></returns>
        private IEnumerator ReloadPanel(Quest quest)
        {
            yield return StartCoroutine(FadeOut(DescriptionCanvasGroup));
            OpenQuest(quest);
            yield return StartCoroutine(FadeIn(DescriptionCanvasGroup));
        }
        
        /// <summary>
        /// Выход панели из прозрачности через корутину
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <returns></returns>
        private IEnumerator FadeIn(CanvasGroup panel)
        {
            if (panel == null) yield break;

            panel.gameObject.SetActive(true);
            float elapsed = 0f;
            float startAlpha = 0; // Начальная прозрачность 0
            float endAlpha = 1; // Конечная прозрачность

            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime * fadeSpeed;
                panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
                yield return null;
            }

            panel.alpha = endAlpha;
        }

        /// <summary>
        /// Уход панели в прозрачность через корутину
        /// </summary>
        /// <param name="panel">Панель</param>
        /// <returns></returns>
        private IEnumerator FadeOut(CanvasGroup panel)
        {
            if (panel == null) yield break;

            float elapsed = 0f;
            float startAlpha = panel.alpha;
            float endAlpha = 0; // Конечная прозрачность

            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime * fadeSpeed;
                panel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
                yield return null;
            }

            panel.alpha = endAlpha;
            panel.gameObject.SetActive(false);
        }
    }
}