using CHARACTER;
using DIALOGUE;
using System.Collections;
using _TEST_;
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
                    StartCoroutine(Dialogue());
                    questHandle.questStage++;
                    questHandle.OnDisable();
                    break;
                case 2:
                    StartCoroutine(Dialogue2());
                    questHandle.questStage++;
                    questHandle.OnDisable();
                    break;
                case 3:
                    StartCoroutine(Dialogue3());
                    questHandle.questStage++;
                    questHandle.OnDisable();
                    break;
                default:
                    break;
            }
            
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
            MC.Show();
            MC.UnHightlight();
            // Первая изба
            yield return narrator.Say("Михаил стучит в дверь с облупившейся краской. Молчание. Затем из-за ставни женский голос:");
            yield return villager.Say("Уходите! Ничем не поможем...");
            yield return narrator.Say("Стучит сильнее");
            MC.Hightlight();
            yield return MC.Say("Я не нищий. Ищу Фёдора.");
            MC.UnHightlight();
            yield return narrator.Say("Щелчок засова. Голос теперь чётче:");
            yield return villager.Say("В церквушке спросите... Только там теперь один дьячок.");
            iteration++;
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
        }

        public IEnumerator Dialogue2()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Старик за дверью кашляет, но не открывает:");
            yield return villager.Say("Федька? Давно не видели... В лесу, сказывают, следы его теряются.");
            MC.Hightlight();
            yield return MC.Say("Чей лес?");
            MC.UnHightlight();
            yield return narrator.Say("Долгая пауза. Потом скрип кровати — старик отошёл от двери.");
            iteration++;
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
        }

        public IEnumerator Dialogue3()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Text villager = CharacterManager.Instance.GetCharacter("Деревенский житель", createIfDoesNotExist:true) as Character_Text;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Молодая мать шипит через щель:");
            yield return villager.Say("Не мучайте людей! Ступайте к отцу Никифору... если осмелитесь.");
            yield return narrator.Say("Ребёнок за стеной начинает плакать. Дверь глухо стучит косяком — её придержали рукой.");
            MC.Hide();
            iteration++;
            DialogueSystem.instance.CloseDialogue();
            gameObject.SetActive(false);
        }
    }
}