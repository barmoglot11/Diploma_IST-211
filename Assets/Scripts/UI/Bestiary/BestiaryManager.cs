using System.Collections;
using System.Collections.Generic;
using BATTLE;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BestiaryManager : MonoBehaviour
    {
        public static BestiaryManager Instance;
        public BestiaryConfigSO config;

        public List<BestiaryConfigData> availableMonsters;
        public int currentMonster = 0;
        
        public MeshRenderer MonsterImage;
        public TextMeshProUGUI MonsterNameText;
        public TextMeshProUGUI MonsterDescText;
        
        // Звук переворота страницы
        public AudioSource pageFlipSound;
        
        public CanvasGroup canvas;

        public Button nextButton;
        public Button prevButton;
        // Настройки анимации
        [Header("Animation Settings")]
        private bool isAnimating = false;
        public float pageTransitionDuration = 0.3f;  // Продолжительность анимации перехода страницы
        public float fadeDuration = 0.3f;  // Продолжительность анимации затухания
        private Vector3 targetScale = new Vector3(430f, 300f, 285f);
        
        //fade в 0aplha -> смена данных -> fade в 1alpha

        public void AddMonsterToList(string monsterName)
        {
            availableMonsters.Add(config.GetConfig(monsterName));
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // Подписка на события кнопок
                nextButton.onClick.AddListener(OnNextButtonClicked);
                prevButton.onClick.AddListener(OnPrevButtonClicked);

                // Настройка эффекта наведения на кнопки
                SetupButtonHoverEffect(nextButton);
                SetupButtonHoverEffect(prevButton);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        
        void OnNextButtonClicked()
        {
            if (isAnimating) return;

            if (availableMonsters.Count == 1) return;
            
            // Переход на следующую страницу или к первой, если достигнута последняя
            if (currentMonster >= availableMonsters.Count - 1)
            {
                currentMonster = 0;
                StartCoroutine(SwitchPage());
            }
            else
            {
                currentMonster++;
                StartCoroutine(SwitchPage());
            }
        }

        // Обработчик нажатия кнопки "Предыдущая"
        void OnPrevButtonClicked()
        {
            if (isAnimating) return;

            if (availableMonsters.Count == 1) return;
            
            // Переход на предыдущую страницу или к последней, если достигнута первая
            if (currentMonster <= 0)
            {
                currentMonster = availableMonsters.Count - 1;
                StartCoroutine(SwitchPage());
            }
            else
            {
                currentMonster--;
                StartCoroutine(SwitchPage());
            }
        }
        
        public void ChangeInfo()
        {
            var monsterInfo = availableMonsters[currentMonster];
            MonsterImage.material = monsterInfo.MonsterMaterial;
            MonsterNameText.text = monsterInfo.MonsterName;
            MonsterDescText.text = monsterInfo.MonsterDesc;
        }
        
        IEnumerator SwitchPage()
        {
            isAnimating = true;

            // Воспроизведение звука переворота страницы
            pageFlipSound.Play();

            // Анимация выхода текущей страницы
            AnimatePageTransition(MonsterImage.gameObject, false);

            // Затухание текущего описания
            yield return StartCoroutine(FadeCanvasGroup(canvas, 0f));

            // Задержка для завершения затухания
            yield return new WaitForSeconds(fadeDuration);
            
            ChangeInfo();
            
            // Анимация появления новой страницы
            AnimatePageTransition(MonsterImage.gameObject, true);

            // Появление нового описания
            yield return StartCoroutine(FadeCanvasGroup(canvas, 1f));

            isAnimating = false;
        }
        
        void AnimatePageTransition(GameObject page, bool isIn)
        {
            Vector3 targetScaleValue = isIn ? targetScale : Vector3.zero;
            StartCoroutine(ScaleTransition(page.transform, targetScaleValue));
        }
        
        IEnumerator ScaleTransition(Transform pageTransform, Vector3 targetScale)
        {
            Vector3 startScale = pageTransform.localScale;
            float time = 0f;

            while (time < pageTransitionDuration)
            {
                pageTransform.localScale = Vector3.Lerp(startScale, targetScale, time / pageTransitionDuration);
                time += Time.deltaTime;
                yield return null;
            }

            pageTransform.localScale = targetScale;
        }
        
        IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha)
        {
            float startAlpha = canvasGroup.alpha;
            float time = 0f;

            while (time < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
                time += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
        }
        
        // Настройка эффекта наведения на кнопки
        void SetupButtonHoverEffect(Button button)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => { OnButtonHoverEnter(button); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => { OnButtonHoverExit(button); });
            trigger.triggers.Add(entryExit);
        }

        // Увеличение кнопки при наведении
        void OnButtonHoverEnter(Button button)
        {
            button.transform.localScale = Vector3.one * 1.1f;
        }

        // Восстановление размера кнопки при выходе
        void OnButtonHoverExit(Button button)
        {
            button.transform.localScale = Vector3.one;
        }
    }
}