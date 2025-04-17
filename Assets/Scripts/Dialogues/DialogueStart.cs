using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueStart : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            //Вкл черный экран
            //вкл объекты
            //выкл черный экран
            //вкл диалог
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (Coach.isFacingLeft)
                Coach.Flip(immediate:true);
            Coach.SetPosition(Vector2.zero);
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            
            Coach.Show();
            yield return Coach.Say("Ваше благородье, доехали. Мне вас здесь ждать или как?");
            Coach.Hide();
            MC.Show();
            yield return MC.Say("Здесь жди, я здесь все равно не надолго.");
            MC.Hide();
            Coach.Show();
            yield return Coach.Say("Ну, как изволите.");
            Coach.Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}