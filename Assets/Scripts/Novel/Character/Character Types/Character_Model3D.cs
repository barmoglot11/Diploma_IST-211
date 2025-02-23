
using UnityEngine;

namespace CHARACTER
{
    public class Character_Model3D : Character
    {
        public Character_Model3D(string name, CharacterConfigData config, GameObject prefab, string rootCharacterFolder) : base(name, config, prefab)
        {
            Debug.Log($"Created 3D character : '{name}'");
        }
    }
}