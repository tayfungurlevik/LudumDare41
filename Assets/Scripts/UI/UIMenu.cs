using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{

    public void LoadGame()
    {
       AsyncOperation operation= SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
        operation.completed += Operation_completed;
    }

    private void Operation_completed(AsyncOperation obj)
    {
        SceneManager.LoadScene(1);
    }
}
