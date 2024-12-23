using UnityEngine;
public static class ObjManager//���������Ч��ģ����ʽ �����������ڸ��Ӷ���
{
    public static void AddObj(string fileName,GameObject father, Transform pos)//�������λ�ò�һ��
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName),pos);
        theObj.transform.SetParent(father.transform);
     
    }

    public static void AddObj(string fileName, GameObject father)//���������λ��
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), father.transform);
        theObj.transform.SetParent(father.transform);
    }

    public static void AddObj(string fileName, Transform pos)//û�и�����
    {
        GameObject theObj = Object.Instantiate(AssetManager.LoadRes<GameObject>(fileName), pos);
        
    }

    public static void wake() { }
}
