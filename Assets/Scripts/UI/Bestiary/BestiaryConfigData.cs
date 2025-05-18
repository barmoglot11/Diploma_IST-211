using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    [System.Serializable]

    public class BestiaryConfigData
    {
        public string MonsterName;
        public Material MonsterMaterial;
        [TextArea(minLines: 5, maxLines: 20)] public string MonsterDesc;

        public BestiaryConfigData Copy()
        {
            var result = new BestiaryConfigData
            {
                MonsterName = MonsterName,
                MonsterMaterial = MonsterMaterial,
                MonsterDesc = MonsterDesc,
            };
            return result;
        }

        public static BestiaryConfigData Default
        {
            get
            {
                BestiaryConfigData result = new BestiaryConfigData();

                result.MonsterName = "Неизвестно";
                result.MonsterDesc = "Слабости: неизвестно" +
                                     "\n\nнеизвестно" +
                                     "\n\nнеизвестно" +
                                     "\n\nТекст... Текст... "
                    ;
                result.MonsterMaterial = Resources.Load<Material>("Monster None");
                return result;
            }
        }
    }
}