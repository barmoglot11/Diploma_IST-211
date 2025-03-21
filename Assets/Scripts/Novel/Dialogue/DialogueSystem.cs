using CHARACTER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;
        /// <summary>
        /// ????????? ? ????????
        /// </summary>
        //[SerializeField] private
        public DialogueContainer dialogueContainer = new DialogueContainer();
        /// <summary>
        /// 
        /// </summary>
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        /// <summary>
        /// ?????????? Singleton(?????? ???? ???????? ??????? ? ???????)
        /// </summary>
        public static DialogueSystem instance { get; private set; }

        public bool isRunningConversation => conversationManager.isRunnging;

        /// <summary>
        /// ?????????? Singleton(?????? ???? ???????? ??????? ? ???????)
        /// </summary>
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);


        }

        bool _initialized = false;
        private void Initialize()
        {
            if(_initialized) return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke();
        }

        public void ApplySpeakerDatToDialogueContainer(string speakerName)
        {
            Character character = CharacterManager.Instance.GetCharacter(speakerName);
            CharacterConfigData config = character != null ? character.config : CharacterManager.Instance.GetCharacterConfig(speakerName);

            ApplySpeakerDatToDialogueContainer(config);
        }

        public void ApplySpeakerDatToDialogueContainer(CharacterConfigData config)
        {
            dialogueContainer.SetDialogueColor(config.dialogueColor);
            dialogueContainer.SetDialogueFont(config.dialogueFont);
            dialogueContainer.nameContainer.SetNameColor(config.nameColor);
            dialogueContainer.nameContainer.SetNameFont(config.nameFont);
        }

        public void ShowSpeakerName(string speakerName = "") 
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerName();
        }
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();
        

        public Coroutine Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            return Say(conversation);
        }

        public Coroutine Say(List<string> conversation)
        {
            return conversationManager.StartConversation(conversation);
        }
        
        
    }
}