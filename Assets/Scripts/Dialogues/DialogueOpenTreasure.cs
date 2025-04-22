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
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator =
                CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist: true) as Character_Text;
            Character_Sprite MC =
                CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate: true);
            MC.SetPosition(Vector2.zero);
// Открытие шкатулки
            yield return narrator.Say(
                "Крышка шкатулки с трудом поддается, на мгновение застревая, будто что-то мешает ей открыться. " +
                "Внутри, на бархатной подкладке, лежат две вещи: пожелтевшая записка и потрепанный кожаный дневник с вытертым золотым тиснением.");

// Осмотр записки
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Первым делом Михаил берет записку, разворачивает её с осторожностью");
            MC.Hightlight();
            yield return MC.Say("Хутор за Чёрной речкой. Спросите у Федора-колесника...");
            MC.UnHightlight();
            yield return narrator.Say("Прищуривается");
            MC.Hightlight();
            yield return MC.Say("Вот это уже что-то стоящее.");

// Анализ записки
            yield return narrator.Say(
                "Записка явно недавняя — чернила не успели выцвести, а бумага сохранила жесткость. На обороте — пятно, похожее на засохшую кровь.");

// Размышления о Федоре
            MC.UnHightlight();
            yield return narrator.Say("Задумчиво вертит её в руках");
            MC.Hightlight();
            yield return MC.Say("Федор-колесник... Почему это имя мне знакомо?");
            MC.UnHightlight();
// Атмосферный момент
            yield return narrator.Say("Где-то за окном хлопает ставень, будто подчеркивая важность момента.");

// Взятие дневника
            
            yield return narrator.Say("Откладывает записку в сторону, берёт дневник");
            yield return narrator.Say(
                "Пальцы задерживаются на потрёпанном переплёте. Дневник тяжёл — не столько от страниц, сколько от странного ощущения, " +
                "будто он не хочет покидать шкатулку.");

// Извлечение дневника
            MC.Hightlight();
            yield return narrator.Say("Резко дёргает дневник, и тот с глухим стуком вырывается из бархатного гнезда");
            yield return MC.Say(
                "Fantaisies macabres... (Мрачные фантазии...) Пусть лучше пылится в участке, чем пугает местных бабушек.");
            MC.UnHightlight();
// Описание дневника
            yield return narrator.Say(
                "Рисунки выполнены тщательно, с пометками на полях — 'водяной, видел в Свирских болотах', 'банник, похищает детей'. " +
                "Некоторые страницы вырваны.");
            yield return narrator.Say(
                "На мгновение ему кажется, что страницы шевельнулись сами по себе, но это просто сквозняк из щели в раме.");

// Закрытие дневника
            MC.UnHightlight();
            yield return narrator.Say("Хмыкает, закрывая дневник");
            MC.Hightlight();
            yield return MC.Say("Видимо, хозяин любил местные сказки... Или самогонку.");

// Убирание дневника
            MC.UnHightlight();
            yield return narrator.Say("Засовывает дневник во внутренний карман сюртука, хлопая по нему ладонью");
            MC.Hightlight();
            yield return MC.Say(
                "Хорошо хоть чернила не выцвели. Пусть следователи поломают голову над этим... творчеством.");
            MC.UnHightlight();
// Неожиданное событие
            yield return narrator.Say(
                "Но когда он поворачивается к выходу, шкатулка вдруг захлопывается с таким грохотом, будто её захлопнула невидимая рука. " +
                "Михаил замирает на полшага.");

// Финальная реплика
            
            yield return narrator.Say("Не оборачиваясь, касается рукояти револьвера");
            MC.Hightlight();
            yield return MC.Say("...Или всё же стоит сжечь эту дрянь на обратном пути.");
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}