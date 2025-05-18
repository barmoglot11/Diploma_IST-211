using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueHouseDeadBody : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            
            yield return narrator.Say("Комната пахла сыростью и воском. Свечи вокруг тела догорали, их пламя дрожало, будто от болезни. " +
                                      "\nЛужи желтоватого сала растекались по полу, соединяясь в причудливые узоры — как будто кто-то пытался нарисовать карту ужаса.");
            yield return narrator.Say("Простыня, когда-то белая, теперь пятнистая от времени и чего-то ещё, скрывала силуэт."+
                                      "{a}Михаил отбросил её — лицо Марии было бледным, как лунный свет, но целым. " +
                                      "{a}Не раны, не следы борьбы. Только кожа, холодная и гладкая, будто мрамор. " +
                                      "{a}Её глаза смотрели в потолок, где трещины в штукатурке извивались, словно река, ведущая в никуда.");
            yield return narrator.Say("На шее — едва заметный след: узор из точек, напоминающий узор коры. " +
                                      "\n{a}Михаил прикоснулся к ней — пальцы онемели. " +
                                      "{a}Казалось, тело не мёртвое, а… {a}замороженное . " +
                                      "\nКак будто её душа застыла между мирами, не успев уйти.");


            CloseDialogueEvent();
            
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
        }
    }
}