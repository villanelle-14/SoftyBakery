using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public void Show()
    {
        var start = FindObjectOfType<StartManager>();
        if (start != null)
        {
            start.isListenStart = gameObject.activeSelf;
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        
        #endif
    }
}
