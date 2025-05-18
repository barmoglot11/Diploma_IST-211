using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueStartVillage : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            /*MC.Show();
            if (MC.isFacingLeft)
                MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);*/
            if (Coach is { isFacingLeft: true })
                Coach.Flip(immediate:true);
            Coach.SetPosition(Vector2.zero);
            // Прибытие в хутор
            yield return narrator.Say("Карета останавливается на въезде в хутор. Серые избы стоят с закрытыми ставнями, будто слепые. " +
                                      "{a}Ни собак, ни голосов — только ветер шуршит сухой травой у дороги.");

            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Кучер оборачивается, лицо его бледнее обычного");
            Coach.Hightlight();
            yield return Coach.Say("Ваше благородие... дальше — сами. Здесь чужих не жалуют.");
            Coach.Hide();
            
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
        }
    }
}
