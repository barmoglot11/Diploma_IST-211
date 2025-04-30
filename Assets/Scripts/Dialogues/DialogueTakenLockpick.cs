using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;


namespace DIALOGUES
{
    public class DialogueTakenLockpick : MonoBehaviour, IDialogue
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
            // Открытие шкафа
            yield return narrator.Say("Дверцы старого шкафа скрипят, выдавая давно не открывавшиеся петли. На полке, между пожелтевшими бумагами, лежит заколка – её металл холодно поблёскивает в тусклом свете.");

// Обнаружение заколки
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Берёт заколку, ощущая её неожиданный вес в ладони");
            mc.Hightlight();
            yield return mc.Say("Curieux... (Любопытно...) Кто-то спрятал её здесь специально.");
            mc.UnHightlight();
// Осмотр заколки
            yield return narrator.Say("Лезвие заколки неестественно заострено, больше похоже на инструмент, чем на украшение.");

// Размышления Михаила
            
            yield return narrator.Say("Проводит пальцем по краю, задумчиво");
            mc.Hightlight();
            yield return mc.Say("Или готовился к визиту... или ждал гостей.");
            mc.UnHightlight();
// Завершающая атмосфера
            yield return narrator.Say("В воздухе повисает лёгкий запах лаванды, смешанный с чем-то металлическим – будто кто-то недавно держал эту вещь в потной руке.");
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
        }
    }
}