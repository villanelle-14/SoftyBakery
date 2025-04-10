using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SequenceImage : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    //[SerializeField]
    private int curIndex = - 1;
    protected int CurIndex
    {
        get => curIndex;
    }
    protected float CurProgress
    {
        get => ((float)(curIndex + 1))/((float)(sprites.Count));
    }
        
    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        GoNextSprite();
    }

    public bool GoNextSprite()
    {
        bool res = curIndex >= sprites.Count - 2;
        curIndex = (curIndex + 1) % sprites.Count;
        GetComponent<Image>().sprite = sprites[curIndex];

        return res;
    }

    public async UniTask PlaySequence()
    {
        //curIndex = -1;
        while (!GoNextSprite())
        {
            //GoNextSprite();
            await UniTask.Delay(100);
        }
    }
}
