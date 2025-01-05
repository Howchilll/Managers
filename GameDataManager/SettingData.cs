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
    public static VolumeData VolumeData;

    static SettingData()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "VolumeData.json")))
        {
            VolumeData = new VolumeData(1, 1);
            AssetManager.WriteData(VolumeData, "VolumeData");
        }
        else
        {
            Read();
        }
    }

    static public void Read()
    {
        VolumeData = AssetManager.ReadData<VolumeData>("VolumeData");
    }

    static public void Write()
    {
        AssetManager.WriteData(VolumeData, "VolumeData");
    }


    public static void Wake()
    {
    }
}