using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BasePanel
{
    public Button btnStart;
    public Slider sliMusic;

    public void Start()
    {
        btnStart.onClick.AddListener(() => { UIManager.ChangeScene("");});
        sliMusic.onValueChanged.AddListener((float value) => {SettingData.volumeData.MusicVol= UIManager.PassValue(value); });
    }

}
