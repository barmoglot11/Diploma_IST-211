using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using QUEST;
using UnityEngine;

namespace Dialogues
{
    public class DialogueInfoVillage : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            // Подход к церкви
            yield return narrator.Say("Полуразрушенная кладка, кривой крест на крыше. Дверь в трапезную приоткрыта на палец, пахнет ладаном и сыростью.");

// Михаил стучит
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Стучит костяшками пальцев в косяк");
            MC.Hightlight();
            yield return MC.Say("Отец Никифор?");
            MC.UnHightlight();
// Ответ из темноты
            yield return narrator.Say("Из темноты глухой голос");
            yield return villager.Say("Фёдора ищите? Опоздали... В лес ушёл три дня назад. Следы обрываются у старой сосны.");
// Вопрос Михаила
            
            yield return narrator.Say("Прищуривается, пытаясь разглядеть говорящего");
            MC.Hightlight();
            yield return MC.Say("А почему не ищете?");
// Ответ дьячка
            MC.UnHightlight();
            yield return narrator.Say("Пауза. Потом скрип половиц — дьячок отступает вглубь");
            yield return villager.Say("Кто ищет... тот не возвращается.");
// Завершение сцены
            yield return narrator.Say("Дверь захлопывается. На пороге остаётся лишь смятая тряпица — будто кто-то вытирал о неё окровавленные руки.");
            MC.Hide();
            QuestManager.Instance.SetQuestStage("mq002", 0);
            DialogueSystem.instance.CloseDialogue();
            gameObject.SetActive(false);
        }
    }
}