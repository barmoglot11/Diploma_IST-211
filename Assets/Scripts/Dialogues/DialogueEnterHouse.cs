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
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
// Вход в дом
            yield return narrator.Say("Игрок входит в дом"); 
// Описание прихожей
            yield return narrator.Say("Прихожая встречает гнетущей тишиной. Слишком аккуратно расставленная мебель, слишком чисто вымытые полы - словно кто-то спешил стереть все следы.");
// Первая реплика Михаила
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Проводит пальцем по каминной полке, поднимая тонкую пыль");
            MC.Hightlight();
            yield return MC.Say("Убрали всё... До последней соринки.");

// Вторая реплика Михаила
            MC.UnHightlight();
            yield return narrator.Say("Медленно обходит комнату, его шаги глухо отдаются в пустом пространстве");
            MC.Hightlight();
            yield return MC.Say("Но тень всегда длиннее, чем предмет, который её отбрасывает...");
            MC.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
            gameObject.SetActive(false);
        }
    }
}