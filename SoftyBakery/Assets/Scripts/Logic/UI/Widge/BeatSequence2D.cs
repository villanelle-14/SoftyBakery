using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataModel.BakeOperation;
using DG.Tweening;
using Logic.UI.Base;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic.Widge
{
    public class BeatSequence2D : MonoBehaviour, IAsyncShow
    {
        private KeyCode LeftKey = KeyCode.Mouse0;
        private KeyCode RightKey = KeyCode.Mouse1;
        [SerializeField] private SpriteRenderer Bg;
        public Action OnEndBeating;
        public Action OnBeatFalse;
        private void Awake()
        {
            for (int i = 0; i < Bg.transform.childCount; i++)
            {
                beatItems.Add(Bg.transform.GetChild(i).GetComponent<BeatItem2D>());;
            }
        }

        private void Start()
        {
            //Read(new MixingEggsModel());
        }

        #region transition animat

        public float Duration { get; set; }
        [SerializeField]
        private Vector3 ShownPos;
        [SerializeField]
        private Vector3 HiddenPos;
        public async UniTask Show()
        {
            await Bg.transform.DOMove(ShownPos, Duration).ToUniTask();
        }

        public async UniTask Hide()
        {
            await Bg.transform.DOMove(HiddenPos, Duration).ToUniTask();
        }        

        #endregion

        public Action<float> OnProgress;
        
        private bool isReading;
        BakeOperationModel bakeOperationModel;
        private int readIndex = 0;
        List<BeatItem2D> beatItems = new List<BeatItem2D>();
        public void Read(BakeOperationModel model)
        {
            isReading = true;
            bakeOperationModel = model;
            ShownModel();
        }

        void ShownModel()
        {
            for (int i = 0; i < beatItems.Count; i++)
            {
                beatItems[i].SetBeat = bakeOperationModel.InputSequence[i];
            }
        }

        void Clear()
        {
            
        }

        void CheckRead(BeatInput beat)
        {
            var hitCorrect = beatItems[readIndex].Hit(beat);
            if (hitCorrect)
            {
                readIndex++;
                float progress = (float)readIndex / beatItems.Count;
                OnProgress?.Invoke(progress);
                if (readIndex >= beatItems.Count)
                {
                    ///todo next
                    readIndex = 0;
                    OnEndBeating?.Invoke();
                }
            }
            else
            {
                readIndex = 0;
                ShownModel();
                OnBeatFalse?.Invoke();
            }
        }

        private void Update()
        {
            if (!MainUI.Instance.isPause && isReading)
            {
                //todo
                if (Input.GetKeyDown(LeftKey))
                {
                    CheckRead(BeatInput.left);
                }else if (Input.GetKeyDown(RightKey))
                {
                    CheckRead(BeatInput.right);
                }
            }
        }
    }
}