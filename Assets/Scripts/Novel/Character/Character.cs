using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CHARACTER
{
    public abstract class Character
    {
        public const bool ENABLE_ON_START = false;
        private const float UNHIGHTLIGHTED_DARKEN_STRENGTH = 0.65f;
        public const bool DEFAULT_ORIENTATION_IS_FACING_LEFT = true;
        public const string ANIMATION_REFRESH_TRIGGER = "Refresh";


        public string name = "";
        public string displayName = "";
        public RectTransform root = null;
        public CharacterConfigData config;
        public Animator animator;
        public Color color { get; protected set; } = Color.white;
        protected Color displayColor => hightlighted ? hightlightColor : unhighlightedColor;
        public Color hightlightColor => color;
        public Color unhighlightedColor => new Color(color.r * UNHIGHTLIGHTED_DARKEN_STRENGTH, color.g * UNHIGHTLIGHTED_DARKEN_STRENGTH, color.b * UNHIGHTLIGHTED_DARKEN_STRENGTH, color.a);
        public bool hightlighted { get; protected set; } = true;
        protected bool facingLeft = DEFAULT_ORIENTATION_IS_FACING_LEFT;
        public int priority { get; protected set; }

        protected CharacterManager manager => CharacterManager.Instance;
        public DialogueSystem dialogueSystem => DialogueSystem.instance;

        public Character(string name, CharacterConfigData config, GameObject prefab)
        {
            this.name = name;
            this.displayName = name;
            this.config = config;

            if(prefab != null)
            {
                RectTransform parentPanel = null;
                switch (config.characterType)
                {
                    case CharacterType.Sprite:
                    case CharacterType.SpriteSheet:
                        parentPanel = manager.characterPanel;
                        break;
                    case CharacterType.Live2D:
                        parentPanel = manager.characterPanelLive2D;
                        break;
                    case CharacterType.Model3D:
                        parentPanel = manager.characterPanelModel3D;
                        break;

                }

                GameObject ob = GameObject.Instantiate(prefab, parentPanel);
                ob.name = manager.FormatCharacterPath(manager.characterPrefabNameFormat, name);
                ob.SetActive(true);
                root = ob.GetComponent<RectTransform>();
                animator = ob.GetComponentInChildren<Animator>();
            }
        }

        //Coroutines
        protected Coroutine co_revealing, co_hiding;
        protected Coroutine co_moving;
        protected Coroutine co_changingColor;
        protected Coroutine co_hightlighting;
        protected Coroutine co_flipping;
        public bool isRevealing => co_revealing != null;
        public bool isHiding => co_hiding != null;
        public bool isMoving => co_moving != null;
        public bool isChangingColor => co_changingColor != null;
        public bool isHightlighting => (hightlighted && co_hightlighting != null);
        public bool isUnHightlighting => (!hightlighted && co_hightlighting != null);
        public bool isFacingLeft => facingLeft;
        public bool isFacingRight => !facingLeft;
        public bool isFlipping => co_flipping != null;
        public virtual bool isVisible { get; set; }
        
        public Coroutine Say(string dialogue) => Say(new List<string> { dialogue });
        public Coroutine Say(List<string> dialogue)
        {
            dialogueSystem.ShowSpeakerName(displayName);
            UpdateTextCustomizationOnScreen();
            DialogueHistory.Instance.AddDialogue(displayName, dialogue);
            return dialogueSystem.Say(dialogue);
            
        }

        public void SetNameFont(TMP_FontAsset font) => config.nameFont = font;
        public void SetNameColor(Color color) => config.nameColor = color;
        public void SetDialogueFont(TMP_FontAsset font) => config.dialogueFont = font;
        public void SetDialogueColor(Color color) => config.dialogueColor = color;

        public void ResetConfigurationData() => config = CharacterManager.Instance.GetCharacterConfig(name);
        public void UpdateTextCustomizationOnScreen() => dialogueSystem.ApplySpeakerDatToDialogueContainer(config);

        public virtual Coroutine Show()
        {
            if (isRevealing)
                return co_revealing;

            if(isHiding)
                manager.StopCoroutine(co_hiding);

            co_revealing = manager.StartCoroutine(ShowingOrHiding(true));

            return co_revealing;
        }
                
        public virtual Coroutine Hide()
        {
            if (isHiding)
                return co_hiding;

            if (isRevealing)
                manager.StopCoroutine(co_revealing);

            co_hiding = manager.StartCoroutine(ShowingOrHiding(false));

            return co_hiding;
        }
        public virtual IEnumerator ShowingOrHiding(bool show)
        {
            Debug.Log("");
            yield return null;
        }

        public virtual void SetPosition(Vector2 position)
        {
            if (root == null)
                return;

            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);

            root.anchorMin = minAnchorTarget;
            root.anchorMax = maxAnchorTarget;
        }
        public virtual Coroutine MoveToPosition(Vector2 position, float speed = 2f, bool smooth = false)
        {
            if (root == null)
                return null;

            if (isMoving)
                manager.StopCoroutine(co_moving);

            co_moving = manager.StartCoroutine(MovingToPosition(position, speed, smooth));

            return co_moving;
        }
        private IEnumerator MovingToPosition(Vector2 position, float speed = 2f, bool smooth = false)
        {
   
            (Vector2 minAnchorTarget, Vector2 maxAnchorTarget) = ConvertUITargetPositionToRelativeCharacterAnchorTargets(position);

            Vector2 padding = root.anchorMax - root.anchorMin;

            while (root.anchorMin != minAnchorTarget || root.anchorMax != maxAnchorTarget)
            {
                root.anchorMin = smooth ?
                    Vector2.Lerp(root.anchorMin, minAnchorTarget, speed * Time.deltaTime)
                  : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed * Time.deltaTime * 0.35f);

                root.anchorMax = root.anchorMin + padding;

                if(smooth && Vector2.Distance(root.anchorMin, minAnchorTarget) <= 0.001f)
                {
                    root.anchorMin = minAnchorTarget;
                    root.anchorMax = maxAnchorTarget;
                    break;
                }
                yield return null;
            }

            co_moving = null;
        }

        protected (Vector2, Vector2) ConvertUITargetPositionToRelativeCharacterAnchorTargets(Vector2 position)
        {
            Vector2 padding = root.anchorMax - root.anchorMin;

            float maxX = 1f - padding.x;
            float maxY = 1f - padding.y;

            Vector2 minAnchorTarget = new Vector2(maxX*position.x, maxY*position.y);
            Vector2 maxAnchorTarget = minAnchorTarget + padding;

            return (minAnchorTarget, maxAnchorTarget);
        }

        public virtual void SetColor(Color color)
        {
            this.color = color;
        }
        public Coroutine TransitionColor(Color color, float speed =1f)
        {
            this.color = color;
            if (isChangingColor)
                manager.StopCoroutine(co_changingColor);

            co_changingColor = manager.StartCoroutine(ChangingColor(displayColor, speed));

            return co_changingColor;
        }
        public virtual IEnumerator ChangingColor(Color color, float speed = 1f)
        {

            yield return null;
        }

        public Coroutine Hightlight(float speed = 1f)
        {
            if (isHightlighting)
                return co_hightlighting;

            if (isUnHightlighting)
                manager.StopCoroutine(co_hightlighting);

            hightlighted = true;
            co_hightlighting = manager.StartCoroutine(Hightlighting(hightlighted, speed));

            return co_hightlighting;
        }
        public Coroutine UnHightlight(float speed = 1f)
        {
            if (isUnHightlighting)
                return co_hightlighting;

            if (isHightlighting)
                manager.StopCoroutine(co_hightlighting);

            hightlighted = false;
            co_hightlighting = manager.StartCoroutine(Hightlighting(hightlighted, speed));

            return co_hightlighting;
        }
        public virtual IEnumerator Hightlighting(bool hightlight, float speedMultiplier)
        {
            yield return null;
        }

        public Coroutine Flip(float speed = 1, bool immediate = false)
        {
            if (isFacingLeft)
                return FaceRight(speed, immediate);
            else
                return FaceLeft(speed, immediate);
        }

        public Coroutine FaceLeft(float speed =1, bool immediate = false)
        {
            if (isFlipping)
                manager.StopCoroutine(co_flipping);

            facingLeft = true;
            co_flipping = manager.StartCoroutine(FaceDirection(facingLeft,speed,immediate));

            return co_flipping;
        }
        public Coroutine FaceRight(float speed =1, bool immediate = false)
        {
            if (isFlipping)
                manager.StopCoroutine(co_flipping);

            facingLeft = false;
            co_flipping = manager.StartCoroutine(FaceDirection(facingLeft, speed, immediate));

            return co_flipping;
        }

        public virtual IEnumerator FaceDirection(bool facingLeft, float speedMultiplier, bool immediate)
        {
            yield return null;
        }

        public void SetPriority(int priority, bool autoSortCharactersOnUI = true)
        {
            this.priority = priority;
            if (autoSortCharactersOnUI)
                manager.SortCharcters();
        }

        public void Animate(string animation)
        {
            animator.SetTrigger(animation);
        }
        public void Animate(string animation, bool state)
        {
            animator.SetBool(animation,state);
            animator.SetTrigger(ANIMATION_REFRESH_TRIGGER);
        }

        public virtual void OnSort(int sortingIndex)
        {
            return;
        }

        public virtual void OnRecieveCastingExpression(int layer, string expression)
        {
            return;
        }

        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D
        }
    }
}