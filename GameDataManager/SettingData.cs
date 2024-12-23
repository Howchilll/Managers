using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct VolumeData
{
    public float MusicVol;
    public float SoundVol;

    public VolumeData(float musicVol, float soundVol)
    {
        MusicVol = musicVol;
        SoundVol = soundVol;
    }
}



public static class SettingData
{
    public static VolumeData volumeData;
    static SettingData() 
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "VolumeData.json")))
        {
            volumeData = new VolumeData(1, 1);
            AssetManager.WriteData(volumeData, "VolumeData");
        }
        else
        {
          Read();
        }



   
    }

    static public void Read()
    {
      volumeData = AssetManager.ReadData<VolumeData>("VolumeData");
    }
    static public void Write()
    {
        AssetManager.WriteData(volumeData, "VolumeData");
    }




    public static void wake() { }
}
