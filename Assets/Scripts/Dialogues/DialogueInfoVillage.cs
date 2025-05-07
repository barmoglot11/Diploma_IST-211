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
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            var villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            // Подход к церкви
            yield return narrator.Say("Полуразрушенная кладка, кривой крест на крыше. Дверь в трапезную приоткрыта на палец, пахнет ладаном и сыростью.");

// Михаил стучит
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Стучит костяшками пальцев в косяк");
            mc.Hightlight();
            yield return mc.Say("Отец Никифор?");
            mc.UnHightlight();
// Ответ из темноты
            yield return narrator.Say("Из темноты глухой голос");
            yield return villager.Say("Фёдора ищите? Опоздали... В лес ушёл три дня назад. Следы обрываются у старой сосны.");
// Вопрос Михаила
            
            yield return narrator.Say("Прищуривается, пытаясь разглядеть говорящего");
            mc.Hightlight();
            yield return mc.Say("А почему не ищете?");
// Ответ дьячка
            mc.UnHightlight();
            yield return narrator.Say("Пауза. Потом скрип половиц — дьячок отступает вглубь");
            yield return villager.Say("Кто ищет... тот не возвращается.");
// Завершение сцены
            yield return narrator.Say("Дверь захлопывается. На пороге остаётся лишь смятая тряпица — будто кто-то вытирал о неё окровавленные руки.");
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            QuestManager.Instance.SetQuestStage("mq002", 5);
            DialogueSystem.instance.CloseDialogue();
            gameObject.SetActive(false);
        }
    }
}