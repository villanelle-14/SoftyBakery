using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Archive.UnityExtension.Common;
using DataModel.Plot;

namespace GameLogic
{
    class DialogDisplay : MonoBehaviour
    {

        private PortraitUI LeftOwnerSide;
        private PortraitUI rightOpposite;
        private Button m_btnUp;
        private Button m_btnDown;
        private Transform m_tfBG;
        private Dictionary<string, GameObject> roleDialogs;

        //protected override void ScriptGenerator()
        protected void ScriptGenerator()
        {
            m_btnUp = transform.FindChildComponent<Button>("m_tfBG/PageChanger/m_btnUp");
            m_btnDown = transform.FindChildComponent<Button>("m_tfBG/PageChanger/m_btnDown");
            m_tfBG = transform.Find("m_tfBG");
            m_btnUp.onClick.AddListener(OnClickUpBtn);
            m_btnDown.onClick.AddListener(OnClickDownBtn);
            roleDialogs = new Dictionary<string, GameObject>();
        }


        private OptionGroupUI m_optionGroupUI;

        private DialogBox m_dialogBox;

        //private RectTransform BgTransform;
        private BackgroundController bgController;

        //private bool isFullScreen = false;
        //private const float CurtainHeight = 100f;

        protected void BindMemberProperty()
        {
            m_optionGroupUI = transform.FindChildComponent<OptionGroupUI>("m_tfBG/DialogBox/OptionGroup");
            m_dialogBox = transform.FindChildComponent<DialogBox>("m_tfBG/DialogBox");
            bgController = transform.FindChildComponent<BackgroundController>("CgBg");
        }

        #region 事件

        private void OnClickUpBtn()
        {
        }

        private void OnClickDownBtn()
        {
        }

        #endregion

        DialogDisplayModel selfModel;
        private Section selfSection;

        private int pageIndex = 0;
        private bool isAwaitConfirm = false;
        DialogDisplayMode lastDisplayMode = DialogDisplayMode.HideMode;

        private Vector2 normalAnchorPos = new Vector2(-174.8882f,-2.85339f);
        private Vector2 normalSizeDelta = new Vector2(-473.5449f,-109.2106f);
        
        private Vector2 fullScreenAnchorPos = new Vector2(0f,0f);
        private Vector2 fullScreenSizeDelta = new Vector2(0f,0f);
        
        private Vector2 normalDialogBoxAnchorPos = new Vector2(0f,0f);
        private Vector2 normalDialogBoxSizeDelta = new Vector2(639.46f,261.79f);
        private Vector2 fullDialogBoxAnchorPos = new Vector2(0f,0f);
        private Vector2 fullDialogBoxSizeDelta = new Vector2(1000f,261.79f);
        
        private Vector3 leftShowPosition = new Vector3(0, 20, 0);
        private Vector3 LeftHidePosition = new Vector3(-477, 20, 0);
        private Vector3 RightShowPosition = new Vector3(-400, 20, 0);
        private Vector3 RightHidePosition = new Vector3(0, 20, 0);
        /*StateMachine dialogDisplayStateMachine;
        CgState cgState;
        ShopState shopState;
        NormalState normalState;
        HideState hideState;*/

        protected void OnCreate(DialogDisplayModel UserData)
        {
            //selfModel = UserData as DialogDisplayModel;
            selfModel = UserData as DialogDisplayModel;
            if (selfModel == null)
            {
                Debug.Log($"OnCreate (selfModel == null)");
            }

            pageIndex = 0;
            //dialogDisplayStateMachine = new StateMachine();
            //GameEvent.Send("LockMove");

            Debug.Log($"DialogDisplay oncreate");

            RefreshModel();
            //base.OnCreate();
            //createAnimaiton().Forget();
        }

        //protected override void OnRefresh()
        protected void OnRefresh(DialogDisplayModel UserData)
        {
            //base.OnRefresh();
            if (selfModel.IsDirty)
            {
                RefreshModel();
            }
        }

        //protected override void Close()
        protected void Close()
        {
            //UnRegisterEvent();
            //GameEvent.Send("UnLockMove");
            //base.Close();
            //GameManager.Instance.GetSingleton<PlotManager>().HideOverSceneUI().Forget();
            Debug.Log($"Close");
        }

        #region Model

        void RefreshModel()
        {
            //var newModel = UserData as DialogDisplayModel;
            selfModel.IsDirty = false;
            if (selfModel.DialogSection != null)
            {
                selfSection = selfModel.DialogSection;
                StartDialog();
            }

            if (selfModel.DisplayMode != lastDisplayMode)
            {
                RefreshDisplayMode(selfModel.DisplayMode);
            }
        }

        #endregion

        async UniTask RefreshDisplayMode(DialogDisplayMode mode)
        {
            RefreshSize();
            if (lastDisplayMode != DialogDisplayMode.Transition)
            {
                switch (mode)
                {
                    case DialogDisplayMode.HideMode:
                        await HideAndClose();
                        break;
                    case DialogDisplayMode.NormalMode:
                        //var show = Show();
                        await Show();
                        break;
                    case DialogDisplayMode.ShopMode:
                        await ShowShopMode();
                        break;
                    case DialogDisplayMode.CgMode:
                        await ShowCgUI();
                        break;
                    case DialogDisplayMode.DialogCgMode:
                        await ShowDialogCgUI();
                        break;
                    default:
                        Debug.Log($"selfModel.DisplayMode = {selfModel.DisplayMode}");
                        break;
                }
            }

            lastDisplayMode = mode;
        }

        async UniTask Show()
        {
            await HideCgUI();
            await UniTask.WhenAll(
                m_dialogBox.Show());

            //lastDisplayMode = selfModel.DisplayMode;

            Debug.Log($"show end");
        }

        async UniTask ShowShopMode()
        {
            //await HideCgUI();
            await m_dialogBox.Hide();

            lastDisplayMode = selfModel.DisplayMode;
        }

        async UniTask ShowCgUI()
        {
            if (lastDisplayMode == DialogDisplayMode.NormalMode)
            {
                lastDisplayMode = DialogDisplayMode.Transition;
                await UniTask.WhenAll( bgController.EnterCgMode());
                //await ;
            }
            else
            {
                await UniTask.WhenAll( bgController.EnterCgMode());
                //await bgController.EnterCgMode();
            }
            lastDisplayMode = selfModel.DisplayMode;
        }
        async UniTask ShowDialogCgUI()
        {
            if (lastDisplayMode == DialogDisplayMode.NormalMode)
            {
                lastDisplayMode = DialogDisplayMode.Transition;
                await UniTask.WhenAll(m_dialogBox.Show(), bgController.EnterCgMode());
            }
            else
            {
                await UniTask.WhenAll(m_dialogBox.Show(), bgController.EnterCgMode());
            }
            lastDisplayMode = selfModel.DisplayMode;
        }

        void RefreshSize()
        {
            if (selfModel.IsFullScreen)
            {
                EnterFullSceneSize();
            }
            else
            {
                EnterNormalSizeUI();
            }
        }
        void EnterFullSceneSize()
        {
            var rectTransform = m_tfBG as RectTransform;
            rectTransform.anchoredPosition = fullScreenAnchorPos;
            rectTransform.sizeDelta = fullScreenSizeDelta;

            var bgT = bgController.transform as RectTransform;
            bgT.anchoredPosition = fullScreenAnchorPos;
            bgT.sizeDelta = fullScreenSizeDelta;
            
            var dbT = m_dialogBox.transform as RectTransform;
            dbT.sizeDelta = fullDialogBoxSizeDelta;
        }
        void EnterNormalSizeUI()
        {
            var rectTransform = m_tfBG as RectTransform;
            rectTransform.anchoredPosition = normalAnchorPos;
            rectTransform.sizeDelta = normalSizeDelta;
            
            var bgT = bgController.transform as RectTransform;
            bgT.anchoredPosition = normalAnchorPos;
            bgT.sizeDelta = normalSizeDelta;
            
            var dbT = m_dialogBox.transform as RectTransform;
            dbT.sizeDelta = normalDialogBoxSizeDelta;
        }

        async UniTask HideCgUI()
        {
            if (lastDisplayMode == DialogDisplayMode.CgMode)
            {
                lastDisplayMode = DialogDisplayMode.Transition;
                await bgController.ExitCgMode();
            }
        }

        /*public async UniTask CreateDialog(Section section)
        {
            if (section == null)
            {
                Debug.LogError($"DialogDisplay receive a null section");
                return;
            }

            //DialogData = dialogData;
            //selfSection = section;
            //OnFinish = onFinish;
            pageIndex = 0;
            isAwaitConfirm = true;
            Debug.Log($"DialogData.Length = {section.dialogContents.Count}");
            await PlayNextDialog();
        }*/

        async UniTask StartDialog()
        {
            /*if (selfSection == null)
            {
                Debug.LogError($"StartDialog : selfSection == null");
                return;
            }*/

            pageIndex = 0;
            isAwaitConfirm = true;
            await PlayNextDialog();
        }

        GameObject GetRoleDialog(string name)
        {
            if (roleDialogs.ContainsKey(name))
            {
                return roleDialogs[name];
            }

            return null;
        }

        private void InitLeft(string name)
        {
            GameObject left = GetRoleDialog(name);
            if (left == null)
            {
                //left = GameObject.Instantiate(
                //GameModule.Resource.LoadAsset<GameObject>(name), m_tfBG);
                left = GameObject.Instantiate(Resources.Load<GameObject>(name));
                left.name = name;
            }

            LeftOwnerSide = left.GetComponent<PortraitUI>();
        }

        private void InitRight(string name)
        {
            GameObject right = GetRoleDialog(name);
            if (right == null)
            {
                //right = GameObject.Instantiate(
                //    GameModule.Resource.LoadAsset<GameObject>(name), m_tfBG);
                right = GameObject.Instantiate(Resources.Load<GameObject>(name));
                right.name = name;
            }

            rightOpposite = right.GetComponent<PortraitUI>();
        }

        async UniTask PlayDialog(DialogContent dialogContent)
        {
            Debug.Log($"PlayDialog");
            try
            {
                if (!string.IsNullOrEmpty(dialogContent.Event))
                {
                    //GameEvent.Send(dialogContent.Event);
                }

                Debug.Log($"dialogContent.DialogKey = {dialogContent.DialogKey}");
                //var dialogTextString = LocalizationManager.GetTranslation(dialogContent.DialogKey);
                var dialogTextString = dialogContent.DialogKey;
                if (LeftOwnerSide == null)
                {
                    InitLeft(dialogContent.LeftCharacter.Name);
                    LeftOwnerSide.Init(new DeltaPos(leftShowPosition, LeftHidePosition));
                }

                if (rightOpposite == null)
                {
                    InitRight(dialogContent.RightCharacter.Name);
                    rightOpposite.Init(new DeltaPos(RightShowPosition, RightHidePosition));
                }

                if (dialogContent.LeftCharacter.Name != null
                    && LeftOwnerSide.gameObject.name != dialogContent.LeftCharacter.Name)
                {
                    LeftOwnerSide.gameObject.SetActive(false);
                    InitLeft(dialogContent.LeftCharacter.Name);
                    LeftOwnerSide.Init(new DeltaPos(leftShowPosition, LeftHidePosition));
                }

                if (dialogContent.RightCharacter.Name != null
                    && rightOpposite.gameObject.name != dialogContent.RightCharacter.Name)
                {
                    rightOpposite.gameObject.SetActive(false);
                    InitRight(dialogContent.RightCharacter.Name);
                    rightOpposite.Init(new DeltaPos(RightShowPosition, RightHidePosition));
                }

                LeftOwnerSide.expressionName = dialogContent.LeftCharacter.Expression;
                rightOpposite.expressionName = dialogContent.RightCharacter.Expression;

                switch (dialogContent.Speaker)
                {
                    case DialogSpeaker.None:
                        break;
                    case DialogSpeaker.Left:
                        LeftOwnerSide.IsSelected = true;
                        rightOpposite.IsSelected = false;
                        break;
                    case DialogSpeaker.Right:
                        LeftOwnerSide.IsSelected = false;
                        rightOpposite.IsSelected = true;
                        break;
                    case DialogSpeaker.Both:
                        LeftOwnerSide.IsSelected = true;
                        rightOpposite.IsSelected = true;
                        break;
                }

                await m_dialogBox.StartDialogTask(dialogTextString, dialogContent.SpeakSpeed,
                    dialogContent.SpeakerName);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                //Console.WriteLine(e);
                throw;
            }
            //await m_dialogBox.StartDialogTask(dialogTextString.Term,dialogContent.SpeakSpeed);
        }

        async UniTask PlayNextDialog()
        {
            if (!isAwaitConfirm)
            {
                return;
            }

            //GameManager.Instance.GetSingleton<AudioManager>().SoundAudio("DialogConfirm04");
            if (pageIndex < selfSection.dialogContents.Count)
            {
                isAwaitConfirm = false;
                await PlayDialog(selfModel.DialogSection.dialogContents[pageIndex]);
                pageIndex++;
                isAwaitConfirm = true;
                return;
            }

            //OnFinish?.Invoke();
            if (!string.IsNullOrEmpty(selfSection.EndEvent))
            {
                //GameEvent.Send(selfModel.DialogSection.EndEvent.GetHashCode());
            }


            if (selfSection.Choices != null)
            {
                isAwaitConfirm = false;
                LeftOwnerSide.IsSelected = false;
                rightOpposite.IsSelected = false;
                m_dialogBox.ClearDialogText();
                m_optionGroupUI.Show(selfSection.Choices);
                return;
            }

            Close();
        }

        public async UniTask HideAndClose()
        {
            await HideCgUI();
            await Hide();
            Close();
            //lastDisplayMode = selfModel.DisplayMode;
        }

        async UniTask Hide()
        {
            await UniTask.WhenAll(
                rightOpposite.Hide(),
                LeftOwnerSide.Hide(),
                m_dialogBox.Hide());
        }

        public async UniTask Change2ShopMode()
        {
            //dialogDisplayMode = DialogDisplayMode.ShopMode;
            //await m_dialogBox.Hide();
        }

        /*protected override void RegisterEvent()
        {
            EventMgr.AddEvent(PlotEventChart.PlayNextDialog.GetHashCode(), () => { PlayNextDialog(); });
            /*EventMgr.AddEvent(PlotEventChart.DialogOver,
                () => { Close(); });#1#
            EventMgr.AddEvent(PlotEventChart.DialogOver.GetHashCode(),
                () => { Close(); });
            EventMgr.AddEvent(PlotEventChart.DialogEnterCgMode.GetHashCode(), () =>
            {
                GameModule.UI.ShowUI<DialogDisplay>(new DialogDisplayModel
                {
                    DisplayMode = DialogDisplayMode.CgMode
                });
            });
            EventMgr.AddEvent(PlotEventChart.DialogExitCgMode.GetHashCode(), () =>
            {
                GameModule.UI.ShowUI<DialogDisplay>(new DialogDisplayModel
                {
                    DisplayMode = DialogDisplayMode.NormalMode
                });
            });
            EventMgr.AddEvent(PlotEventChart.DialogHideOption.GetHashCode(), () => { m_optionGroupUI.Hide(); });
        }

        void UnRegisterEvent()
        {
            EventMgr.Clear();
        }*/
    }

    public class DialogDisplayModel
    {
        public Section DialogSection;
        public DialogDisplayMode DisplayMode;

        //public BaseRole DialogOwner;
        public bool IsFullScreen;
        public bool IsDirty; //是否需要刷新
    }

    public enum DialogDisplayMode
    {
        HideMode,
        NormalMode,
        ShopMode,
        CgMode, //CG模式
        DialogCgMode,
        Transition,
    }
}