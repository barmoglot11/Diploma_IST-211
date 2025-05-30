using CHARACTER;
using DIALOGUE;
using System.Collections;
using Interfaces;
using INVESTIGATION;
using UnityEngine;

namespace DIALOGUES
{
    public class DialogueMirror : MonoBehaviour, IDialogue
    {
        public GameObject camera;
        public GameObject cameraPosition;
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
            mc.displayName = "Михаил";
            mc.Show();
            mc.SetSprite(mc.GetSprite($"{mc.name}-Shocked"));
            mc.Hightlight();
            yield return mc.Say("Кто ты...?");
            mc.SetSprite(mc.GetSprite($"{mc.name}-Default"));
            mc.UnHightlight();
            camera.transform.position = cameraPosition.transform.position;
            camera.transform.rotation = cameraPosition.transform.rotation;
            yield return narrator.Say("В зеркале, сквозь дрожь стекла, на него смотрит девушка. Не моргая. Не дыша. Её глаза — будто два окна в зимнюю ночь.");
            MirrorController.Instance.SetMaterial(0);
            yield return narrator.Say("Её рука дрожит. Внезапно палец резко указывает на шкатулку в углу. {a}Михаил оборачивается, взгляд скользит по тени на стене, которая дрожит, будто живая.");
            
            mc.Hightlight();
            yield return mc.Say("Что ты хочешь показать? {a}Что в ней?");
            mc.UnHightlight();
            mc.Hide();
            CloseDialogueEvent();
        }

        public void CloseDialogueEvent()
        {
            DialogueSystem.instance.CloseDialogue();
            CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist: true).Hide();
            MirrorController.Instance.SetMaterial();
        }
    }
}