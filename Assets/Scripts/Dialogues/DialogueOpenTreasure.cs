using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueOpenTreasure : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            MC.Show();
            yield return MC.Say("Письмо...");
            yield return MC.Say("Вот это уже зацепка.");
            yield return MC.Say("Надо съездить в этот хутор и найти отправителя.");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}