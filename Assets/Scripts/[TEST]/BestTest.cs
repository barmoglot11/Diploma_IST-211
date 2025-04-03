using System;
using UI;
using UnityEngine;

namespace _TEST_
{
    public class BestTest : MonoBehaviour
    {
        private BestiaryManager Manager => BestiaryManager.Instance;

        private void Start()
        {
            Manager.AddMonsterToList("ОЗЛОБЛЕННЫЙ ЛЕШИЙ");
            Manager.AddMonsterToList("ОЗЛОБЛЕННЫЙ");
            
        }
    }
}