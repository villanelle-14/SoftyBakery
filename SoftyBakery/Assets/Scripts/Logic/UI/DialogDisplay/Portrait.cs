using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Logic.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class Portrait : MonoBehaviour, IAsyncShow
    {
        [SerializeField] Image portraitImage;

        public DeltaView UnSelected;
        public DeltaView Selected;
        public float SelectedAniDuration = 0.3f;
        private bool isSelected;
        private int index = -1;
        public DeltaPos DeltaPos;
        [SerializeField] private Transform expressionParent; //表情

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnSelected();
                }
            }
        }

        public string expressionName;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Init(DeltaPos deltaPos)
        {
            DeltaPos = deltaPos;
            (transform as RectTransform).anchoredPosition = DeltaPos.hidePosition;
            gameObject.SetActive(true);
            Debug.Log($"anchoredPosition = {(transform as RectTransform).anchoredPosition}");
            Show();
        }

        void OnSelected()
        {
            DoSelectedAnimation(portraitImage, isSelected ? Selected : UnSelected, SelectedAniDuration, isSelected);
            //portraitImage.tr
            /*if (isSelected)
            {
                DoSelectedAnimation(portraitImage, Selected, SelectedAniDuration);
            }
            else
            {
                DoSelectedAnimation(portraitImage, UnSelected, SelectedAniDuration);
            }*/
        }

        void DoSelectedAnimation(Image image, DeltaView deltaView, float duration, bool isSelected)
        {
            if (isSelected)
            {
                if (!string.IsNullOrEmpty(expressionName))
                {
                    int count = expressionParent.transform.childCount;
                    for (int i = 0; i < count; i++)
                    {
                        if (expressionParent.transform.GetChild(i).name == expressionName)
                        {
                            index = i;
                            expressionParent.transform.GetChild(i).gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }
            else
            {
                if (index != -1)
                {
                    expressionParent.transform.GetChild(index).gameObject.SetActive(false);
                }
            }


            image.rectTransform.DOScale(deltaView.TargetScale, duration);
            image.DOColor(deltaView.TargetColor, duration);
        }

        public Sprite GetSprite(string spriteName)
        {
            return Resources.Load<Sprite>(spriteName);
            //return GameModule.Resource.LoadAsset<Sprite>(spriteName);
        }

        public float duration;

        public float Duration
        {
            get => duration;
        }


        public async UniTask Show()
        {
            //Debug.Log($" {name} ；{ShowPosition} {(transform as RectTransform).anchoredPosition}");
            await (transform as RectTransform).DOAnchorPos(DeltaPos.showPosition, Duration).ToUniTask();
            //Debug.Log($" {name} ；{ShowPosition} {(transform as RectTransform).anchoredPosition}");
        }


        public async UniTask Hide()
        {
            await (transform as RectTransform).DOAnchorPos(DeltaPos.hidePosition, Duration).ToUniTask();
            gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class DeltaView
    {
        public Vector3 TargetScale;
        public Color TargetColor;
    }

    [Serializable]
    public class DeltaPos
    {
        public Vector3 showPosition;
        public Vector3 hidePosition;

        public DeltaPos(Vector3 show, Vector3 hide)
        {
            showPosition = show;
            hidePosition = hide;
        }
    }
}