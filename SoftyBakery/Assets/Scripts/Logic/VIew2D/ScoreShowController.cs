using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic.VIew2D;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShowController : MonoBehaviour
{
    [SerializeField]
    SequenceImage score33;
    [SerializeField]
    SequenceImage score66;
    [SerializeField]
    SequenceImage score100;

    public async UniTask ShowScore(int score)
    {
        //var bg = GetComponent<Image>();
        //bg.enabled = true;
        if (score < 66)
        {
            score33.gameObject.SetActive(true);
            await score33.PlaySequence();
            score33.transform.GetChild(0).gameObject.SetActive(true);
        }else if (score < 100)
        {
            score66.gameObject.SetActive(true);
            await score66.PlaySequence();
            score66.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            score100.gameObject.SetActive(true);
            await score100.PlaySequence();
            score100.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
