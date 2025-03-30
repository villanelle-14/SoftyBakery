using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameLogic
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private RectTransform DisplayRect;
        [SerializeField] private RectTransform CgBg;
        [SerializeField] private RectTransform TopCurtain;
        [SerializeField] private RectTransform ButtomCurtain;

        private bool isFullScene = false;
        
        public float AnimationDuration = 0.5f;
        //public float ButtomHeight;
        private readonly Vector2 BgHideSize = new Vector2(0, 0);
        private readonly Vector2 BgShowSize = new Vector2(0, -200f);
        private readonly Vector2 CurtainHideSize = new Vector2(0, 0);
        private readonly Vector2 CurtainShowSize = new Vector2(0, 100f);
        public async UniTask EnterCgMode()
        {
            gameObject.SetActive(true);
            CgBg.gameObject.SetActive(true);
            
            Debug.Log($"EnterCgMode");
            var endSize = DisplayRect.sizeDelta;
            endSize.y =endSize.y - 200f;
            await UniTask.WhenAll(DisplayRect.DOSizeDelta(endSize, AnimationDuration).ToUniTask(),
                TopCurtain.DOSizeDelta(CurtainShowSize, AnimationDuration).ToUniTask(),
                ButtomCurtain.DOSizeDelta(CurtainShowSize, AnimationDuration).ToUniTask());
            Debug.Log($"EnterCgMode end");
        }
        public async UniTask ExitCgMode()
        {
            var endSize = DisplayRect.sizeDelta;
            endSize.y = endSize.y + 200f;
            
            await UniTask.WhenAll(DisplayRect.DOSizeDelta(endSize, AnimationDuration).ToUniTask(),
                    TopCurtain.DOSizeDelta(CurtainHideSize, AnimationDuration).ToUniTask(),
                    ButtomCurtain.DOSizeDelta(-CurtainHideSize, AnimationDuration).ToUniTask());
            
            gameObject.SetActive(false);
            CgBg.gameObject.SetActive(false);
        }
    }
}