using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public Button exitButton; // Кнопка выхода из игры

    void Start()
    {
        exitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Останавливает игру в редакторе
        #else
        Application.Quit(); // Закрывает приложение
        #endif
    }
}