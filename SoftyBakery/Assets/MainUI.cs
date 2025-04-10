using System;
using System.Collections;
using System.Collections.Generic;
using GameBase;
using UnityEngine;
using System.IO.Ports;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    SettingUI settingUI;

    public bool isPause;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            settingUI.Show();
        }
    }
    
    private static MainUI _instance;
    public static MainUI Instance
    {
        get => _instance;
    }
}
