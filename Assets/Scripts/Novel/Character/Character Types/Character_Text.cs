
using UnityEngine;

namespace CHARACTER
{
    public class Character_Text : Character
    {
        public Character_Text(string name, CharacterConfigData config) : base(name, config, prefab: null)
        {
            Debug.Log($"Created text character : '{name}'");
        }
    }
}