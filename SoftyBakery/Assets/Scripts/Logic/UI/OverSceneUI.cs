using Archive.UnityExtension.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameBase;
using TEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    
    public class OverSceneUI : MonoBehaviour
    {
        #region 脚本工具生成的代码
        private RawImage m_rimgBackground;
        private Text m_textAnnouncement;
        protected void ScriptGenerator()
        {
            m_rimgBackground = transform.FindChildComponent<RawImage>("m_rimgBackground");
            m_textAnnouncement = transform.FindChildComponent<Text>("m_textAnnouncement");
        }
        #endregion
        
        OverSceneMode overSceneMode;

        protected void BindMemberProperty()
        {
            
        }

        protected void RegisterEvent()
        {
            //base.RegisterEvent();
            /*GameEvent.AddEventListener(PlotEventChart.OverSceneConfirm.GetHashCode(), () =>
            {
                if (overSceneMode == OverSceneMode.Wait)
                {
                    overSceneMode = OverSceneMode.Normal;
                    Hide();
                }
            });*/
            m_textAnnouncement.GetComponent<EventTrigger>().
                triggers[0].callback.AddListener((pointerEventData) =>
                {
                    if (overSceneMode == OverSceneMode.Wait)
                    {
                        overSceneMode = OverSceneMode.Normal;
                        Hide();
                    }
                });
        }

        public async UniTask Show()
        {
            if (m_rimgBackground == null)
            {
                ScriptGenerator();
            }
            m_rimgBackground.enabled = true;
            //GameEvent.Send("LockMove");
            await m_rimgBackground.DOColor(Color.black,0.5f).ToUniTask();
        }

        public async UniTask Hide()
        {
            var hideDo = m_rimgBackground.DOColor(Color.clear, 0.5f);
            hideDo.onComplete = () =>
            {
                m_rimgBackground.enabled = false;
            };
            ClearAnnouncement();
            //GameEvent.Send("UnLockMove");
            await hideDo.ToUniTask();
            //Close();
        }
        
        public async UniTask ShowAnnouncement(string announcement)
        {
            m_textAnnouncement.enabled = true;
            overSceneMode = OverSceneMode.Print;
            int rockIndex = 0;
            while (rockIndex < announcement.Length)
            {
                m_textAnnouncement.text += announcement[rockIndex];
                rockIndex++;
                //CancellationToken
                await UniTask.Delay(100);
            }

            overSceneMode = OverSceneMode.Wait;
            while (overSceneMode == OverSceneMode.Wait)
            {
                await UniTask.DelayFrame(1,PlayerLoopTiming.PreUpdate);
            }
        }
        public void ClearAnnouncement()
        {
            m_textAnnouncement.enabled = false;
            m_textAnnouncement.text = "";
        }
    }

    public enum OverSceneMode
    {
        Normal,
        Print,
        Wait,
    }
}
