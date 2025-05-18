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
            yield return narrator.Say("Дверь церкви скрипит, приоткрываясь на несколько ладоней. Сквозь щель пахнет сыростью и ладаном.");

// Михаил стучит
            mc.Show();
            mc.Hightlight();
            yield return mc.Say("Ищу девушку. Бледная, в белом. Была здесь?");
            mc.UnHightlight();
// Ответ из темноты
            yield return narrator.Say("Пауза. Колеблющееся пламя свечи мелькает за дверью.");
            yield return villager.Say("Была. Но не задержалась. Ушла в лес.");
// Вопрос Михаила
            
            yield return narrator.Say("Прищуривается, пытаясь разглядеть говорящего");
            mc.Hightlight();
            yield return mc.Say("Куда именно?");
// Ответ дьячка
            mc.UnHightlight();
            yield return narrator.Say("Голос старосты становится чуть ближе, будто он наклонился");
            yield return villager.Say("Есть поляна. Где дерево с глазами. Ты её узнаешь.");
            yield return villager.Say("Уходи, пока можешь.");
// Завершение сцены
            yield return narrator.Say("Дверь захлопывается. На земле — пепел. Он двигается, собираясь в силуэт птицы. Каркающий смех доносится из пустоты.");
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