using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void BeginGame()
    {
        StartCoroutine(LoadGame());
    }
    private IEnumerator LoadGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }

    }
    public void ExitGame()
    {
        Application.Quit();
    }



}
