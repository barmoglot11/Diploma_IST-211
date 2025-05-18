using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueMirror : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.Show();
            mc.SetSprite(mc.GetSprite($"{mc.name}-Shocked"));
            mc.Hightlight();
            yield return mc.Say("Кто ты...?");
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.UnHightlight();
            yield return narrator.Say("В зеркале, сквозь дрожь стекла, на него смотрит девушка. Не моргая. Не дыша. Её глаза — будто два окна в зимнюю ночь.");
            yield return narrator.Say("Её рука дрожит. Внезапно палец резко указывает на шкатулку в углу. {a}Михаил оборачивается, взгляд скользит по тени на стене, которая дрожит, будто живая.");
            
            mc.Hightlight();
            yield return mc.Say("Что ты хочешь показать? {a}Что в ней?");
            mc.UnHightlight();
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
        }
    }
}