using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    class DialogueParser
    {
        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";

        public static DialogueLine Parse(string rawLine)
        {
            //Debug.Log($"Parsing line - '{rawLine}'");

            (string speaker, string dialogue, string command) = RipContent(rawLine);

            //Debug.Log($"Speaker = '{speaker}'\n Dialogue = '{dialogue}'\n Commands = '{command}'");

            return new DialogueLine(speaker, dialogue, command);
        }

        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for(int i = 0; i < rawLine.Length; i++)
            {
                char current = rawLine[i];
                if (current == '\\')
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                        dialogueEnd = i;
                }
                else
                    isEscaped = false;
            }

            //Debug.Log(rawLine.Substring(dialogueStart + 1, (dialogueEnd - dialogueStart) - 1));

            Regex commangdRegex = new Regex(commandRegexPattern);
            MatchCollection matches = commangdRegex.Matches(rawLine);
            int commandStart = -1;
            foreach(Match match in matches)
            {
                if (match.Index < dialogueStart || match.Index > dialogueEnd)
                {
                    commandStart = match.Index;
                    break;
                }
            }

            if (commandStart != -1 && (dialogueStart == -1 && dialogueEnd == -1))
                return ("", "", rawLine.Trim());

            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart+1, dialogueEnd-dialogueStart -1).Replace("\\\"", "\"");
                if(commandStart != -1)
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
                commands = rawLine;
            else
                dialogue = rawLine;



            return (speaker, dialogue, commands);
        }
    }
}