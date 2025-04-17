using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMMANDS;
using CHARACTER;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
        public bool isRunnging => process != null;

        private TextArchitect architect = null;
        private bool userPrompt = false;

        public bool IsRunningText => architect.isBuilding;
        
        public ConversationManager(TextArchitect architect) 
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next; 
        }

        public void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();
            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
            return process;
        }

        public void StopConversation()
        {
            if (!isRunnging)
                return;

            dialogueSystem.StopCoroutine(process);
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;
                DialogueLine line = DialogueParser.Parse(conversation[i]);

                if (line.hasDialogue)
                     yield return Line_RunDialogue(line);

                if (line.hasCommands)
                    yield return Line_RunCommands(line);

                if (line.hasDialogue)
                    yield return WaitForUserInput();
            }
        }

        IEnumerator Line_RunDialogue(DialogueLine line)
        {
            if (line.hasSpeaker)
                HandleSpeakerLogic(line.speakerData);
           
            yield return BuildLineSegments(line.dialogueData);
            
        }

        private void HandleSpeakerLogic(DL_SpeakerData speakerData)
        {
            bool characterMustBeCreated = (speakerData.makeCharacterEnter || speakerData.isCastingExpressions || speakerData.isCastingPosition);
            Character character = CharacterManager.Instance.GetCharacter(speakerData.name, createIfDoesNotExist: characterMustBeCreated);

            if (speakerData.makeCharacterEnter && (!character.isVisible && !character.isRevealing))
                character.Show();

            dialogueSystem.ShowSpeakerName(speakerData.displayName);

            DialogueSystem.instance.ApplySpeakerDatToDialogueContainer(speakerData.name);

            if (speakerData.isCastingPosition)
                character.MoveToPosition(speakerData.castPosition);

            if (speakerData.isCastingExpressions)
            {
                foreach(var ce in speakerData.CastExpressions)
                {
                    character.OnRecieveCastingExpression(ce.layer, ce.expresion);
                }
            }
        }

        IEnumerator BuildLineSegments(DL_DialogueData dLine)
        {
            for(int i =0; i < dLine.segments.Count; i++)
            {
                DL_DialogueData.DialogueSegment segment = dLine.segments[i];
                Debug.Log(segment.dialogue);
                yield return WaitForDialogueSegmentSignalToBeTrigger(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }

        IEnumerator WaitForDialogueSegmentSignalToBeTrigger(DL_DialogueData.DialogueSegment segment)
        {
            switch (segment.startSignal)
            {
                case DL_DialogueData.DialogueSegment.StartSignal.C:
                case DL_DialogueData.DialogueSegment.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DialogueData.DialogueSegment.StartSignal.WC:
                case DL_DialogueData.DialogueSegment.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;
            }
        }

        IEnumerator Line_RunCommands(DialogueLine line)
        {
            List<DL_Command_Data.Command> commands = line.commandsData.commands;
            
            foreach(DL_Command_Data.Command command in commands)
            {
                if (command.waitForComplletion || command.name == "wait")
                    yield return CommandManager.instance.Execute(command.name, command.arguments);
                else
                    CommandManager.instance.Execute(command.name, command.arguments);
            }

            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);

            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }
                
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
            {
                yield return null;
            }

            userPrompt = false;
        }
    }
}