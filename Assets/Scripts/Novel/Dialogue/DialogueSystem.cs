using CHARACTER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        public TogglePanelOnE toggleScript;
        
        public Button CloseButton;

        public void SetupCloseEvent(UnityAction closeAction)
        {
            CloseButton.onClick.RemoveAllListeners();
            CloseButton.onClick.AddListener(closeAction);
        }
        
        public void OpenDialogue()
        {
            toggleScript.ToggleCanvasesAction();
            
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            InputManager.Instance.EnableCharacterControls(false);
        }
        
        public void CloseDialogue()
        {
            toggleScript.ToggleCanvasesAction();
            
            InputManager.Instance.ChangeInputStatus(InputStatus.Gameplay);
            InputManager.Instance.EnableCharacterControls(true);
        }
        
        /// <summary>
        /// Контейнер для диалога
        /// </summary>
        public DialogueContainer dialogueContainer = new DialogueContainer();

        private ConversationManager conversationManager; // Менеджер разговоров
        private TextArchitect architect; // Архитектор текста

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next; // Событие для обработки пользовательского ввода

        /// <summary>
        /// Экземпляр Singleton (обеспечивает доступ к единственному экземпляру класса)
        /// </summary>
        public static DialogueSystem instance { get; private set; }

        public bool isRunningConversation => conversationManager.isRunnging; // Проверка, идет ли разговор

        public bool IsRunningDialogue => conversationManager.IsRunningText;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize(); // Инициализация при первом создании экземпляра
            }
            else
            {
                DestroyImmediate(gameObject); // Уничтожение дубликата
            }
        }

        bool _initialized = false; // Флаг инициализации
        private void Initialize()
        {
            if (_initialized) return;

            architect = new TextArchitect(dialogueContainer.dialogueText); // Создание архитектора текста
            conversationManager = new ConversationManager(architect); // Создание менеджера разговоров
            _initialized = true; // Установка флага инициализации
        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke(); // Вызов события при следующем вводе пользователя
        }

        public void ApplySpeakerDatToDialogueContainer(string speakerName)
        {
            Character character = CharacterManager.Instance.GetCharacter(speakerName);
            CharacterConfigData config = character != null ? character.config : CharacterManager.Instance.GetCharacterConfig(speakerName);

            ApplySpeakerDatToDialogueContainer(config); // Применение данных о спикере к контейнеру диалога
        }

        public void ApplySpeakerDatToDialogueContainer(CharacterConfigData config)
        {
            dialogueContainer.SetDialogueColor(config.dialogueColor); // Установка цвета текста
            dialogueContainer.SetDialogueFont(config.dialogueFont); // Установка шрифта текста
            dialogueContainer.nameContainer.SetNameColor(config.nameColor); // Установка цвета имени
            dialogueContainer.nameContainer.SetNameFont(config.nameFont); // Установка шрифта имени
        }

        public void ShowSpeakerName(string speakerName = "") 
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName); // Показ имени спикера
            else
                HideSpeakerName(); // Скрыть имя, если это рассказчик
        }

        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide(); // Скрытие имени спикера

        public Coroutine Say(string speaker, string dialogue)
        {
            DialogueHistory.Instance.AddDialogue(speaker, dialogue);
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            return Say(conversation); // Начало разговора с одним сообщением
        }

        public Coroutine Say(List<string> conversation)
        {
            return conversationManager.StartConversation(conversation); // Начало разговора с несколькими сообщениями
        }
    }
}
