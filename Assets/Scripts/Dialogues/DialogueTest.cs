using System;
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
            Character_Text MC = CharacterManager.Instance.CreateCharacter("Главный герой") as Character_Text;
            Character_Text Coach = CharacterManager.Instance.CreateCharacter("Кучер") as Character_Text;
            
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
            Character_Text MC = CharacterManager.Instance.GetCharacter("Главный герой") as Character_Text;
            MC.Say("jhlksdgdhfjkffhg");
            yield return  new WaitForSeconds(1.5f);
            Character_Text Coach = CharacterManager.Instance.GetCharacter("Кучер") as Character_Text;
            Coach.Say("ххххххх");
            yield return  new WaitForSeconds(1.5f);
            MC.Say("завщппвап");
            yield return null;
        }
        
        public IEnumerator Test2()
        {
            Character_Text MC = CharacterManager.Instance.GetCharacter("Главный герой") as Character_Text;
            yield return MC.Say("ghjkhdghdgdh");
            Character_Text Coach = CharacterManager.Instance.GetCharacter("Кучер") as Character_Text;
            yield return Coach.Say("ххххххх");;
            yield return MC.Say("завщппвап");;
        }
    }
}
