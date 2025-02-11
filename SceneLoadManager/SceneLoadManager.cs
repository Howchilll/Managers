using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{
    public static float loadValue=0f;
    public static bool changeScene = true;
    private static GameObject _SceneLoadManager;

    private static void Init()
    {
        if (_SceneLoadManager == null)
        {
            _SceneLoadManager = new GameObject("SceneLoadManager");
            _SceneLoadManager.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(_SceneLoadManager); 
        }
    }
    
    
    public static void LoadScene(string sceneName)
    {
        _SceneLoadManager.GetComponent<FakeMono>().StartCoroutine(_LoadScene(sceneName));
        
    }

    private static IEnumerator _LoadScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        load.allowSceneActivation = false;
   
        while (!load.isDone)
        {
            if (loadValue >= 0.9f)
            {
                if (changeScene)
                {
                    load.allowSceneActivation = true;
                   UIManager.UIObjects.Clear();
                }
            }
            else
            {
                loadValue= load.progress;  
                Debug.Log(load.progress);
            }
            yield return null;
        }

        loadValue = 0;
      
    }
    
    private class FakeMono : MonoBehaviour
    {
    }

    public static void Wake()
    {
        Init();
    }
}

