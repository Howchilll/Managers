using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class SettingData //某一类数据
{
    public static TheSetingData data;


    
    static SettingData()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath,
                "SettingData.json"))) //因为数据组是一起的 所以VolumeData 不在 ScreenData 也肯定不在
        {
            data = new TheSetingData
            (
                new TheSetingData.VolumeData(1, 1),
                new TheSetingData.ScreenData(1, 1, 1, 1)
            );
            AssetManager.WriteData(data, "SettingData");
        }
        else Read();
    }

    static public void Read()
    {
        data = AssetManager.ReadData<TheSetingData>("SettingData");
    }

    static public void Write()
    {
        AssetManager.WriteData(data, "SettingData");
    }


    public static void Wake()
    {
    }
    
    public struct TheSetingData
    {
      public   VolumeData volumeData;
      public  ScreenData screenData;


        public struct VolumeData //某一组数据（结构体） 
        {
            public float MusicVol;
            public float SoundVol;

            public VolumeData(float musicVol, float soundVol)
            {
                MusicVol = musicVol;
                SoundVol = soundVol;
            }
        }

        public struct ScreenData
        {
            public  float Brightness;
            public  float Contrast;
            public  float Saturation;
            public  float Gamma;

            public ScreenData(float brightness, float contrast, float saturation, float gamma)
            {
                Brightness = brightness;
                Contrast = contrast;
                Saturation = saturation;
                Gamma = gamma;
            }
        }

        public TheSetingData(VolumeData volumeData, ScreenData screenData)
        {
            this.volumeData = volumeData;
            this.screenData = screenData;
        }
    }
}

//用法 申明数据组 加入数据类 并加入读写函数 