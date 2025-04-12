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
            if(CurrentQuest != null)
                ChangeMarkerPosition();
            else
            {
                markerObject.SetActive(false);
                markerCanvasObject.SetActive(false);
            }
        }

        void ChangeMarkerPosition()
        {
            var markerLoc = markerConfig.GetConfig(CurrentQuest.QuestID);
            markerObject.GetComponent<RectTransform>().anchoredPosition3D = markerLoc.points[CurrentQuest.QuestStage];
        }
    }
}