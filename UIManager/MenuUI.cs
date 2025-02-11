using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MenuUI : BasePanel<MenuUI>
{
    public Button btnStart;
    public Slider sliMusic;

    public override void Init()
    {
            btnStart.onClick.AddListener(() =>
            {
                SceneLoadManager.Wake();
             SceneLoadManager.LoadScene("SampleScene");
             
            });
        // sliMusic.onValueChanged.AddListener((value) =>
        // {
        //     
        // });


    }



    public override void ShowMe()
    {
        base.ShowMe();
        Debug.Log("ShowMe");
    }

}
