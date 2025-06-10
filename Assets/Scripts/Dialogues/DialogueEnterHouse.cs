using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueEnterHouse : MonoBehaviour, IDialogue
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
// Вход в дом
            yield return narrator.Say("Игрок входит в дом"); 
// Описание прихожей
            yield return narrator.Say("Прихожая встречает гнетущей тишиной. Слишком аккуратно расставленная мебель, слишком чисто вымытые полы - словно кто-то спешил стереть все следы.");
// Первая реплика Михаила
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Проводит пальцем по каминной полке, поднимая тонкую пыль");
            mc.Hightlight();
            yield return mc.Say("Убрали всё... До последней соринки.");

// Вторая реплика Михаила
            mc.UnHightlight();
            yield return narrator.Say("Медленно обходит комнату, его шаги глухо отдаются в пустом пространстве");
            mc.Hightlight();
            yield return mc.Say("Но тень всегда длиннее, чем предмет, который её отбрасывает...");
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
            gameObject.SetActive(false);
        }
    }
}