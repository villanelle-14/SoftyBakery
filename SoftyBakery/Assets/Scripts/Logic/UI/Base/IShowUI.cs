
using UnityEngine;

namespace Logic.UI.Base
{
    public interface IShowUI
    {
        float Duration { get;}
        Vector3 ShowPosition { get;}
        Vector3 HidePosition { get;}
        public void Show();
        public void Hide();
    }
}