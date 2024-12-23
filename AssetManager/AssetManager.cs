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
            //�������͵��ļ�����Ϸ�����ɵ���Ҫ��¼����Ƶ ͼƬ�������� 
        }
        else if (obj is Class || (obj.GetType().IsValueType && !obj.GetType().IsPrimitive))//д����
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
            //�������͵��ļ�����Ϸ�����ɵ���Ҫ��¼����Ƶ ͼƬ��������
         
        }
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum))//������
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

                    // �ȴ��������
                    while (!operation.isDone)
                    {
                        // �����Ҫ��������������ӳ�ʱ�߼�
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
        else if (typeof(T).IsClass || (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum))//�����ļ�
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
        if (typeof(T) == typeof(AudioClip))//��Ч
        {
            object audioClip = Resources.Load<AudioClip>(FileName);
            return (T)audioClip;
        }
        else if (typeof(T) == typeof(GameObject))//��Ч Ԥ���� 
        {
            object obj = Resources.Load<GameObject>(FileName);
            return (T)obj;
        }
        //����������Դ
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
