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
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            // Открытие шкафа
            yield return narrator.Say("Дверцы старого шкафа скрипят, выдавая давно не открывавшиеся петли. На полке, между пожелтевшими бумагами, лежит заколка – её металл холодно поблёскивает в тусклом свете.");

// Обнаружение заколки
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Берёт заколку, ощущая её неожиданный вес в ладони");
            MC.Hightlight();
            yield return MC.Say("Curieux... (Любопытно...) Кто-то спрятал её здесь специально.");
            MC.UnHightlight();
// Осмотр заколки
            yield return narrator.Say("Лезвие заколки неестественно заострено, больше похоже на инструмент, чем на украшение.");

// Размышления Михаила
            
            yield return narrator.Say("Проводит пальцем по краю, задумчиво");
            MC.Hightlight();
            yield return MC.Say("Или готовился к визиту... или ждал гостей.");
            MC.UnHightlight();
// Завершающая атмосфера
            yield return narrator.Say("В воздухе повисает лёгкий запах лаванды, смешанный с чем-то металлическим – будто кто-то недавно держал эту вещь в потной руке.");
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}