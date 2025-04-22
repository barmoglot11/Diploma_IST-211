using CHARACTER;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _TEST_
{
    public class DialogueTest : MonoBehaviour
    {
        public void StartDialogue()
        {
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            MC.Show();
            MC.Flip(immediate:true);
            MC.SetPosition(Vector2.zero);
            yield return MC.Say("ААААААААААА");
            
            yield return Coach.Say("ххххххх");
            
            yield return MC.Say("завщппвап");
            
            Choice();
        }

        public void Choice()
        {
            List<string> choices = new List<string>() {"fgfgfg", "hdrgfdggrd"};
            ChoiceManager.Instance.SetupChoices(choices);
            ChoiceManager.Instance.SetupActions(new List<UnityAction>() {Choice1, Choice2 });
            ChoiceManager.Instance.ShowChoices();
        }

        public void Choice1()
        {
            StartCoroutine(Test1());
        }
        public void Choice2()
        {
            StartCoroutine(Test2());
        }
        public IEnumerator Test1()
        {
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            MC.Say("jhlksdgdhfjkffhg");
            yield return  new WaitForSeconds(1.5f);
            Coach.Say("ххххххх");
            yield return  new WaitForSeconds(1.5f);
            MC.Say("завщппвап");
            yield return null;
            DialogueSystem.instance.CloseDialogue();
        }
        
        public IEnumerator Test2()
        {
            Character_Sprite MC = CharacterManager.Instance.GetCharacter("Главный герой", createIfDoesNotExist:true) as Character_Sprite;
            Character_Sprite Coach = CharacterManager.Instance.GetCharacter("Кучер", createIfDoesNotExist:true) as Character_Sprite;
            yield return MC.Say("ghjkhdghdgdh");
            yield return Coach.Say("ххххххх");
            yield return MC.Say("завщппвап");
            DialogueSystem.instance.CloseDialogue();
        }
    }
}
