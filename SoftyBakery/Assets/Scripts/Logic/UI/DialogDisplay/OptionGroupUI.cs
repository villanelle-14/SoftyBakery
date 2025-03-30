using System;
using System.Collections.Generic;
using TEngine;
using TEngine.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class OptionGroupUI : MonoBehaviour
    {
        Stack<Button> btnHideStack = new Stack<Button>();
        Stack<Button> btnShowStack = new Stack<Button>();
        
        /// <summary>
        /// 看后面有没有按钮的预制体需求
        /// </summary>
        [SerializeField]
        Button btnPrefab;

        private void Awake()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var go = transform.GetChild(i);
                if (go.TryGetComponent<Button>(out var btn))
                {
                    btnHideStack.Push(btn);
                }
            }
        }

        public void Show(List<ScetionChoice> contents)
        {
            foreach (var content in contents)
            {
                if (!btnHideStack.TryPop(out var btn))
                {
                    var go = Instantiate(btnPrefab, transform).transform;
                    go.transform.SetAsLastSibling();
                    btn = go.GetComponent<Button>();
                }
                string translation = LocalizationManager.GetTranslation(content.ChoiceTermKey);
                var term = LocalizationManager.GetTermData(content.ChoiceTermKey);
                if (term != null)
                {
                    string t0 = term.GetTranslation(0);
                    string t1 = term.GetTranslation(1);
                    Debug.Log($"t0 = {t0} , t1 = {t1}");
                }

                Debug.Log($"clang = {LocalizationManager.CurrentLanguage} Key = {content.ChoiceTermKey} translation = {translation}");
                btn.GetComponentInChildren<Text>().text = translation;
                btn.onClick.AddListener(() =>
                {
                    //GameEvent.Send(content.ChoiceEvent);
                    content.ChoiceEvent.SendEvent();
                    //Hide();
                });
                btn.gameObject.SetActive(true);
                btnShowStack.Push(btn);
            }
        }

        public void Hide()
        {
            while (btnShowStack.TryPop(out var btn))
            {
                btnHideStack.Push(btn);
                btn.gameObject.SetActive(false);
                btn.onClick.RemoveAllListeners();
                btn.GetComponentInChildren<Text>().text = "";
            }
        }
    }
}
