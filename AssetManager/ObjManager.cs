using UnityEngine;

public static class ObjManager //
{
    public static void AddObj(string fileName, GameObject father, Transform pos) //
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, GameObject father) //
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), father.transform);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, Transform pos) //
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
    }

    public static void Wake()
    {
    }
}