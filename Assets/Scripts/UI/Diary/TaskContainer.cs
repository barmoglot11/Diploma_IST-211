using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DIARY
{
    public class TaskContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private GameObject doneMarker;

        public TextMeshProUGUI TaskText => taskText;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onTaskCompleted;

        private bool _taskDone;
        private string _currentDescription;

        /// <summary>
        /// Состояние выполнения задачи (ReadOnly)
        /// </summary>
        public bool TaskDone
        {
            get => _taskDone;
            private set
            {
                if (_taskDone == value) return;
                
                _taskDone = value;
                UpdateTaskVisuals();
                
                if (_taskDone)
                    onTaskCompleted?.Invoke();
            }
        }

        /// <summary>
        /// Инициализирует контейнер задачи
        /// </summary>
        /// <param name="description">Текст задачи</param>
        public void Initialize(string description)
        {
            _currentDescription = description;
            
            if (taskText != null)
                taskText.text = description;
            else
                Debug.LogError("TextMeshProUGUI component is missing!", gameObject);

            TaskDone = false;
        }

        /// <summary>
        /// Отмечает задачу как выполненную
        /// </summary>
        public void MarkAsDone()
        {
            if (!TaskDone)
                TaskDone = true;
        }

        /// <summary>
        /// Обновляет визуальные элементы состояния задачи
        /// </summary>
        private void UpdateTaskVisuals()
        {
            if (doneMarker != null)
                doneMarker.SetActive(TaskDone);
            else
                Debug.LogError("Done Marker reference is missing!", gameObject);

            if (taskText != null)
                taskText.fontStyle = TaskDone ? FontStyles.Strikethrough : FontStyles.Normal;
        }

        /// <summary>
        /// Сбрасывает состояние задачи к начальному
        /// </summary>
        public void ResetTask()
        {
            TaskDone = false;
            if (taskText != null)
                taskText.text = _currentDescription;
        }
        
        public string GetCurrentDescription() => _currentDescription;
    }
}