using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    /// <summary>
    /// Компонент TMPro для пользовательского интерфейса
    /// </summary>
    private TextMeshProUGUI tmpro_ui;

    /// <summary>
    /// Компонент TMPro для мира
    /// </summary>
    private TextMeshPro tmpro_world;

    /// <summary>
    /// Возвращает текущий компонент TMPro, либо для UI, либо для мира
    /// </summary>
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    /// <summary>
    /// Текущий текст, отображаемый в компоненте
    /// </summary>
    public string currentText => tmpro.text;

    /// <summary>
    /// Целевой текст, который будет отображен
    /// </summary>
    public string targetText { get; private set; } = "";

    /// <summary>
    /// Предыдущий текст перед целевым
    /// </summary>
    public string preText { get; private set; } = "";

    /// <summary>
    /// Длина предыдущего текста
    /// </summary>
    private int preTextLength = 0;

    /// <summary>
    /// Полный целевой текст (предыдущий + целевой)
    /// </summary>
    public string fullTargetText => preText + targetText;

    /// <summary>
    /// Методы построения текста
    /// </summary>
    public enum BuildMethod { instant, typewriter, fade }

    /// <summary>
    /// Текущий метод построения текста
    /// </summary>
    public BuildMethod buildMethod = BuildMethod.typewriter;

    /// <summary>
    /// Цвет текста
    /// </summary>
    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

    /// <summary>
    /// Скорость отображения текста
    /// </summary>
    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }

    /// <summary>
    /// Базовая скорость
    /// </summary>
    private const float baseSpeed = 1;

    /// <summary>
    /// Множитель скорости
    /// </summary>
    private float speedMultiplier = 1;

    /// <summary>
    /// Количество символов, отображаемых за цикл
    /// </summary>
    private int characterPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }

    /// <summary>
    /// Множитель символов
    /// </summary>
    private int characterMultiplier = 1;

    /// <summary>
    /// Ускоренное отображение текста
    /// </summary>
    public bool hurryUp = false;

    /// <summary>
    /// Конструктор для UI текста
    /// </summary>
    /// <param name="tmpro_ui">TMPro для пользовательского интерфейса</param>
    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }

    /// <summary>
    /// Конструктор для мирового текста
    /// </summary>
    /// <param name="tmpro_world">TMPro для мира</param>
    public TextArchitect(TextMeshPro tmpro_world)
    {
        this.tmpro_world = tmpro_world;
    }

    /// <summary>
    /// Начинает построение текста
    /// </summary>
    /// <param name="text">Текст, который нужно построить</param>
    /// <returns></returns>
    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    /// <summary>
    /// Добавляет текст к текущему
    /// </summary>
    /// <param name="text">Текст, который нужно добавить</param>
    /// <returns></returns>
    public Coroutine Append(string text)
    {
        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    /// <summary>
    /// Текущий процесс построения текста
    /// </summary>
    private Coroutine buildProcess = null;

    /// <summary>
    /// Проверяет, идет ли процесс построения текста
    /// </summary>
    public bool isBuilding => buildProcess != null;

    /// <summary>
    /// Останавливает процесс построения текста
    /// </summary>
    public void Stop()
    {
        if (!isBuilding)
            return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    /// <summary>
    /// Начинает процесс построения текста
    /// </summary>
    /// <returns></returns>
    IEnumerator Building()
    {
        Prepare();
        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }
        OnComplete();
    }

    /// <summary>
    /// Завершает процесс построения текста
    /// </summary>
    private void OnComplete()
    {
        buildProcess = null;
        hurryUp = false;
    }

    /// <summary>
    /// Принудительно завершает процесс построения текста
    /// </summary>
    public void ForceComplete()
    {
        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                tmpro.ForceMeshUpdate();
                break;
        }
        Stop();
        OnComplete();
    }

    /// <summary>
    /// Подготавливает текст для отображения
    /// </summary>
    private void Prepare()
    {
        switch (buildMethod)
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    /// <summary>
    /// Подготовка для мгновенного отображения текста
    /// </summary>
    private void Prepare_Instant()
    {
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    /// <summary>
    /// Подготовка для метода печатной машинки
    /// </summary>
    private void Prepare_Typewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    /// <summary>
    /// Подготовка для метода затухания
    /// </summary>
    private void Prepare_Fade()
    {
        tmpro.text = preText;
        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            preTextLength = tmpro.textInfo.characterCount;
        }
        else
        {
            preTextLength = 0;
        }

        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpro.textInfo;

        Color colorVisible = new Color(textColor.r, textColor.g, textColor.b, 1);
        Color colorHidden = new Color(textColor.r, textColor.g, textColor.b, 0);

        Color32[] vertexColor = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) 
                continue;

            if (i < preTextLength)
            {
                for (int v = 0; v < 4; v++)
                    vertexColor[charInfo.vertexIndex + v] = colorVisible;
            }
            else
            {
                for (int v = 0; v < 4; v++)
                    vertexColor[charInfo.vertexIndex + v] = colorHidden;
            }
        }
        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    /// <summary>
    /// Процесс построения текста методом печатной машинки
    /// </summary>
    /// <returns></returns>
    private IEnumerator Build_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += hurryUp ? characterPerCycle * 5 : characterPerCycle;

            yield return new WaitForSeconds(0.015f / speed);
        }
    }

    /// <summary>
    /// Процесс построения текста методом затухания
    /// </summary>
    /// <returns></returns>
    private IEnumerator Build_Fade()
    {
        int minRange = preTextLength;
        int maxRange = minRange + 1;

        byte alphaThreshold = 15;

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color32[] vertexColor = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
        float[] alphas = new float[textInfo.characterCount];

        while (true)
        {
            float fadeSpeed = ((hurryUp ? characterPerCycle * 5 : characterPerCycle) * speed) * 4f;
            for (int i = minRange; i < maxRange; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                for (int v = 0; v < 4; v++)
                    vertexColor[charInfo.vertexIndex + v].a = (byte)alphas[i];

                if (alphas[i] >= 255)
                    minRange++;
            }
            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            bool lastCharacterIsInvisible = !textInfo.characterInfo[maxRange - 1].isVisible;
            if (alphas[maxRange - 1] > alphaThreshold || lastCharacterIsInvisible)
            {
                if (maxRange < textInfo.characterCount)
                    maxRange++;
                else if (alphas[maxRange - 1] >= 255 || lastCharacterIsInvisible)
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
