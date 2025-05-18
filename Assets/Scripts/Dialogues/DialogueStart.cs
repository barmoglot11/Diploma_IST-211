using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace DIALOGUES
{
    public class DialogueStart : MonoBehaviour, IDialogue
    {
        public GameObject coachNpc;

        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator =
                CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist: true) as Character_Text;
            Character_Sprite mc =
                CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true) as Character_Sprite;
            Character_Sprite coach =
                CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist: true) as Character_Sprite;
            if (coach is { isVisible: true })
                coach.Hide();
            if (mc is { isVisible: true })
                mc.Hide();
            if (coach is { isFacingLeft: true })
                coach.Flip(immediate: true);
            coach.SetPosition(Vector2.zero);
            if (mc is { isFacingLeft: true })
                mc.Flip(immediate: true);
            mc.SetPosition(Vector2.zero);
            yield return narrator.Say("Город дышал холодом. Каменные фасады домов, выстроенные в шахматном порядке, будто сжимались от сырости, просачивающейся сквозь туман." +
                                      "{a}Фонари на улицах мерцали, как будто боялись разогнать тень, которая цеплялась за каждую булыжную плиту мостовой."
                                      );
            yield return narrator.Say("Особняк стоял тихо, будто выдохнул последние остатки жизни наружу ещё до прибытия Михаила.");
            
            coach.Show();
            coach.UnHightlight();
            yield return narrator.Say("Кучер останавливает экипаж, не оборачиваясь. Лошадь фыркает, будто чувствуя неладное.");
            coach.Hightlight();
            yield return coach.Say("Дальше не поеду. Тут... {a}не место для живых.");
            coach.Hide();

            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Михаил выходит, с силой захлопнув дверцу. Его сапоги глухо стучат по камням двора.");
            mc.Hightlight();
            yield return mc.Say("Я заплатил за дорогу. Не за советы.");
            mc.Hide();
            
            coach.Show();
            coach.UnHightlight();
            yield return narrator.Say("Кучер сжимает вожжи, костяшки пальцев белеют. {a}Он шепчет, не глядя на героя.");
            coach.Hightlight();
            yield return coach.Say("Забирай свою монету. Только... не оставайся надолго. Дом этот... он не спит.");
            coach.Hide();

            yield return narrator.Say("Кучер заходит в карету, чтобы уснуть и забыть об этом месте надолго.");
            
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
            coachNpc.SetActive(false);
        }
    }
}