using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using INVESTIGATION;
using UnityEngine;
using MANAGER;

namespace DIALOGUES
{
    public class DialogueUseInvestigation : MonoBehaviour, IDialogue
    {
        public AudioSource source;
        public GameObject ToDisable;
        public GameObject footsteps;
        public void StartDialogue()
        {
            source.Play();
            AnimationManager.Instance.AgonyCall();
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }

        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc.isFacingLeft)
                mc.Flip(immediate:true);
            mc.SetPosition(Vector2.zero);
            mc.SetSprite(mc.GetSprite($"{mc.name}-Angry"));
            mc.displayName = "Михаил";
            mc.Show();
            mc.UnHightlight();
            // Активация режима расследования
            yield return narrator.Say("Михаил приближается к зеркалу, шаги замедляются. {a}Воздух сгущается, как кисель.");
// Реакция Михаила на боль
            yield return narrator.Say("Хватается за голову, опираясь о стену");
            mc.Hightlight();
            yield return mc.Say("Ааа... Nom de Dieu... (Ради Бога...)");
            mc.UnHightlight();
// Последействие
            yield return narrator.Say("Зеркало дрожит, будто вода в проруби. Голоса нарастают — шипение, рыдания, крики, будто кто-то давится в темноте.");
            

            yield return narrator.Say("Он сжимает виски, суставы побелели. В глазах темнеет, но не отпускает взгляд от отражения");
            yield return narrator.Say("Боль в голове разливалась, как тёмная вода из проруби, но вдруг схлынула, оставив лишь холодную пустоту под черепом. " +
                                      "{a}Михаил сделал глубокий вдох — воздух пах плесенью и воском, но уже не резал глаза шипами.");
            
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            
            mc.Hide();
            
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            ToDisable?.SetActive(false);
            footsteps?.SetActive(true);
        }
    }
}