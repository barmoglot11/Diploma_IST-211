using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace DIALOGUE
{
    public class AnimationCharacterController : MonoBehaviour
    {
        public static AnimationCharacterController Instance;
        public SerializedDictionary<string, Animator> characterAnimator;

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

        public void UseAnimation(string charName, string animName)
        {
            characterAnimator[charName]?.Play("Base Layer." + animName);
        }
    }
}