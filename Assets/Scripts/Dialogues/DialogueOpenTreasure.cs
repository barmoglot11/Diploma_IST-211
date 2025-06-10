using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueOpenTreasure : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator =
                CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist: true) as Character_Text;
            Character_Sprite mc =
                CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate: true);
            mc.SetPosition(Vector2.zero);
            mc.displayName = "Михаил";
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Михаил берёт шкатулку, пальцы задерживаются на трещине крышки. Замок щёлкает, будто вздыхает.");
            mc.Hightlight();
            yield return mc.Say("«Приди до заката...» {a}Как приказ. Не просьба.");
            mc.UnHightlight();
            yield return narrator.Say("Он гладит бумагу, будто ожидая, что слова изменятся. Пальцы оставляют следы на влажном пергаменте.");
            yield return narrator.Say("Он берёт дневник, листает страницы. {a}Каракули по краям напоминают паутину. Один рисунок — лицо с множеством глаз, собранных в комок.");
            mc.Hightlight();
            yield return mc.Say("Что ты скрываешь, Надежда?");


            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}