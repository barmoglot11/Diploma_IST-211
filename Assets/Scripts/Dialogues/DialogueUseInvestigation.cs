using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using INVESTIGATION;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueUseInvestigation : MonoBehaviour, IDialogue
    {
        public AudioSource source;
        public GameObject ToDisable;
        public TeleportPoint teleport;
        public void StartDialogue()
        {
            source.Play();
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.SetSprite(mc.GetSprite("Angry"));
            mc.Show();
            
            // Активация режима расследования
            yield return narrator.Say("Внезапная волна боли сжимает виски, в ушах звучит далёкий колокольный звон. Воздух становится густым, затрудняя дыхание.");

// Реакция Михаила на боль
            mc.UnHightlight();
            yield return narrator.Say("Хватается за голову, опираясь о стену");
            mc.Hightlight();
            yield return mc.Say("Ааа... Nom de Dieu... (Ради Бога...)");
            mc.UnHightlight();
// Последействие
            yield return narrator.Say("Постепенно боль отступает, оставляя после себя странную обострённость восприятия. Тени в углах кажутся чуть гуще, чем должны быть.");

// Обнаружение следа
            
            mc.SetSprite(mc.GetSprite("Shocked"));
            yield return narrator.Say("Замечает едва различимый след на полу");
            mc.Hightlight();
            yield return mc.Say("Qu'est-ce que c'est? (Что это?)");

// Исследование следа
            mc.UnHightlight();
            mc.SetSprite(mc.GetSprite("Default"));
            yield return narrator.Say("Присаживается на корточки, всматриваясь");
            mc.Hightlight();
            yield return mc.Say("След... Но не от полицейских сапог.");

// Финальная реплика
            mc.UnHightlight();
            yield return narrator.Say("Выпрямляется, бросая взгляд в тёмный дверной проём");
            mc.Hightlight();
            yield return mc.Say("Что же здесь на самом деле произошло?");
            mc.Hide();
            
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
            teleport.ClearTeleportEvents();
            ToDisable?.SetActive(false);
        }
    }
}