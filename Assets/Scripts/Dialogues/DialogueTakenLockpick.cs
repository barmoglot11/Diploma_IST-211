using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using UnityEngine;


namespace DIALOGUES
{
    public class DialogueTakenLockpick : MonoBehaviour, IDialogue
    {
        public GameObject footstepsToEnable;
        public GameObject footstepsToDisable;
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            Character_Text narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            Character_Sprite mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);

            
            yield return narrator.Say("Следы вели к шкафу — высокому, с резными узорами в виде переплетённых корней. " +
                                      "Дверцы скрипнули, будто протестуя против чужих рук. " +
                                      "\n{a}Внутри — полка, покрытая пылью, и на ней — женская шаль с вышитыми воронами. " +
                                      "Под ней пряталась заколка: тонкая, изящная, с застывшими каплями вещества, похожего на воск.");
            yield return narrator.Say("Михаил взял её. Пальцы ощутили неожиданную прохладу — будто металл хранил в себе зиму. " +
                                      "Вещество на ней было плотным, с маслянистым блеском. " +
                                      "Он провёл ногтем — капля не оторвалась, а будто вздёрнулась , потянувшись к коже.");
            mc.Show();
            yield return mc.Say("Что ты такое?");
            mc.UnHightlight();
            
            yield return narrator.Say("Запах усилился — сладкий запах тления застлал комнату." +
                                      "\nГде-то за спиной скрипнула половица. " +
                                      "{a}Он обернулся — пусто. " +
                                      "{a}Только тень от шкафа медленно поползла в сторону, будто её отодвинул невидимый шаг.");

            yield return narrator.Say("Он спрятал заколку в карман, но ощущение холода осталось.");
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            DialogueSystem.instance.CloseDialogue();
            footstepsToEnable?.SetActive(true);
            footstepsToDisable?.SetActive(false);
        }
    }
}