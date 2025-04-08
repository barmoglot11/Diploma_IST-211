using CHARACTER;
using DIALOGUE;
using System.Collections;
using UnityEngine;

namespace Dialogues
{
    public class DialogueMoveToVillage : MonoBehaviour
    {
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        IEnumerator Dialogue()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Dialogue);
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
            
            MC.Show();
            yield return MC.Say("Serviteur (Слуга), надо ехать в место, описанное в письме. Знаешь где оно?");
            MC.Hide();
            Coach.Show();
            yield return Coach.Say("Как жеж, знаю. Быстро доедем, господин.");
            yield return Coach.Say("Садитесь.");
            Coach.Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}