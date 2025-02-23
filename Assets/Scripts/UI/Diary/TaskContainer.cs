using TMPro;
using UnityEngine;

namespace DIARY
{
    public class TaskContainer : MonoBehaviour
    {
        public TextMeshProUGUI taskText;
        public GameObject doneMarker;
        
        public bool TaskDone = false;

        public void DoneTask()
        {
            TaskDone = true;
            doneMarker.SetActive(true);
        }
    }
}