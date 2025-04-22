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
            // Прибытие в хутор
            yield return narrator.Say("Карета останавливается на въезде в хутор. Серые избы стоят с закрытыми ставнями, будто слепые. Ни собак, ни голосов — только ветер шуршит сухой травой у дороги.");

// Кучер отказывается ехать дальше
            Coach.Show();
            Coach.UnHightlight();
            yield return narrator.Say("Оборачивается, бледнее обычного");
            Coach.Hightlight();
            yield return Coach.Say("Ваше благородие... дальше — сами. Здесь чужих не жалуют.");
            Coach.Hide();
// Михаил спрашивает о Фёдоре
            MC.Show();
            MC.UnHightlight();
            yield return narrator.Say("Выходит, поправляя перчатки");
            MC.Hightlight();
            yield return MC.Say("Где здесь найти Фёдора-колесника?");
            MC.Hide();
// Ответ хутора
            yield return narrator.Say("В ответ — тишина. Лишь в дальней избе хлопает дверь, будто кто-то поспешно захлопнул её.");

// Завершение сцены
            
            
            DialogueSystem.instance.CloseDialogue();
        }
    }
}
