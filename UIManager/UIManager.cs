using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class UIManager  //ΪUI�ӿ��ṩ����
{
    public static void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public static float PassValue(float value)
    {
        return value;
    }


}