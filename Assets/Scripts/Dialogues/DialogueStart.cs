using CHARACTER;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DIALOGUES
{
    public class DialogueStart : MonoBehaviour
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
            DialogueSystem.instance.CloseDialogue();
        }
    }
}