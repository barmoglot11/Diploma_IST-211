using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueUseInvestigation : MonoBehaviour, IDialogue
    {
        public AudioSource source;
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
            MC.UnHightlight();
            yield return narrator.Say("Головная боль, сравнимая с ударами самым тяжелым молотом, растекается по вискам, стремясь заполнить всю голову");
            yield return narrator.Say("Странные звуки, крики и перешептывания окружают Михаила. Они часто меняются местами - то близко, то далеко");
            yield return narrator.Say("Спустя некоторое время боль сходит, и появляется возможность осмотреться вокруг");
            MC.Hightlight();
            yield return MC.Say("Ааа..Голова...");
            yield return MC.Say("Qu'est-ce que le (Что здесь)...");
            yield return MC.Say("Следы?");
            yield return MC.Say("Что здесь происходит?");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}