using System.Collections;
using CHARACTER;
using DIALOGUE;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogues
{
    public class DialogueCoachCycle : MonoBehaviour, IDialogue
    {
        public bool isTalkedOnce = false;
        
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(!isTalkedOnce ? Dialogue() : CycleDialogue());
        }

        public IEnumerator CycleDialogue()
        {
            var coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            if (coach is { isVisible: true })
                coach.Hide();
            yield return coach.Say("Карета на месте, я на месте... Чего ещё надо-то?");
            CloseDialogueEvent();
        }
        
        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            var coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            
            if (coach is { isVisible: true })
                coach.Hide();
            if (mc is { isVisible: true })
                mc.Hide();
            
// Сцена у кареты
            yield return narrator.Say("Карета стоит на окраине города, кучер дремлет, развалившись на облучке. Его поношенная шляпа съехала набок, закрывая глаза.");

// Михаил будит кучера
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Стучит по дверце кареты");
            mc.Hightlight();
            yield return mc.Say("Проснись, дружище.");

// Кучер просыпается
            coach.Show();
            coach.UnHightlight();
            yield return narrator.Say("Вздрагивает, поправляя шляпу, глаза мутные от сна");
            coach.Hightlight();
            yield return coach.Say("М-м? Ась? Ваше благородие...");

// Вопрос о времени
            mc.UnHightlight();
            yield return mc.Say("Сколько времени?");

            coach.UnHightlight();
            yield return narrator.Say("Зевая, достаёт карманные часы");
            coach.Hightlight();
            yield return coach.Say("Час пополуночи... или пополудни... Да чёрт их разберёт, эти ваши городские часы...");

// Диалог о возвращении
            mc.UnHightlight();
            yield return mc.Say("Ты меня ждать будешь?");

            coach.UnHightlight();
            yield return narrator.Say("Почесывая щетину, оглядывает мрачные улицы");
            coach.Hightlight();
            yield return coach.Say("Да уж куда деваться... Только ежели что — я спать буду.");
            yield return narrator.Say("Укладывается на облучок, натягивает шляпу на лицо");
            yield return coach.Say("А вы не шумите, будить не надо — я и так проснусь.");

            mc.UnHightlight();
            yield return mc.Say("А если я не вернусь?");

            coach.UnHightlight();
            yield return narrator.Say("Не поднимая шляпы, ворчливо");
            coach.Hightlight();
            yield return coach.Say("Тогда и будить не придётся...");
            yield return narrator.Say("Переворачивается на бок");
            yield return coach.Say("Спите спокойно, ваше благородие...");

// Завершение сцены
            yield return narrator.Say("Кучер начинает похрапывать, словно продолжил разговор во сне. Его карманные часы тикают в такт капающей с крыши воды.");
            mc.Hide();
            coach.Hide();
            
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            isTalkedOnce = true;
            DialogueSystem.instance.CloseDialogue();
        }
    }
}