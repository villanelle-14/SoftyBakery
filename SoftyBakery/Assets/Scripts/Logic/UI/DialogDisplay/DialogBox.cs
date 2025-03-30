using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameBase;
using TEngine;
using TEngine.Localization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    interface IAsyncShowUI
    {
        float Duration { get; }
        public UniTask Show();
        public UniTask Hide();
    }
    public class DialogBox : MonoBehaviour , IAsyncShowUI , IPointerClickHandler
    {
        [SerializeField] private Text DialogText;
        
        public string DialogTextString;

        public float chineseSpeed = 3f;
        public float englishSpeed = 1f;
        public float japaneseSpeed = 1f;
        public float koreanSpeed = 1f;

        private int rockRefreshTime;
        private float rockSpeed;

        public float RockSpeed
        {
            get => rockSpeed;
            set
            {
                if (value != rockSpeed)
                {
                    rockSpeed = value;
                    float languageSpeed = 0;
                    switch (LocalizationManager.CurrentLanguage)
                    {
                        case "English":
                            languageSpeed = englishSpeed;
                            break;
                        default:
                            languageSpeed = chineseSpeed;
                            break;
                    }
                    rockRefreshTime = (int)(1000 / (languageSpeed * 20));
                }
            }
        }
        private RectTransform rectTransform;
        private void Awake()
        {
            rectTransform = transform as RectTransform;
            rectTransform.anchoredPosition = HidePosition;
        }
        public async UniTask StartDialogTask(string dialogText,float Speed, string dialogTitle = "")
        {
            DialogTextString = dialogText;
            RockSpeed = Speed;
            DialogText.text = dialogTitle == "" ? string.Empty : dialogTitle + " : ";
            await ShowDialogTask();
        }
        
        
        async UniTask ShowDialogTask()
        {
            int rockIndex = 0;
            while (rockIndex < DialogTextString.Length)
            {
                DialogText.text += DialogTextString[rockIndex];
                Debug.Log($"Dialog: {DialogText.text}");
                rockIndex++;
                GameManager.Instance.GetSingleton<AudioManager>().SoundAudio("DialogPrint05");
                //CancellationToken
                await UniTask.Delay(rockRefreshTime/2);
            }
        }

        public void ClearDialogText()
        {
            DialogText.text = string.Empty;
        }

        public Vector3 showPosition;
        public Vector3 hidePosition;

        public float duration;
        public float Duration { get =>duration;}
        public Vector3 ShowPosition { get => showPosition; }
        public Vector3 HidePosition { get => hidePosition; }
        public async UniTask Show()
        {
            //RectTransform rectTransform = transform as RectTransform;
            await rectTransform.DOAnchorPos(ShowPosition, Duration).ToUniTask();
        }

        public async UniTask Hide()
        {
            //RectTransform rectTransform = transform as RectTransform;
            await rectTransform.DOAnchorPos(HidePosition, Duration).ToUniTask();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEvent.Send(PlotEventChart.PlayNextDialog.GetHashCode());
        }
    }
}
