using LitJson;
using OpenCover.Framework.Model;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public static class AssetManager
{
    private static GameObject fakeObj;

    public static void WriteData(object obj, string FileName)//Application.persistentDataPath 
    {
        string path = Path.Combine(Application.persistentDataPath, FileName);
        if (false)
        {
            //其他类型的文件（游戏中生成的需要记录的音频 图片。。。） 
        }
        else if (obj is Class || (obj.GetType().IsValueType && !obj.GetType().IsPrimitive))//写配置
        {
            path += ".json";
            string jsonStr = "";
            jsonStr = JsonMapper.ToJson(obj);
            System.IO.File.WriteAllText(path, jsonStr);
        }

    }

    public static T ReadData<T>(string FileName)//Application.persistentDataPath 
    {
        string path = Path.Combine(Application.persistentDataPath, FileName);

        if (false)
        {
            //其他类型的文件（游戏中生成的需要记录的音频 图片。。。）
         
        }
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum))//读配置
        {
            if (System.IO.File.Exists(path + ".json"))
            {
                path = path + ".json";
                string json = System.IO.File.ReadAllText(path);
                T data = JsonMapper.ToObject<T>(json);
                return data;
            }
            else
            {
                Debug.LogError("File not found: " + path);
                return default;
            }

        }
        else
        {
            return default(T);
        }


    }

    public static T LoadData<T>(string FileName)// Application.streamingAssetsPath
    {
        string path = Path.Combine(Application.streamingAssetsPath, FileName);


        if (typeof(T) == typeof(AudioClip))
        {
            if (System.IO.File.Exists(path + ".wav")|| System.IO.File.Exists(path + ".mp3"))
            {
                path = System.IO.File.Exists(path + ".wav") ? path + ".wav" : path + ".mp3";

                using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.UNKNOWN))
                {
                    var operation = request.SendWebRequest();

                    // 等待请求完成
                    while (!operation.isDone)
                    {
                        // 如果需要，可以在这里添加超时逻辑
                    }

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed to load audio clip: {request.error}");
                        return default;
                    }


                    return (T)(object)DownloadHandlerAudioClip.GetContent(request);
                }
            }
            else
            {
                Debug.LogError("Audio file not found at: " + path);
                return default;
            }

        }
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum))//配置文件
        {
            if (System.IO.File.Exists(path + ".json"))
            {
                path = path + ".json";
                string json = System.IO.File.ReadAllText(path);
                T data = JsonMapper.ToObject<T>(json);
                return data;
            }
            else
            {
                Debug.LogError("File not found: " + path);
                return default;
            }
        }

        return default;
    }

    public static T LoadRes<T>(string FileName)//  Resources
    {
        if (typeof(T) == typeof(AudioClip))//音效
        {
            object audioClip = Resources.Load<AudioClip>(FileName);
            return (T)audioClip;
        }
        else if (typeof(T) == typeof(GameObject))//特效 预设体 
        {
            object obj = Resources.Load<GameObject>(FileName);
            return (T)obj;
        }
        //其他类型资源
        return default;
    }

    static AssetManager()
    {
        SoundManager.wake();
        ObjManager.wake();
        MusicManager.wake();
    }

    public static void wake() {}


    private class FakeClass : MonoBehaviour { }

}
