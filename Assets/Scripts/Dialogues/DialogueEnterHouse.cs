using CHARACTER;
using DIALOGUE;
using System.Collections;
using UnityEngine;

namespace Dialogues
{
    public class DialogueEnterHouse : MonoBehaviour
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
            yield return MC.Say("Все следы уже подчистила милиция...");
            yield return MC.Say("Но что-нибудь должно было остаться. Надо осмотреться.");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}