using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        _instance = this as T;
    }

    void Start()
    {
        Init();
    }


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