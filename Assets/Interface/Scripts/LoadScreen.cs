using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScreen : MonoBehaviour
{

    public GameObject LoadingScreen;
    public TextMeshProUGUI textComponent;
    public Slider scale;
    public int SceneID = 1;

    // Start is called before the first frame update
    public void Loading()
    {
        LoadingScreen.SetActive(true);

        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(2.2f);
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(SceneID);
        loadAsync.allowSceneActivation = false;
        textComponent.color = Color.white;
        while (!loadAsync.isDone)
        {
            scale.value = loadAsync.progress;

            if(loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
