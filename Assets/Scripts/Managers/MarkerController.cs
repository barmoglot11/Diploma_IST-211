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
        public GameObject markerCanvas;

        private Quest CurrentQuest;          // Текущий отслеживаемый квест
        [SerializeField] private bool _hasActiveQuest;

        private void Update()
        {
            UpdateCurrentQuest();
            HasActiveQuest();
            
            if (_hasActiveQuest)
            {
                ChangeMarkerPosition();
            }
            else
            {
                DisableMarkers();
            }
        }

        private void UpdateCurrentQuest()
        {
            CurrentQuest = QuestManager.Instance.TrackedQuest;
        }

        private void HasActiveQuest()
        {
            _hasActiveQuest = CurrentQuest != null && !string.IsNullOrEmpty(CurrentQuest.QuestID);
        }

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

        private void UpdateMarkerPosition(Vector3 newPosition)
        {
            // markerObject — это World Space Canvas
            if (markerObject != null)
            {
                markerObject.transform.position = newPosition;
            }
        }


        private void EnableMarkers()
        {
            if (markerCanvas != null) markerCanvas.SetActive(true);
            if (markerCanvasObject != null) markerCanvasObject.SetActive(true);
        }
        
        private void DisableMarkers()
        {
            if (markerCanvas != null) markerCanvas.SetActive(false);
            if (markerCanvasObject != null) markerCanvasObject.SetActive(false);
        }
    }
}
