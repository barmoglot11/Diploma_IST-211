namespace DIALOGUE
{
    class DialogueLine
    {
        public DL_SpeakerData speakerData;
        public DL_DialogueData dialogueData;
        public DL_Command_Data commandsData;

        public bool hasSpeaker => speakerData != null; //speaker != string.Empty;
        public bool hasDialogue => dialogueData != null;
        public bool hasCommands => commandsData != null;

        public DialogueLine(string speaker, string dialogue, string commands)
        {
            this.speakerData = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_SpeakerData(speaker));
            this.dialogueData = (string.IsNullOrWhiteSpace(dialogue) ? null : new DL_DialogueData(dialogue));
            this.commandsData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_Command_Data(commands));
        }
    }
}