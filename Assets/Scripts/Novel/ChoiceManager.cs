using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DIALOGUE
{
    public class ChoiceManager : MonoBehaviour
    {
        public static ChoiceManager Instance;
        public List<Button> choicesContainer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetupChoices(List<string> choices)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                choicesContainer[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            }
        }

        public void SetupActions(List<UnityAction> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            {
                choicesContainer[i].onClick.AddListener(actions[i]);
                choicesContainer[i].onClick.AddListener(HideChoices);
            }
        }
        public void ShowChoices()
        {
            gameObject.SetActive(true);
        }

        public void HideChoices()
        {
            foreach (var button in choicesContainer)
            {
                button.onClick.RemoveAllListeners();
            }
            
            gameObject.SetActive(false);
        }
    }
}