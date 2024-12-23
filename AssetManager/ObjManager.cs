using UnityEngine;
public static class ObjManager//用于添加特效和模型样式 （不建议用于复杂对象）
{
    public static void AddObj(string fileName,GameObject father, Transform pos)//父物体和位置不一样
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName),pos);
        theObj.transform.SetParent(father.transform);
     
    }

    public static void AddObj(string fileName, GameObject father)//父物体就是位置
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), father.transform);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, Transform pos)//没有父物体
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
        
    }

    public static void wake() { }
}
