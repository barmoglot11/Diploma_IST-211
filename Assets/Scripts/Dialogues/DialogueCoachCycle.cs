using System.Collections;
using CHARACTER;
using DIALOGUE;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueCoachCycle : MonoBehaviour, IDialogue
    {
        public bool IsTalkedOnce = false;
        
        public void StartDialogue()
        {
            StartCoroutine(!IsTalkedOnce ? Dialogue() : CycleDialogue());
        }

        public IEnumerator CycleDialogue()
        {
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            yield return Coach.Say("Карета на месте, я на месте... Чего ещё надо-то?");
            DialogueSystem.instance.CloseDialogue();
        }
        
        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
// Сцена у кареты
            yield return narrator.Say("Карета стоит на окраине города, кучер дремлет, развалившись на облучке. Его поношенная шляпа съехала набок, закрывая глаза.");

// Михаил будит кучера
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Стучит по дверце кареты");
            MC.Hightlight();
            yield return MC.Say("Проснись, дружище.");

// Кучер просыпается
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Вздрагивает, поправляя шляпу, глаза мутные от сна");
            Coach.Hightlight();
            yield return Coach.Say("М-м? Ась? Ваше благородие...");

// Вопрос о времени
            MC.UnHightlight();
            yield return MC.Say("Сколько времени?");

            Coach.UnHightlight();
            yield return narrator.Say("Зевая, достаёт карманные часы");
            Coach.Hightlight();
            yield return Coach.Say("Час пополуночи... или пополудни... Да чёрт их разберёт, эти ваши городские часы...");

// Диалог о возвращении
            MC.UnHightlight();
            yield return MC.Say("Ты меня ждать будешь?");

            Coach.UnHightlight();
            yield return narrator.Say("Почесывая щетину, оглядывает мрачные улицы");
            Coach.Hightlight();
            yield return Coach.Say("Да уж куда деваться... Только ежели что — я спать буду.");
            yield return narrator.Say("Укладывается на облучок, натягивает шляпу на лицо");
            yield return Coach.Say("А вы не шумите, будить не надо — я и так проснусь.");

            MC.UnHightlight();
            yield return MC.Say("А если я не вернусь?");

            Coach.UnHightlight();
            yield return narrator.Say("Не поднимая шляпы, ворчливо");
            Coach.Hightlight();
            yield return Coach.Say("Тогда и будить не придётся...");
            yield return narrator.Say("Переворачивается на бок");
            yield return Coach.Say("Спите спокойно, ваше благородие...");

// Завершение сцены
            yield return narrator.Say("Кучер начинает похрапывать, словно продолжил разговор во сне. Его карманные часы тикают в такт капающей с крыши воды.");
            MC.Hide();
            Coach.Hide();
            IsTalkedOnce = true;
            DialogueSystem.instance.CloseDialogue();
            
        }
    }
}