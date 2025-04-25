using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UnityEngine;


namespace DIALOGUES
{
    public class DialogueHousePapers : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            // Описание конторки
            yield return narrator.Say("Конторка, заваленная бумагами. Чернильница с пером. На стене — портрет Гоголя (гравюра, подпись: \"Н.В., 1841\").");

// Обнаружение тетради
            yield return narrator.Say("На столе, заваленном пожелтевшими бумагами, лежала раскрытая тетрадь. Её страницы испещрены нервными, дрожащими строчками: \"3 октября. Федя привёл меня к Нему. Настоящего Вия. Слава Богу, успел зарисовать, пока тьма не забрала зрение...\" Буквы местами расплылись, будто автор писал их дрожащей рукой, смачивая страницы потом или слезами.");

// Описание страницы из дневника
            yield return narrator.Say("Рядом, словно нарочно подложенная для внимательного глаза, лежала знакомая страница из дневника. Вверху крупными, почти истеричными буквами выведено: \"Вий, дух проклятых мест\". Ниже - детальное описание: \"Ростом с вековую сосну, веки свисают до самой земли. Кричит голосами утонувших - слышишь родных, а потом... понимаешь, что их давно нет в живых\". В углу, будто добавленное позже, дрожащей рукой приписано: \"Не смотри в глаза! Железо гнёт, но не убивает. Зеркала - ложь, но могут спасти\".");
            yield return narrator.Say("Страница была испачкана бурыми пятнами, а в местах описания способов защиты бумага истончилась от частого касания пальцев.");

// Реакция Михаила
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Лихорадочно листает страницы, глаза расширяются");
            MC.Hightlight();
            yield return MC.Say("Ils ont décrit l'indescriptible... (Они описали неописуемое...)");
            MC.UnHightlight();
            yield return narrator.Say("Указывает пальцем в рисунок Вия");
            MC.Hightlight();
            yield return MC.Say("Это же то самое чудовище из леса!");
            MC.Hide();
// Атмосферные детали
            yield return narrator.Say("Страницы дневника шелестят, будто подтверждая его догадку. На полях — пометки чернилами: «Слышит шаги», «Боится зеркал?», «Не смотри!».");
            yield return narrator.Say("За окном раздаётся скрежет — будто что-то огромное провело когтями по стене дома.");

            
            DialogueSystem.instance.CloseDialogue();
        }
        
        
    }
}