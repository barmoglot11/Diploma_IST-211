using QUEST;
using UI;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Контроллер для управления маркерами квестов на UI/сцене
    /// </summary>
    public class MarkerController : MonoBehaviour
    {
        [Header("Конфигурация")]
        public MarkerConfigData markerConfig; // Конфиг с позициями маркеров для квестов
        
        [Header("UI элементы")]
        public GameObject markerObject;       // 3D объект маркера в мире
        public GameObject markerCanvasObject; // UI элемент маркера на канвасе

        private Quest CurrentQuest;          // Текущий отслеживаемый квест

        private void Update()
        {
            UpdateCurrentQuest();
            
            if (HasActiveQuest())
            {
                ChangeMarkerPosition();
            }
            else
            {
                DisableMarkers();
            }
        }

        /// <summary>
        /// Обновляет ссылку на текущий квест
        /// </summary>
        private void UpdateCurrentQuest()
        {
            CurrentQuest = QuestManager.Instance.TrackedQuest;
        }

        /// <summary>
        /// Проверяет есть ли активный квест для отображения
        /// </summary>
        private bool HasActiveQuest()
        {
            return CurrentQuest != null && !string.IsNullOrEmpty(CurrentQuest.QuestID);
        }

        /// <summary>
        /// Изменяет позицию маркера в соответствии с текущим этапом квеста
        /// </summary>
        private void ChangeMarkerPosition()
        {
            var markerLoc = markerConfig.GetConfig(CurrentQuest.QuestID);
            
            if (markerLoc.isActiveAtStage[CurrentQuest.QuestStage])
            {
                EnableMarkers();
                UpdateMarkerPosition(markerLoc.points[CurrentQuest.QuestStage]);
            }
            else
            {
                DisableMarkers();
            }
        }

        /// <summary>
        /// Обновляет позицию маркера
        /// </summary>
        private void UpdateMarkerPosition(Vector3 newPosition)
        {
            markerObject.transform.position = newPosition;
        }

        /// <summary>
        /// Активирует маркеры
        /// </summary>
        private void EnableMarkers()
        {
            markerObject.SetActive(true);
            markerCanvasObject.SetActive(true);
        }
        
        /// <summary>
        /// Деактивирует маркеры
        /// </summary>
        private void DisableMarkers()
        {
            markerObject.SetActive(false);
            markerCanvasObject.SetActive(false);
        }
    }
}
