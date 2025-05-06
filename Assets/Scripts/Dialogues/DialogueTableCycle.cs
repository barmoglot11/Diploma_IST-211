using System.Collections;
using CHARACTER;
using DIALOGUE;
using Interfaces;
using UnityEngine;

namespace Dialogues
{
    public class DialogueTableCycle : MonoBehaviour, IDialogue
    {
        public void StartDialogue()
        {
            DialogueSystem.instance.SetupCloseEvent(CloseDialogueEvent);
            StartCoroutine(Dialogue());
        }
        
        public IEnumerator Dialogue()
        {
            var narrator = CharacterManager.Instance.GetCharacter("Narrator", createIfDoesNotExist:true) as Character_Text;
            var mc = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            if (mc is { isVisible: true })
                mc.Hide();
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.Show();
            mc.UnHightlight();
            yield return narrator.Say("Пыльный стол с выщербленной столешницей. На поверхности — лишь круги от стаканов да царапины, оставленные временем.");
            mc.Hightlight();
            mc.Say("Пусто...");
            mc.UnHightlight();
            yield return narrator.Say("В углу стола застревает заноза. Капля крови падает на дерево, но след мгновенно исчезает — будто дом впитал его.");
            mc.Hightlight();
            mc.Say("Чёрт...");
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
        }
    }
}