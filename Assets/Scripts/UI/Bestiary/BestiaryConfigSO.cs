using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [CreateAssetMenu(fileName = "BestiaryConfig", menuName = "Bestiary", order = 0)]
    public class BestiaryConfigSO : ScriptableObject
    {
        public List<BestiaryConfigData> bestiaryConfigData;
        
        public BestiaryConfigData GetConfig(string monsterName)
        {
            monsterName = monsterName.ToLower();

            foreach (var data in bestiaryConfigData)
            {
                if(string.Equals(monsterName, data.MonsterName.ToLower()))
                    return data.Copy();
            }

            return BestiaryConfigData.Default;
        }
    }
}