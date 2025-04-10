using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Logic.UI.Base;
using Logic.VIew2D;
using UnityEngine;

public class ToolSeleterUI : MonoBehaviour , IShowUI
{
    private bool isSelecting;
    [SerializeField] private HandTool bat;
    [SerializeField] private HandTool axe;
    [SerializeField] private HandTool nunchuncks;

    public Action OnEndSelect;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = (RectTransform)transform;
    }

    private void Update()
    {
        if (!MainUI.Instance.isPause &&isSelecting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //var obj = Instantiate(bat);
                BakeryManager.Instance.SetHandTool(bat);
                EndSelect();
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //var obj = Instantiate(axe);
                BakeryManager.Instance.SetHandTool(axe);
                EndSelect();
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //var obj = Instantiate(nunchuncks);
                BakeryManager.Instance.SetHandTool(nunchuncks);
                EndSelect();
            }
        }
    }

    public void StartSelect()
    {
        isSelecting = true;
        Show();
    }
    public void EndSelect()
    {
        isSelecting = false;
        Hide();
        OnEndSelect?.Invoke();
    }

    #region show/hidden
    [SerializeField] private Vector3 showPos;
    [SerializeField] private Vector3 hiddenPos;
    [SerializeField] private float duration;
    public float Duration { get => duration; }
    public Vector3 ShowPosition { get => showPos; }
    public Vector3 HidePosition { get => hiddenPos; }
    public void Show()
    { 
        rectTransform.DOMove(ShowPosition,Duration);
    }

    public void Hide()
    { 
        rectTransform.DOMove(HidePosition,Duration);
    }    

    #endregion


}
