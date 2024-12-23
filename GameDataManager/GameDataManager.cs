using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public static class GameDataManager
{


    static void SaveGameData()
    {
        SettingData.Write();
        
    }

    static void LoadGameData()
    {
        SettingData.Read();
        
    }



    static GameDataManager()
    {



        SettingData.wake();
    }





    static void wake() { }
}
