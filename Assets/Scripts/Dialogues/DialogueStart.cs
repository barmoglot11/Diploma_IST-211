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
            yield return narrator.Say(
                "Карета останавливается. Лошадь фыркает, разгоняя пар в холодном воздухе. Где-то вдали воет ветер.");
            yield return narrator.Say(
                "Узкие переулки Петербурга окутаны густым туманом. Фонари мерцают, словно насмехаясь над попытками света пробиться сквозь тьму. Воздух пропитан сыростью и тревогой.");
            coach.Show();
            coach.TransitionSprite(coach.GetSprite($"{coach.name}-Question"));
            coach.UnHightlight();
            yield return narrator.Say("Кучер снимает потрёпанную шляпу, уголки губ дрогнули в едва заметной насмешке.");
            coach.Hightlight();
            yield return coach.Say("Ваше благородие, прибыли. Ждать прикажете или сразу бежать за гробовщиком?");
            coach.Hide();
            coach.SetSprite(coach.GetSprite($"{coach.name}-Default"));
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say(
                "Ступни сапог глухо стучат по булыжнику, взгляд скользит по слепым окнам и покосившимся карнизам.");
            mc.Hightlight();
            yield return mc.Say("Жди. Если через полчаса не вернусь — скачи в участок. И ни слова лишнего.");
            mc.Hide();
            coach.Show();
            coach.UnHightlight();
            yield return narrator.Say("Сухой кашель вырывается из груди, пальцы нервно теребят вожжи.");
            coach.Hightlight();
            yield return coach.Say("Как скажете... Только смотрите — здесь тени не любят чужих.");
            coach.Hide();
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say(
                "Резкий поворот головы, тени переулка на мгновение замерли, будто втянутые внезапной напряжённостью его движений.");
            mc.Hightlight();
            yield return mc.Say("Тени? Спасибо за совет.");
            mc.Hide();
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