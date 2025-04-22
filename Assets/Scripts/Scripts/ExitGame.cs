using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public Button exitButton; // Кнопка выхода из игры

    void Start()
    {
        // Добавление слушателя для кнопки выхода
        exitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Остановить игру в редакторе
#else
        Application.Quit(); // Выйти из приложения
#endif
    }
}