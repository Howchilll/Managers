using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

public static class MusicManager
{

    private static GameObject _musicManagerObject;
    private static Dictionary<string, AudioSource> _asDic = new Dictionary<string, AudioSource>();
    private static Dictionary<string, AudioClip> _acDic = new Dictionary<string, AudioClip>();
    
    private static void Init()
    {
        if (_musicManagerObject == null)
        {
            _musicManagerObject = new GameObject("MusicManager");
            _musicManagerObject.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(_musicManagerObject);
        }
    }

    public static void AddMusic(string fileName, float delay, float volume,
        GameObject father = null, float minDis = 1, float maxDis = 10) //
    {
        if (!_asDic.ContainsKey(fileName))
        {
            AudioClip clip=null;
            AudioSource audioSource = null;
            if (_acDic.ContainsKey(fileName))
            {
                clip = _acDic[fileName];
            }
            else
            {  clip = AssetManager.LoadData<AudioClip>(fileName);  
                if (clip == null) return;  
                _acDic.Add(fileName, clip);
            }


            if (father == null) audioSource = _musicManagerObject.AddComponent<AudioSource>();
            else audioSource = father.AddComponent<AudioSource>();
            
            _asDic.Add(fileName, audioSource);
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = Mathf.Clamp01(volume) * SettingData.data.volumeData.MusicVol;

            if (father != null)
            {
                audioSource.spatialBlend = 1.0f;
                audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
                audioSource.minDistance = minDis;
                audioSource.maxDistance = maxDis;
            }
            
            _musicManagerObject.GetComponent<FakeMono>()
                .StartCoroutine(PlayMusicCoroutine(delay, audioSource));
        }

    }

    public static void EndMusic(string fileName)
    {
        if (_asDic.ContainsKey(fileName) && (_asDic[fileName] != null))
            _musicManagerObject.GetComponent<FakeMono>()
                .StartCoroutine(EndMusicCoroutine(fileName, _asDic[fileName]));
    }

    public static void ChangeVolume(int vol=1)
    {
        foreach (AudioSource audioSource in _asDic.Values)
        {
            audioSource.volume *= SettingData.data.volumeData.MusicVol*vol;
        }
    }

    private static IEnumerator PlayMusicCoroutine(float delayTime, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }

    private static IEnumerator EndMusicCoroutine(string fileName, AudioSource audioSource, float time = 2)
    {
        if (_asDic.ContainsKey(fileName))
        {
            float startVolume = audioSource.volume; // 
            float timeElapsed = 0f;
            while (timeElapsed < time)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / time); // 
                timeElapsed += Time.deltaTime;
                yield return null; // 
            }

            audioSource.volume = 0f; // 
            _asDic.Remove(fileName);
        }
    }

    public static void PreAdd(string fileName)
    {
        _acDic.Add(fileName, AssetManager.LoadData<AudioClip>(fileName));
    }

    public static void ClearRam()
    {
        _acDic.Clear();
    }
    
    
    
    private class FakeMono : MonoBehaviour
    {
    }

    public static void Wake()
    {
        Init();
    }
}

