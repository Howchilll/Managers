using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class UIManager 
{
    public static Dictionary<string,ThePanel> UIObjects = new Dictionary<string,ThePanel>();

   

    public static void Show(params string[] UINames)
    {
        foreach (var name in UINames)
        {
            if (UIObjects.ContainsKey(name))
            {
              UIObjects[name].ShowMe();
            }
        }
    }
    public static void Hide(params string[] UINames)
    {
        foreach (var name in UINames)
        {
            if (UIObjects.ContainsKey(name))
            {
                UIObjects[name].HideMe();
            }
        }
    }
    

    public static void wake()
    {
    }
    public static void CleanUIObjects()
    {
        // foreach (var kv in UIObjects)
        // {
        //     if (UIObjects[kv.Key] == null)
        //     {
        //         UIObjects.Remove(kv.Key);
        //     }
        // }
        UIObjects.Clear();
    }
}
