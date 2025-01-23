using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public static class SoundManager
{
    private static GameObject _soundManagerObject;

    
    private static void Init()
    {
        if (_soundManagerObject == null)
        {
            _soundManagerObject = new GameObject("SoundManager");
            _soundManagerObject.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(_soundManagerObject); //
        }
    }

    public static void AddSound(string fileName, float delayTime, float volume,
        GameObject father = null, float minDis = 1, float maxDis = 10)
    {
    
            AudioClip clip = AssetManager.LoadRes<AudioClip>(fileName);
            if (clip == null) return;
            AudioSource audioSource = null;

            if (father == null) audioSource = _soundManagerObject.AddComponent<AudioSource>();
            else audioSource = father.AddComponent<AudioSource>();
  
            
            
        
            audioSource.clip = clip;
            audioSource.playOnAwake = false;
            audioSource.volume = Mathf.Clamp01(volume) * SettingData.VolumeData.SoundVol;
            audioSource.loop = false;


            if (father != null)
            {
                audioSource.spatialBlend = 1.0f;
                audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
                audioSource.minDistance = minDis;
                audioSource.maxDistance = maxDis;
            }

            Debug.Log("1");
            _soundManagerObject.GetComponent<FakeMono>()
                .StartCoroutine(PlaySoundCoroutine(delayTime, audioSource));
         
            
    }

    
    
    private static IEnumerator PlaySoundCoroutine(float delayTime, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.2f);
        Object.Destroy(audioSource);
        Debug.Log("2");
    }


    private class FakeMono : MonoBehaviour
    {
    }

    public static void Wake()
    {
        Init();
    }
}