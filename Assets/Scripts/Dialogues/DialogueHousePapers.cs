using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UI;
using UnityEngine;


namespace DIALOGUES
{
    public class DialogueHousePapers : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc is { isFacingLeft: true })
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            if (mc is { isVisible: true })
                mc.Hide();
            
            // Описание конторки
            yield return narrator.Say("Конторка, заваленная бумагами. Чернильница с пером. На стене — портрет Гоголя (гравюра, подпись: \\\"Н.В., 1841\\\").");

// Обнаружение тетради
            yield return narrator.Say("На столе, заваленном пожелтевшими бумагами, лежала раскрытая тетрадь. Её страницы испещрены нервными, дрожащими строчками: \\\"3 октября. Федя привёл меня к Нему. Настоящего Вия. Слава Богу, успел зарисовать, пока тьма не забрала зрение...\\\" Буквы местами расплылись, будто автор писал их дрожащей рукой, смачивая страницы потом или слезами.");

// Описание страницы из дневника
            yield return narrator.Say("Рядом, словно нарочно подложенная для внимательного глаза, лежала знакомая страница из дневника. Вверху крупными, почти истеричными буквами выведено: \\\"Вий, дух проклятых мест\\\". Ниже - детальное описание: \\\"Ростом с вековую сосну, веки свисают до самой земли. Кричит голосами утонувших - слышишь родных, а потом... понимаешь, что их давно нет в живых\\\". В углу, будто добавленное позже, дрожащей рукой приписано: \\\"Не смотри в глаза! Железо гнёт, но не убивает. Зеркала - ложь, но могут спасти\\\".");
            yield return narrator.Say("Страница была испачкана бурыми пятнами, а в местах описания способов защиты бумага истончилась от частого касания пальцев.");

// Реакция Михаила
            mc.Show();
            mc.SetSprite(mc.GetSprite($"{mc.name}-Scared"));
            mc.UnHightlight();
            yield return narrator.Say("Лихорадочно листает страницы, глаза расширяются");
            mc.Hightlight();
            yield return mc.Say("Ils ont décrit l'indescriptible... (Они описали неописуемое...)");
            mc.UnHightlight();
            yield return narrator.Say("Указывает пальцем в рисунок Вия");
            mc.Hightlight();
            yield return mc.Say("Это же то самое чудовище из леса!");
            mc.Hide();
// Атмосферные детали
            yield return narrator.Say("Страницы дневника шелестят, будто подтверждая его догадку. На полях — пометки чернилами: «Слышит шаги», «Боится зеркал?», «Не смотри!».");
            yield return narrator.Say("За окном раздаётся скрежет — будто что-то огромное провело когтями по стене дома.");
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            BestiaryManager.Instance.AddMonsterToList("Ползучий Шёпот(«То, что не следует называть»)");
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}