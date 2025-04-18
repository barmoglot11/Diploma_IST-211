using QUEST;
using UI;
using UnityEngine;

namespace Managers
{
    public class MarkerController : MonoBehaviour
    {
        public MarkerConfigData markerConfig;

        public GameObject markerObject;
        
        public GameObject markerCanvasObject;

        public Quest CurrentQuest;

        private void Update()
        {
            CurrentQuest = QuestManager.Instance.trackedQuest;
            if (CurrentQuest != null)
            { 
                if (!string.IsNullOrEmpty(CurrentQuest.QuestID))
                {
                        ChangeMarkerPosition();
                        return;
                }
            }
            
            DisableMarkers();
        }

        void ChangeMarkerPosition()
        {
            var markerLoc = markerConfig.GetConfig(CurrentQuest.QuestID);
            if (markerLoc.isActiveAtStage[CurrentQuest.QuestStage])
            {
                EnableMarkers();
                markerObject.GetComponent<RectTransform>().anchoredPosition3D = markerLoc.points[CurrentQuest.QuestStage];
            }
            else
            {
                DisableMarkers();
            }
        }

        void EnableMarkers()
        {
            markerObject.SetActive(true);
            markerCanvasObject.SetActive(true);
        }
        
        void DisableMarkers()
        {
            markerObject.SetActive(false);
            markerCanvasObject.SetActive(false);
        }
    }
}