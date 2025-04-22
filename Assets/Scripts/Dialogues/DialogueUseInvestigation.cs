using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueUseInvestigation : MonoBehaviour, IDialogue
    {
        public AudioSource source;
        public GameObject ToDisable;
        public GameObject ToEnable;
        public void StartDialogue()
        {
            source.Play();
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            // Активация режима расследования
            yield return narrator.Say("ВКЛ режим расследования");
            yield return narrator.Say("Внезапная волна боли сжимает виски, в ушах звучит далёкий колокольный звон. Воздух становится густым, затрудняя дыхание.");

// Реакция Михаила на боль
            MC.UnHightlight();
            yield return narrator.Say("Хватается за голову, опираясь о стену");
            MC.Hightlight();
            yield return MC.Say("Ааа... Nom de Dieu... (Ради Бога...)");

// Последействие
            yield return narrator.Say("Постепенно боль отступает, оставляя после себя странную обострённость восприятия. Тени в углах кажутся чуть гуще, чем должны быть.");

// Обнаружение следа
            MC.UnHightlight();
            yield return narrator.Say("Замечает едва различимый след на полу");
            MC.Hightlight();
            yield return MC.Say("Qu'est-ce que c'est? (Что это?)");

// Исследование следа
            MC.UnHightlight();
            yield return narrator.Say("Присаживается на корточки, всматриваясь");
            MC.Hightlight();
            yield return MC.Say("След... Но не от полицейских сапог.");

// Финальная реплика
            MC.UnHightlight();
            yield return narrator.Say("Выпрямляется, бросая взгляд в тёмный дверной проём");
            MC.Hightlight();
            yield return MC.Say("Что же здесь на самом деле произошло?");
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
            ToEnable.SetActive(true);
            ToDisable.SetActive(false);
        }
    }
}