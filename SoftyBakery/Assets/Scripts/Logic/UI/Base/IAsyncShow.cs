using Cysharp.Threading.Tasks;

namespace Logic.UI.Base
{
    public interface IAsyncShow
    {
        float Duration { get; }
        public UniTask Show();
        public UniTask Hide();
    }
}