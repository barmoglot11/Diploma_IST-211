

using UnityEngine;
using CHARACTER;
using TMPro;

namespace DIALOGUE 
{
    [CreateAssetMenu(fileName = "Dialogue System Configuration", menuName = "Dialogue System/Dialogue Configuration Asset")]
    public class DialogueSystemConfigurationSO : ScriptableObject
    {
        public CharacterConfigSO characterConfigAsset;

        public Color defaultTextColor = Color.white;
        public TMP_FontAsset defaultFont;
    }
}