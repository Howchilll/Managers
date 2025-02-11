using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{
    public static float LoadValue=0f;
    public static bool ChangeScene = true;
    private static GameObject _sceneLoadManager;

    private static void Init()
    {
        if (_sceneLoadManager == null)
        {
            _sceneLoadManager = new GameObject("SceneLoadManager");
            _sceneLoadManager.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(_sceneLoadManager); 
        }
    }
    
    
    public static void LoadScene(string sceneName)
    {
        _sceneLoadManager.GetComponent<FakeMono>().StartCoroutine(_LoadScene(sceneName));
        
    }

    private static IEnumerator _LoadScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        load.allowSceneActivation = false;
   
        while (!load.isDone)
        {
            if (LoadValue >= 0.9f)
            {
                if (ChangeScene)
                {
                    load.allowSceneActivation = true;
                   UIManager.UIObjects.Clear();
                }
            }
            else
            {
                LoadValue= load.progress;  
            }
            yield return null;
        }

        LoadValue = 0;
      
    }
    
    private class FakeMono : MonoBehaviour
    {
    }

    public static void Wake()
    {
        Init();
    }
}

