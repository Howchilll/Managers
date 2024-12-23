using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public static class MusicManager
{
    private static GameObject musicManagerObject;
    private static bool isInit = false;
    private static Dictionary<string, AudioSource> MSDic = new Dictionary<string, AudioSource>();
    private static void Init()
    {
        if (musicManagerObject == null)
        {
            musicManagerObject = new GameObject("SoundManager");
            musicManagerObject.transform.position = Camera.main.transform.position;
            musicManagerObject.transform.SetParent(Camera.main.transform);
            musicManagerObject.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(musicManagerObject); // ȷ���糡������
            isInit = true;
        }
    }

    public static void AddMusic(string fileName,float delay,float volume,GameObject father= null,float minDis=1, float maxDis=10)//������չ��
    {
        AudioSource audioSource=null;
        if (!isInit) Init();
        AudioClip clip = AssetManager.LoadData<AudioClip>(fileName);

        if(father==null) audioSource = musicManagerObject.AddComponent<AudioSource>();
        else audioSource = father.AddComponent<AudioSource>();

        MSDic.Add(fileName, audioSource);
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = Mathf.Clamp01(volume)*SettingData.volumeData.MusicVol;

        if(father!=null)
        {
            audioSource.spatialBlend = 1.0f;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.minDistance = minDis;
            audioSource.maxDistance = maxDis;
        }



        musicManagerObject.GetComponent<FakeMono>()
       .StartCoroutine(PlayMusicCoroutine(delay, audioSource));
    }
    
    public static void EndMusic(string fileName)
    {
        if (MSDic.ContainsKey(fileName)&&(MSDic[fileName]!=null))
        musicManagerObject.GetComponent<FakeMono>()
       .StartCoroutine(EndMusicCoroutine(fileName, MSDic[fileName]));
    }

    public static void ChangeVolume()
    {
        foreach(AudioSource AS in MSDic.Values)
        {
            AS.volume *= SettingData.volumeData.MusicVol;
        }
    }

    private static IEnumerator PlayMusicCoroutine(float delayTime, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }

    private static IEnumerator EndMusicCoroutine(string fileName,AudioSource audioSource, float time=2)
    {
        float startVolume = audioSource.volume; // ��ȡ��ǰ����
        float timeElapsed = 0f;
        while (timeElapsed < time)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / time); // ʹ�� Lerp ƽ������
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }
        audioSource.volume = 0f; // ȷ����������Ϊ 0
        MSDic.Remove(fileName);
    }

    private class FakeMono : MonoBehaviour { }

    public static void wake() { }
}
