using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }

    void Start()
    {
        //父类当中会去强行调用 初始化方法
        //该初始化方法 又是一个抽象函数 子类就必须去实现
        Init();
    }

    //主要用于 初始化 控件的事件监听 等等的逻辑 
    public abstract void Init();

    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
