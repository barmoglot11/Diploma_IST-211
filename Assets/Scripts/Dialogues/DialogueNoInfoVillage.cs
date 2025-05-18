using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interaction;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueNoInfoVillage : MonoBehaviour, IDialogue
    {
        public int iteration = 1;
        public QuestHandleMonoBeh questHandle;
        
        public void StartDialogue()
        {
            switch (iteration)
            {
                case 1:
                    DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
                    StartCoroutine(Dialogue());
                    questHandle.questStage++;
                    questHandle.QuestHandle();
                    break;
                case 2:
                    DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
                    StartCoroutine(Dialogue2());
                    questHandle.questStage++;
                    questHandle.QuestHandle();
                    break;
                case 3:
                    DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
                    StartCoroutine(Dialogue3());
                    questHandle.questStage++;
                    questHandle.QuestHandle();
                    break;
                default:
                    break;
            }
            
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.Show();
            mc.UnHightlight();
            // Первая изба
            yield return narrator.Say("Михаил подходит к окну с плотно задёрнутыми занавесками. Ткань вздрагивает — щель между досками вспыхивает глазом, тут же исчезающим.");
            mc.Hightlight();
            yield return mc.Say("Скажите, видели ли вы тут женщину приезжую?");
            mc.UnHightlight();
            yield return narrator.Say("Из-за занавески доносится хрипящий шепот");
            yield return villager.Say("Убирайся!");
            yield return narrator.Say("Щель в двери внезапно распахивается — не рукой, а порывом ветра. На пороге появляется тряпичная кукла, висящая на верёвке. Её голова крутится вслед герою, пока он отходит.");
            iteration++;
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
        }

        public IEnumerator Dialogue2()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Михаил стучит в дверь с облупленной краской. Изнутри слышны шаги, но дверь не открывается.");
            mc.Hightlight();
            yield return mc.Say("Я ищу девушку. Бледная, в белом. Была здесь?");
            mc.UnHightlight();
            yield return narrator.Say("Щель в двери расширяется на ладонь. Через неё вылетает горсть пепла, оседая на плечах Михаила.");
            yield return villager.Say("Пепел не лжёт.{a} Все, кто спрашивал... там.");
            yield return narrator.Say("Щель захлопывается. За дверью — звук скребущих ногтей по дереву, будто кто-то ползёт вглубь дома.");
            iteration++;
            mc.Hide();
            CloseDialogueEvent();
        }

        public IEnumerator Dialogue3()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Михаил замечает окно на втором этаже, где трясётся грязное стекло. В темноте мелькает силуэт старика, сидящего за столом.");
            mc.Hightlight();
            yield return mc.Say("Где она? Куда ушла?");
            mc.UnHightlight();
            yield return narrator.Say("Старик не отвечает. {a}Вместо этого он поднимает руку с зажатым в пальцах листом бумаги. На нём каракули: «Под землёй». Внезапно лист рвётся, и обрывки падают на голову Михаилу.");
            yield return narrator.Say("Окно захлопывается. Стекло трескается, образуя узор, похожий на корни дерева.");
            mc.Hide();
            iteration++;
            gameObject.SetActive(false);
            CloseDialogueEvent();
        }
    }
}