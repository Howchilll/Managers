using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
   public static void InitAll()
   {
      AssetManager.Wake();
      GameDataManager.Wake();
      SceneLoadManager.Wake();
      UIManager.Wake();
      LanguageManager.Wake();
   }
}
