using QUEST;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DIARY
{
    public class QuestContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image trackingIndicator;
        
        public Image TrackingIndicator => trackingIndicator;
        public Quest Quest { get; private set; }

        /// <summary>
        /// Инициализирует контейнер данными квеста
        /// </summary>
        public void SetContainer(Quest quest)
        {
            if (quest == null)
            {
                Debug.LogWarning("Attempted to set null quest to container");
                gameObject.SetActive(false);
                return;
            }

            Quest = quest;
            gameObject.SetActive(true);
            
            if (title != null)
                title.text = quest.QuestName;
            else
                Debug.LogError("Title reference is missing in QuestContainer");
        }

        /// <summary>
        /// Открывает связанный квест в интерфейсе
        /// </summary>
        public void OpenQuest()
        {
            if (DiaryInterface.Instance == null)
            {
                Debug.LogError("DiaryInterface instance not found");
                return;
            }

            if (Quest == null)
            {
                Debug.LogWarning("Attempted to open null quest");
                return;
            }

            DiaryInterface.Instance.OnClickReload(Quest);
        }

        /// <summary>
        /// Обновляет состояние индикатора отслеживания
        /// </summary>
        public void UpdateTrackingState(bool isTracked)
        {
            if (trackingIndicator != null)
                trackingIndicator.gameObject.SetActive(isTracked);
        }
    }
}