using CHARACTER;
using DIALOGUE;
using System.Collections;
using UnityEngine;

namespace Dialogues
{
    public class DialogueUseInvestigation : MonoBehaviour
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            yield return MC.Say("Ааа..Голова...");
            yield return MC.Say("Qu'est-ce que le (Что здесь)...");
            yield return MC.Say("Следы?");
            yield return MC.Say("Что здесь происходит?");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}