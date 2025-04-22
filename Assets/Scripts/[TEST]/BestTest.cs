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
            Manager.AddMonsterToList("Ползучий Шёпот(«То, что не следует называть»)");
            Manager.AddMonsterToList("ОЗЛОБЛЕННЫЙ");
            
        }
    }
}