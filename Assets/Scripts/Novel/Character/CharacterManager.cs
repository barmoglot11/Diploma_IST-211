using DIALOGUE;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace CHARACTER
{
    public class CharacterManager: MonoBehaviour
    {
        #region Singleton
        public static CharacterManager Instance { get; private set; }
        #endregion
        
        #region Поля и Атрибуты
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigAsset;

        private const string CHARACTER_CASTING_ID = " as ";
        private const string CHARACTER_NAME_ID = "<charname>";
        public string characterRootPathFormat => $"Characters/{CHARACTER_NAME_ID}";
        public string characterPrefabNameFormat => $"Character - [{CHARACTER_NAME_ID}]";
        public string charaterPrefabPathFormat => $"{characterRootPathFormat}/{characterPrefabNameFormat}";

        [SerializeField] private RectTransform _characterPanel = null;
        [SerializeField] private RectTransform _characterPanel_live2D = null;
        [SerializeField] private RectTransform _characterPanel_model3D = null;
        public RectTransform characterPanel => _characterPanel;
        public RectTransform characterPanelLive2D => _characterPanel_live2D;
        public RectTransform characterPanelModel3D => _characterPanel_model3D;
        #endregion

        #region Публичные методы

        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
                return characters[characterName.ToLower()];
            else if (createIfDoesNotExist)
                return CreateCharacter(characterName);

            return null;

        }
        
        public CharacterConfigData GetCharacterConfig(string characterName)
        {
            return config.GetConfig(characterName);
        }

        public Character CreateCharacter(string characterName, bool revealAfterCreation = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                Debug.LogWarning($"A character called '{characterName}' already exist. Did not create a character.");
                return null;
            }

            CHARACTER_INFO info = GetCharacterInfo(characterName);

            Character character = CreateCharacterFromInfo(info);

            characters.Add(info.name.ToLower(), character);

            if (revealAfterCreation)
                character.Show();

            return character;
        }

        public string FormatCharacterPath(string path, string characterName) => path.Replace(CHARACTER_NAME_ID, characterName);
        
        public void SortCharcters()
        {
            List<Character> activeCharacters = characters.Values
                .Where(c => c.root.gameObject.activeInHierarchy && c.isVisible)
                .ToList();
            List<Character> inactiveCharacters = characters.Values
                .Except(activeCharacters)
                .ToList();

            activeCharacters.Sort((a,b) => a.priority.CompareTo(b.priority));
            activeCharacters.Concat(inactiveCharacters);

            SortCharacters(activeCharacters);
        }

        public void SortCharacters(string[] characterNames)
        {
            List<Character> sortedCharacters = new List<Character>();

            sortedCharacters = characterNames
                .Select(name => GetCharacter(name))
                .Where(character => character != null)
                .ToList();

            List<Character> remainingCharacters = characters.Values
                .Except(sortedCharacters)
                .OrderBy(character => character.priority)
                .ToList();

            sortedCharacters.Reverse();

            int startingPriority = remainingCharacters.Count > 0 ? remainingCharacters.Max(c=> c.priority) : 0;
            for(int i = 0; i<sortedCharacters.Count; i++)
            {
                Character character = sortedCharacters[i];
                character.SetPriority(startingPriority + i + 1, autoSortCharactersOnUI: false);

            }

            List<Character> allCharacters = remainingCharacters.Concat(sortedCharacters).ToList();
            SortCharacters(allCharacters);
        }

        #endregion

        #region Приватные методы
        private void Awake()
        {
            Instance = this;
        }
        
        private void SortCharacters(List<Character> characterSortingOrder)
        {
            int i = 0;
            foreach(Character character in characterSortingOrder)
            {
                character.root.SetSiblingIndex(i++);
                character.OnSort(i);
            }
        }
        
        private Character CreateCharacterFromInfo(CHARACTER_INFO info)
        {
            CharacterConfigData config = info.config;

            switch (info.config.characterType)
            {
                case Character.CharacterType.Text:
                    return new Character_Text(info.name, config);

                case Character.CharacterType.Sprite:
                case Character.CharacterType.SpriteSheet:
                    return new Character_Sprite(info.name, config, info.prefab, info.rootCharacterFolder);

                case Character.CharacterType.Model3D:
                    return new Character_Model3D(info.name, config, info.prefab, info.rootCharacterFolder);

                default:
                    return null;
            }
        }
        
        private CHARACTER_INFO GetCharacterInfo(string characterName)
        {
            CHARACTER_INFO result = new CHARACTER_INFO();

            string[] nameData = characterName.Split(CHARACTER_CASTING_ID, System.StringSplitOptions.RemoveEmptyEntries);

            result.name = nameData[0];

            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;

            result.config= config.GetConfig(result.castingName);

            result.prefab = GetPrefabForCharacter(result.castingName);

            result.rootCharacterFolder = FormatCharacterPath(characterRootPathFormat, result.castingName);

            return result;
        }

        private GameObject GetPrefabForCharacter(string characterName)
        {
            string prefabPath = FormatCharacterPath(charaterPrefabPathFormat, characterName);
            return Resources.Load<GameObject>(prefabPath);
        }
        
        #endregion
        
        #region Вспомогательный класс

        private class CHARACTER_INFO
        {
            public string name = "";
            public string castingName = "";
            public string rootCharacterFolder = "";
            public CharacterConfigData config = null;
            public GameObject prefab = null;
        }

        #endregion
    }
}