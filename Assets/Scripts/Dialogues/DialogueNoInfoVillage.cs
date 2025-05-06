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
            yield return narrator.Say("Михаил стучит в дверь с облупившейся краской. Молчание. Затем из-за ставни женский голос:");
            yield return villager.Say("Уходите! Ничем не поможем...");
            yield return narrator.Say("Стучит сильнее");
            mc.SetSprite(mc.GetSprite($"{mc.name}-Shocked"));
            mc.Hightlight();
            yield return mc.Say("Я не нищий. Ищу Фёдора.");
            mc.UnHightlight();
            yield return narrator.Say("Щелчок засова. Голос теперь чётче:");
            yield return villager.Say("В церквушке спросите... Только там теперь один дьячок.");
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
            yield return narrator.Say("Старик за дверью кашляет, но не открывает:");
            yield return villager.Say("Федька? Давно не видели... В лесу, сказывают, следы его теряются.");
            mc.Hightlight();
            yield return mc.Say("Чей лес?");
            mc.UnHightlight();
            yield return narrator.Say("Долгая пауза. Потом скрип кровати — старик отошёл от двери.");
            iteration++;
            mc.Hide();
            CloseDialogueEvent();
        }

        public IEnumerator Dialogue3()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Молодая мать шипит через щель:");
            yield return villager.Say("Не мучайте людей! Ступайте к отцу Никифору... если осмелитесь.");
            yield return narrator.Say("Ребёнок за стеной начинает плакать. Дверь глухо стучит косяком — её придержали рукой.");
            mc.Hide();
            iteration++;
            gameObject.SetActive(false);
            CloseDialogueEvent();
        }
    }
}