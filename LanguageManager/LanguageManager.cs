using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  LanguageManager
{
  public static Dictionary<string, string> LanDic = new Dictionary<string, string>();
  private static string LanName;
  private static Language _language;

  private static void  init()
  {
    LanName="";
    loadLanguage();
  }
  
  public static void loadLanguage(string languageName="English")
  {
    if (LanName != languageName)
    {
      LanDic.Clear();
      _language= AssetManager.LoadData<Language>(languageName);
      LanName = _language.LanguageName;
      LanDic=_language.LanguageDictionary;
    }
  }

  public static void Wake()
  {
    init();
  }
}
public class Language
{
  public string LanguageName;
  public Dictionary<string, string> LanguageDictionary = new Dictionary<string, string>();
}
