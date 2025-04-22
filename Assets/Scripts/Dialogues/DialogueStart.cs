using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueStart : MonoBehaviour, IDialogue
    {
        public GameObject CoachNPC;
        public void StartDialogue()
        {
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
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
            yield return narrator.Say("Карета останавливается. Лошадь фыркает, разгоняя пар в холодном воздухе. Где-то вдали воет ветер.");
            yield return narrator.Say("Узкие переулки Петербурга окутаны густым туманом. Фонари мерцают, словно насмехаясь над попытками света пробиться сквозь тьму. Воздух пропитан сыростью и тревогой.");
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Кучер снимает потрёпанную шляпу, уголки губ дрогнули в едва заметной насмешке.");
            Coach.Hightlight();
            yield return Coach.Say("Ваше благородие, прибыли. Ждать прикажете или сразу бежать за гробовщиком?");
            Coach.Hide();
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Ступни сапог глухо стучат по булыжнику, взгляд скользит по слепым окнам и покосившимся карнизам.");
            MC.Hightlight();
            yield return MC.Say("Жди. Если через полчаса не вернусь — скачи в участок. И ни слова лишнего.");
            MC.Hide();
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Сухой кашель вырывается из груди, пальцы нервно теребят вожжи.");
            Coach.Hightlight();
            yield return Coach.Say("Как скажете... Только смотрите — здесь тени не любят чужих.");
            Coach.Hide();
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Резкий поворот головы, тени переулка на мгновение замерли, будто втянутые внезапной напряжённостью его движений.");
            MC.Hightlight();
            yield return MC.Say("Тени? Спасибо за совет.");
            MC.Hide();
            DialogueSystem.instance.CloseDialogue();
            CoachNPC.SetActive(false);
        }
    }
}