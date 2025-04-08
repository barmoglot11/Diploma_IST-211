using CHARACTER;
using DIALOGUE;
using System.Collections;
using UnityEngine;

namespace Dialogues
{
    public class DialogueLockedDoor : MonoBehaviour
    {
        public void StartDialogue()
        {
            //Вкл черный экран
            //вкл объекты
            //выкл черный экран
            //вкл диалог
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
            yield return MC.Say("Bon sang! (Черт возьми!) Заперто.");
            yield return MC.Say("И как мне попасть во внутрь?");
            yield return MC.Say("Стоп, кажется что-то блеснуло рядом...");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}