using DG.Tweening;
using UnityEngine;

namespace Logic.VIew2D
{
    public class HandTool : MonoBehaviour
    {
        public Vector3 HoldPos;
        public Vector3 HoldQua;
        public Vector3 BeatPos;
        public Vector3 BeatQua;
        public float Duration;
        public void Hit()
        {
            transform.DORotate(BeatQua, Duration).OnComplete(() => transform.DORotate(HoldQua, Duration));
            transform.DOMove(BeatPos, Duration).OnComplete(() => transform.DOMove(HoldPos, Duration));
        }
    }
}