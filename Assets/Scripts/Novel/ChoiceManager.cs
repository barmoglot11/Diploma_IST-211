using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DIALOGUE
{
    public class ChoiceManager : MonoBehaviour
    {
        public List<Button> choicesContainer;

        public void SetupChoices(List<string> choices)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                choicesContainer[i].GetComponent<TextMeshProUGUI>().text = choices[i];
            }
        }
        
        public void ShowChoices()
        {
            
        }

        public void HideChoices()
        {
            foreach (var button in choicesContainer)
            {
                button.onClick.RemoveAllListeners();
            }
            
            
        }
    }
}