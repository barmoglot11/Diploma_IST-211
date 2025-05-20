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
            mc.displayName = "Михаил";
            yield return narrator.Say("Стол в соседней комнате был завален письмами, исписанными мелким, нервным почерком. Они лежали в беспорядке, будто кто-то пытался спрятать правду под слоем лжи.");
            
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Михаил развернул первое:");
            yield return narrator.Say("«…не было выбора. {a}Они требуют кровь. {a}Каждую осень — одну душу. Иначе заберут всех»");

            mc.Hightlight();
            yield return mc.Say("Выбор? Это не выбор. Это выживание");
            mc.UnHightlight();

            yield return narrator.Say("Следующие были хуже — имена, даты, краткие отчёты: {a}«Анна, 12 октября» , {a}«Иван, 5 ноября»");
            yield return narrator.Say("Последнее письмо — дрожащие строки");
            yield return narrator.Say("«Мария. Не виновата, но они сказали — она будет первой. Дочь барона — достойная жертва. Прости, Господи, прости…»");
            
            mc.Hightlight();
            yield return mc.Say("Первой? {a}Значит, это только начало.");
            mc.UnHightlight();
            
            yield return narrator.Say("Под бумагами лежал пергамент, потрескавшийся от времени и покрытый жёлтыми пятнами, словно его писали на высохшей крови.");
            yield return narrator.Say("На главном листе — портрет Вия. " +
                                      "{a}Его тело — сплетение дерева и плоти: ветви пробиваются сквозь грудь, корни торчат из суставов, а кожа покрыта лишайником, как кора старого дуба.");
            
            mc.Hightlight();
            yield return mc.Say("Это не человек. Это… лес, ставший плотью.");
            mc.UnHightlight();
            
            yield return narrator.Say("На полях пометки:" +
                                      "{a}\n«Не открывает глаза. Но видит всё. Слышит мысли»" +
                                      "{a}\n«Сила в воле. Кто не дрогнет — выживет»" +
                                      "{a}\n«Если он назовёт твоё имя — ты его»");
            
            yield return narrator.Say("Михаил отложил пергамент. Пальцы дрожали." +
                                      "Где-то вдалеке, за стенами дома, лес снова заревел — и теперь Михаил знал, что это не просто зверь, а нечто, что ждёт, слушает и помнит.");
            
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